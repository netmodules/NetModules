/*
    The MIT License (MIT)

    Copyright (c) 2019 John Earnshaw.
    Repository Url: https://github.com/johnearnshaw/netmodules/

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
 */

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Collections.Generic;
using NetModules.Interfaces;

namespace NetModules.Classes
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AssemblyLoader : AssemblyLoadContext
    {
        string AssemblyIdentifier;
        Assembly LoadedAssembly;
        static Dictionary<string, Assembly> ReferencedAssemblies;
        object Padlock = new object();

        internal bool KeepAlive;

        /// <summary>
        /// 
        /// </summary>
        public AssemblyLoader(AssemblyName name, bool keepAlive = false)
            //: base(!keepAlive)
        {
            if (name == null)
            {
                throw new Exception("AssemblyLoader name parameter is null.");
            }

            AttachEvents();
            AssemblyIdentifier = name.FullName;
            KeepAlive = keepAlive;
        }


        /// <summary>
        /// 
        /// </summary>
        public AssemblyLoader(Uri path, bool keepAlive = false)
            //: base(!keepAlive)
        {
            if (path == null || !path.IsFile)
            {
                throw new Exception("AssemblyLoader path parameter must be a System.Uri that targets a local file.");
            }

            AttachEvents();
            AssemblyIdentifier = path.LocalPath;
            KeepAlive = keepAlive;
        }


        /// <summary>
        /// 
        /// </summary>
        void AttachEvents()
        {
            Resolving += AssemblyLoader_Resolving;
            Unloading += AssemblyLoader_Unloading;
        }


        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Type> GetExportedTypes()
        {
            if (LoadedAssembly != null)
            {
                return LoadedAssembly.GetExportedTypes();
            }

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        private void AssemblyLoader_Unloading(AssemblyLoadContext obj)
        {
            if (obj is AssemblyLoader loader && loader.KeepAlive)
            {
                // Garbage collector tries to collect our Module's AssemblyLoadContext even though
                // it's globally referenced in ModuleContainer. To try and avoid errors we tell GC
                // to keep alive so that ModuleHost.ModuleCollection still holds the reference to
                // the Module instance while the AssemblyLoadContext may have been destroyed..?
                // KeepAlive seems to fix spontaneous fatal error. Internal CLR error. (0x80131506)
                // Although it may be enough to just not call our unload method. Needs more testing...
                GC.KeepAlive(obj);
                GC.SuppressFinalize(obj);
            }
            else
            {
                this.Unload();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private Assembly AssemblyLoader_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            return Load(arg2);
        }


        /// <summary>
        /// 
        /// </summary>
        public Assembly Load(bool keepAlive = false)
        {
            KeepAlive = keepAlive;
            return Load(AssemblyIdentifier);
        }


        /// <inheritdoc/>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            return Load(assemblyName.FullName);
        }


        /// <summary>
        /// 
        /// </summary>
        protected Assembly Load(string assemblyIdentifier)
        {
            try
            {
                string assemblyLocation = assemblyIdentifier.IndexOf('\\') > -1
                    || assemblyIdentifier.IndexOf('/') > -1
                    ? assemblyIdentifier
                    : null;

                var assemblyName = string.IsNullOrEmpty(assemblyLocation)
                    ? new AssemblyName(assemblyIdentifier)
                    : AssemblyName.GetAssemblyName(assemblyLocation);

                lock (Padlock)
                {
                    if (ReferencedAssemblies == null)
                    {
                        ReferencedAssemblies = new Dictionary<string, Assembly>();
                    }

                    if (LoadedAssembly != null && LoadedAssembly.GetName().Name == assemblyName.Name)
                    {
                        return LoadedAssembly;
                    }

                    Assembly assembly = null;

                    if (ReferencedAssemblies.TryGetValue(assemblyName.Name, out assembly))
                    {
                        if (LoadedAssembly == null)
                        {
                            LoadedAssembly = assembly;
                        }

                        return assembly;
                    }

                    var current = AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic);

                    // It's possible that assemblyName can equal both the path to an assembly file location or the simple
                    // name of an assembly. We must compare against both when checking for currently loaded assemblies.
                    Assembly loaded = null;

                    if (assemblyName != null)
                    {
                        loaded = current.FirstOrDefault(x => x.GetName().Name == assemblyName.Name);
                    }
                    else if (!string.IsNullOrEmpty(assemblyLocation))
                    {
                        loaded = current.FirstOrDefault(x => x.Location == assemblyLocation);
                    }


                    if (loaded != null)
                    {
                        ReferencedAssemblies.Add(assemblyName.Name, loaded);
                        assembly = loaded;
                    }

                    if (assembly != null)
                    {
                        if (LoadedAssembly == null)
                        {
                            LoadedAssembly = assembly;
                        }

                        return assembly;
                    }

                    string assemblyPath = GetPossibleReferencePath(assemblyLocation);

                    if (assemblyPath != null)
                    {
                        // Try loading via the default AssemblyLoadContext 
                        assembly = Default.LoadFromAssemblyPath(assemblyPath);// LoadFromAssemblyPath(assemblyPath);
                    }

                    if (assembly == null && assemblyName != null)
                    {
                        // Try loading via the default AssemblyLoadContext 
                        assembly = Default.LoadFromAssemblyName(assemblyName);
                    }

                    if (assembly != null)
                    {
                        ReferencedAssemblies.Add(assemblyName.Name, assembly);

                        if (LoadedAssembly == null && (assembly.Location == AssemblyIdentifier
                            || assembly.GetName().Name == AssemblyIdentifier))
                        {
                            LoadedAssembly = assembly;
                        }

                        return assembly;
                    }
                }
            }
            catch { }
            return null;
        }


        /// <inheritdoc/>
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = GetPossibleReferencePath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }


        /// <summary>
        /// 
        /// </summary>
        public new void Unload()
        {
            if (LoadedAssembly != null)
            {
                LoadedAssembly = null;
            }

            KeepAlive = false;

            // Possibly don't remove referenced assemblies???
            if (ReferencedAssemblies != null)
            {
                ReferencedAssemblies = null;
            }

            GC.ReRegisterForFinalize(this);

            // base.Unload();
            // ^^^^^^^^^^^^^^ Throws Fatal error. Internal CLR error. (0x80131506)
        }


        /// <summary>
        /// 
        /// </summary>
        string GetPossibleReferencePath(string assemblyName)
        {
            if (assemblyName == null)
            {
                return null;
            }

            if (assemblyName.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase) || assemblyName.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase))
            {
                assemblyName = assemblyName.Substring(0, assemblyName.Length - 4);
            }

            var dir = Path.GetDirectoryName(AssemblyIdentifier);
            var relative = Path.Combine(dir, assemblyName);
            string parent;

            try
            {
                parent = Directory.GetParent(dir).ToString();
                parent = Path.Combine(parent, assemblyName);
            }
            catch
            {
                parent = assemblyName;
            }

            var fileNames = new string[]
            {
                relative + ".dll",
                relative + ".exe",
                parent + ".dll",
                parent + ".exe"
            };

            foreach (var f in fileNames)
            {
                if (File.Exists(f))
                {
                    return f;
                }
            }

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        public Type[] GetTypes()
        {
            Load();
            return LoadedAssembly.GetTypes();
        }
    }
}

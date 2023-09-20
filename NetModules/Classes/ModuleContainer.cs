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
using NetModules.Events;
using NetModules.Interfaces;

namespace NetModules.Classes
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ModuleContainer : IModuleContainer, IModule
    {
        /// <inheritdoc/>
        public IModuleHost Host { get; private set; }


        /// <inheritdoc/>
        public bool Initialized {  get { return Module != null; } }


        /// <inheritdoc/>
        public Uri Path
        {
            get; private set;
        }


        /// <inheritdoc/>
        public AssemblyLoader LoadContext
        {
            get; internal set;
        }


        /// <inheritdoc/>
        public Type ModuleType
        {
            get; private set;
        }


        /// <inheritdoc/>
        public Module Module { get; private set; }


        /// <inheritdoc/>
        public IModuleAttribute ModuleAttributes { get; internal set; }


        /// <inheritdoc/>
        public Uri WorkingDirectory { get { return Module.WorkingDirectory; } }


        /// <inheritdoc/>
        public bool Loaded { get { return Initialized && Module.Loaded; } }


        /// <inheritdoc/>
        public ModuleContainer(Uri path, Type type, IModuleAttribute attribute, IModuleHost host)
        {
            Host = host;
            Path = path;
            ModuleType = type;
            ModuleAttributes = attribute;
            LoadContext = new AssemblyLoader(path, true);
        }



        /// <inheritdoc/>
        public void InitializeModule()
        {
            try
            {
                LoadContext.Load(true);
                var module = TypeManager.InstantiateModule(ModuleType, LoadContext);
                module.ModuleAttributes = ModuleAttributes;
                module.Host = Host;
                Module = module;
            }
            catch (Exception ex)
            {
                Host.Log(LoggingEvent.Severity.Error
                    , "An error occurred while attempting to initialize a module."
                    , Path
                    , ModuleType.FullName
                    , ex);
            }
        }


        /// <inheritdoc/>
        public void DeinitializeModule()
        {
            Module = null;
            
            if (LoadContext.IsCollectible)
            {
                LoadContext.Unload();
            }
        }


        /// <inheritdoc/>
        public T GetSetting<T>(string name, T @default = default)
        {
            return Module.GetSetting<T>(name, @default);
        }


        /// <inheritdoc/>
        public void Log(LoggingEvent.Severity severity, params object[] args)
        {
            Module.Log(severity, args);
        }


        /// <inheritdoc/>
        public void OnLoading()
        {
            Module.OnLoading();
        }


        /// <inheritdoc/>
        public void OnLoaded()
        {
            Module.OnLoaded();
        }


        /// <inheritdoc/>
        public void OnAllModulesLoaded()
        {
            Module.OnAllModulesLoaded();
        }


        /// <inheritdoc/>
        public void OnUnloading()
        {
            Module.OnUnloading();
        }


        /// <inheritdoc/>
        public void OnUnloaded()
        {
            Module.OnUnloaded();
        }


        /// <inheritdoc/>
        public bool CanHandle(IEvent e)
        {
            return Module.CanHandle(e);
        }


        /// <inheritdoc/>
        public void Handle(IEvent e)
        {
            Module.Handle(e);
        }


        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is ModuleContainer container)
            {
                container.ModuleAttributes.Name.Equals(ModuleAttributes.Name);
            }

            return base.Equals(obj);
        }


        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return ModuleAttributes.Name.GetHashCode();
        }


        /// <inheritdoc/>
        public override string ToString()
        {
            if (Module != null)
            {
                return Module.ModuleAttributes.Name;
            }

            return base.ToString();
        }
    }
}

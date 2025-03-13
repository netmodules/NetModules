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
using System.Collections.Generic;

namespace NetModules.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModuleCollection
    {
        /// <summary>
        /// This should return a list containing the ModuleName for all known modules whether they are loaded or not.
        /// </summary>
        IList<ModuleName> GetModuleNames();


        /// <summary>
        /// This should return a list of loaded modules. Other modules can check this list for modules that they may depend on. If an IEvent is passed
        /// to this method, a list containing only loaded modules that are able to handle the event type should be returned.
        /// </summary>
        IList<IModule> GetLoadedModules(IEvent @event = null);


        /// <summary>
        /// This method should tell the IModuleHost instance to import modules. Modules are imported from the ModuleHost working directory.
        /// </summary>
        void ImportModules();


        /// <summary>
        /// Load a specific module by its ModuleName.
        /// </summary>
        /// <param name="module"></param>
        void LoadModule(ModuleName module);

        /// <summary>
        /// This should invoke the OnLoad() method on each imported module followed the OnLoaded() method on each module. This allows for things like module
        /// dependency checking, initialization, threaded loops etc... This should enable the Handle() event for each loaded module. If modules is null, all
        /// imported modules should be loaded.
        /// </summary>
        /// <param name="modules"></param>
        void LoadModules(IList<ModuleName> modules = null);

        /// <summary>
        /// Unload a specific module by its ModuleName.
        /// </summary>
        /// <param name="module"></param>
        void UnloadModule(ModuleName module);

        /// <summary>
        /// If implemented, this method should disable/unload the loaded modules.
        /// </summary>
        /// <param name="modules">A list of selected modules to unload. If null, all modules should be unloaded.</param>
        void UnloadModules(IList<ModuleName> modules = null);


        /// <summary>
        /// If implemented, this method should reload the loaded modules.
        /// </summary>
        /// <param name="modules">A list of selected modules to reload. If null, all modules should be reloaded.</param>
        void ReloadModules(IList<ModuleName> modules = null);


        /// <summary>
        /// This should check to see if a module is available (imported) and can be used by modules to check for other module dependancy etc...
        /// </summary>
        /// <param name="name">Name of the module to look for.</param>
        /// <param name="min">(Optional) Specify a minimum module version.</param>
        /// <param name="max">(Optional) Specify a maximum module version.</param>
        bool HasModule(ModuleName name, Version min = null, Version max = null);


        /// <summary>
        /// This should return a list of modules that implement a specific <see cref="IModule"/> type.
        /// </summary>
        IList<IModule> GetModulesByType<T>() where T : IModule;


        /// <summary>
        /// Returns a list of instances that are registered as <see cref="IEventPreHandler"/> types for a specified <see cref="IEvent"/> type.
        /// </summary>
        /// <param name="e">The <see cref="IEvent"/> type that each returned instance must be assigned to pre-handle.</param>
        IList<IEventPreHandler> GetPreHandlers(IEvent e);


        /// <summary>
        /// Returns a list of instances that are registered as <see cref="IEventPostHandler"/> types for a specified <see cref="IEvent"/> type.
        /// </summary>
        /// <param name="e">The <see cref="IEvent"/> type that each returned instance must be assigned to post-handle.</param>
        IList<IEventPostHandler> GetPostHandlers(IEvent e);
    }
}

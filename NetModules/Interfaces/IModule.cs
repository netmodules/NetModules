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

namespace NetModules.Interfaces
{
    /// <summary>
    /// An IModule is an <see cref="IEventHandler">IEventHandler</see> that can be instantiated by an <see cref="IModuleHost">IModuleHost</see>.
    /// This interface exposes members to the IModuleHost that identify both the IModule itself and what IEvents it is able to handle.
    /// </summary>
    public interface IModule : IEventHandler
    {
        /// <summary>
        /// Each module requires a reference to the IModuleHost that parents the module. Make sure you set this property!
        /// </summary>
        IModuleHost Host { get; }

        /// <summary>
        /// Attributes contains the standard information about the instance implementing IModule such as its name, description and
        /// any dependencies.
        /// </summary>
        IModuleAttribute ModuleAttributes { get; }


        /// <summary>
        /// The WorkingDirectory property should return the absolute path to the directory the module was loaded from.
        /// This is used by the IModule to access external files that it may depend on such as a required file,
        /// external executable or resource.
        /// </summary>
        Uri WorkingDirectory { get; }


        /// <summary>
        /// The IModule should be marked as loaded and this property should be checked before any object that implements IEvent can be handled.
        /// </summary>
        bool Loaded { get; }


        /// <summary>
        /// Implemented due to demand. An object implementing IModule may often require the ability to retrieve configurable settings. This method is implemented in Module
        /// and acts as a wrapper for pushing a GetSettingEvent into the IModuleHost for handling. If a module is not provided to handle the GetSettingEvent then retrieving
        /// the requested setting will fail. Functionality can be overridden. For security reasons this IModule should not be allowed to access another IModule instances settings.
        /// </summary>
        /// <typeparam name="T">The type of the required setting.</typeparam>
        /// <param name="name">The identifier string for the required setting. This is passed to the generated LoggingEvent for handling.</param>
        /// <param name="default">The default setting to use if a configured setting is not available or if the returned setting is the wrong type.</param>
        T GetSetting<T>(string name, T @default = default);


        /// <summary>
        /// Implemented due to demand. An object implementing IModule may often require the ability to log debug data and analytics. This method is implemented in Module
        /// and acts as a wrapper for pushing a LoggingEvent into the IModuleHost for handling. If a module is not provided to handle the LoggingEvent then logging will
        /// fail. This Functionality can be overridden on a per-module basis.
        /// </summary>
        /// <param name="severity">The severity of logging required. This is passed to the generated LoggingEvent for handling.</param>
        /// <param name="arguments">The arguments to be logged. These are passed to the generated LoggingEvent for handling.</param>
        void Log(LoggingEvent.Severity severity, params object[] arguments);


        /// <summary>
        /// This method should be invoked by IModuleHost when the module is first loading. It allows for a module to initialize or request handling of an IModuleEvent
        /// by a module whose loading priority is higher than its own. Attempting to request data from another module in a module's constructor will fail and requesting
        /// data from a module in this invocation may fail if the required module is not yet loaded.
        /// </summary>
        void OnLoading();


        /// <summary>
        /// This method should be invoked by IModuleHost when a module is loaded. Any cross-module activity should only occur when this method is invoked to allow for
        /// each module to initialize itself during the OnLoading method. Attempting to request that a module handles an event before now may fail if the reuired plugin
        /// is not yet completelyloaded.
        /// </summary>
        void OnLoaded();


        /// <summary>
        /// This method should be invoked by IModuleHost when all modules have been loaded. Any cross-module activity should only occur when this method is invoked to allow for
        /// each module to initialize itself during the OnLoading method. Attempting to request that a module handles an event before now may fail if the reuired plugin
        /// is not yet completelyloaded.
        /// </summary>
        void OnAllModulesLoaded();


        /// <summary>
        /// This method should be invoked by the IModuleHost just before a module is going to be unloaded and the host should attempt to delay any final application
        /// termination for a number of seconds to allow for any per-module finalization.
        /// </summary>
        void OnUnloading();


        /// <summary>
        /// This method should be invoked by the IModuleHost when all modules are being unloaded and you should do any final cleanup of resources here. It is not advised
        /// to try and access other modules inside this method.
        /// </summary>
        void OnUnloaded();
    }
}

/*
    The MIT License (MIT)

    Copyright (c) 2025 John Earnshaw, NetModules Foundation.
    Repository Url: https://github.com/netmodules/netmodules/

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
using System.Linq;
using NetModules.Interfaces;
using NetModules.Classes;
using NetModules.Events;

namespace NetModules
{
    /// <summary>
    /// A Module is an <see cref="IEventHandler"/> that can be instatiated by an <see cref="IModuleHost"/>.
    /// This class exposes members to the <see cref="ModuleHost"/> that identify both the IModule itself and what <see cref="IEvent"/>
    /// instances it can handle.
    /// </summary>
    [Serializable]
    [Module(Description = "This is an abstract module that can be inherited while creating a new Module Type.")]
    public abstract class Module : IModule
    {
        /// <summary>
        /// The <see cref="IModuleHost"/> is responsible for loading known <see cref="IEvent"/> and known instances inheriting from <see cref="Module"/>.
        /// If you wish to raise an <see cref="IEvent"/> for handling this should be done by invoking <see cref="IEventHandler.Handle(IEvent)"/> on
        /// <see cref="IModuleHost"/>. Although individual <see cref="Module"/> instances can be accessed via <see cref="IModuleHost.Modules"/>, it is
        /// not recommended to invoke <see cref="Module.Handle(IEvent)"/> directly as this will bypass internal processing and the <see cref="IEvent"/>
        /// instance will be processed invisibly. This is called a ghost event.
        /// </summary>
        public virtual IModuleHost Host { get; internal set; }


        /// <summary>
        /// This contains module metadata such as the module's name and public description. This metadata is visible to other modules that are loaded
        /// in the system and can be used for documentation and dependency checks.
        /// </summary>
        public virtual IModuleAttribute ModuleAttributes
        {
            get; internal set;
        }


        Uri path;

        /// <summary>
        /// Returns the system directory from where the module was loaded. This can be useful for loading additional resources
        /// or creating files such as logs and storing other data alongside the module.
        /// </summary>
        public virtual Uri WorkingDirectory
        {
            get
            {
                if (path == null)
                {
                    path = AssemblyTools.GetPathToAssembly(GetType());
                }

                return path;
            }
        }


        /// <summary>
        /// This property is set internally by <see cref="IModuleHost.Modules"/> once the module has been loaded and an instance
        /// of the module has been created. If the module if then unloaded this property will be set back to false. This property
        /// is checked before <see cref="IEventHandler.CanHandle(IEvent)"/> and <see cref="IEventHandler.Handle(IEvent)"/> are
        /// invoked on <see cref="Host"/>. Only loaded modules can handle events.
        /// </summary>
        public virtual bool Loaded
        {
            get; internal set;
        }


        /// <summary>
        /// CanHandle is invoked by <see cref="Host"/> to see if this <see cref="Module"/> instance is able to handle
        /// the requested <see cref="IEvent"/> object.
        /// </summary>
        /// <param name="e">The <see cref="IEvent"/>instance to to check if this <see cref="Module"/> instance can handle it.</param>
        /// <returns></returns>
        public abstract bool CanHandle(IEvent e);


        /// <summary>
        /// This method is invoked by <see cref="IModuleHost"/> passing an <see cref="IEvent"/> instance to this <see cref="Module"/>
        /// for further processing. Once the <see cref="IEvent.Handled"/> property is set to true and retrned, no further processing
        /// will occur on the <see cref="IEvent"/> instance by other modules that may be able to handle the <see cref="IEvent"/> type.
        /// </summary>
        /// <param name="e">The <see cref="IEvent"/> instance to be handled by this <see cref="Module"/>.</param>
        public abstract void Handle(IEvent e);


        /// <summary>
        /// Implemented due to demand. An object implementing <see cref="IModule"/> may often require the ability to retrieve configurable settings.
        /// This method is implemented in <see cref="Module"/> and acts as a wrapper for raising a <see cref="GetSettingEvent"/> into the
        /// <see cref="Host"/> for handling. If a <see cref="Module"/> is not loaded that can handle <see cref="GetSettingEvent"/> then this method
        /// will fail and attempt to log using <see cref="Module.Log(LoggingEvent.Severity, object[])"/>. Functionality can be overridden.
        /// </summary>
        /// <typeparam name="T">The type of the required setting.</typeparam>
        /// <param name="name">The identifier string for the required setting. This is passed to the generated <see cref="IEventInput"/>.</param>
        /// <param name="default">The default setting to return if a configured setting is not available or the returned setting is the wrong type.</param>
        public virtual T GetSetting<T>(string name, T @default = default) => GetSetting(name, @default, false);

        /// <summary>
        /// Implemented due to demand. An object implementing <see cref="IModule"/> may often require the ability to retrieve configurable settings.
        /// This method is implemented in <see cref="Module"/> and acts as a wrapper for raising a <see cref="GetSettingEvent"/> into the
        /// <see cref="Host"/> for handling. If a <see cref="Module"/> is not loaded that can handle <see cref="GetSettingEvent"/> then this method
        /// will fail and attempt to log using <see cref="Module.Log(LoggingEvent.Severity, object[])"/>. Functionality can be overridden.
        /// </summary>
        /// <typeparam name="T">The type of the required setting.</typeparam>
        /// <param name="name">The identifier string for the required setting. This is passed to the generated <see cref="IEventInput"/>.</param>
        /// <param name="default">The default setting to return if a configured setting is not available or the returned setting is the wrong type.</param>
        /// <param name="suppressLogMessage">Set to true if you do not wish to raise a <see cref="LoggingEvent"/> if the underlying <see cref="GetSettingEvent"/> is not marked as handled.</param>
        public virtual T GetSetting<T>(string name, T @default = default, bool suppressLogMessage = false)
        {
            /*
             * This overridable method acts is a wrapper for creating a GetSettingEvent and invoking IModuleHost.Handle on it. If no module exists to handle the
             * GetSettingEvent or if the return type is unexpected we do some logging using the IModule.Log method, see below...
             */

            var getSettingEvent = new GetSettingEvent();
            getSettingEvent.Input = new GetSettingEventInput
            {
                ModuleName = ModuleAttributes.Name,
                SettingName = name
            };
            Host.Handle(getSettingEvent);

            if (getSettingEvent.Handled)
            {
                var setting = getSettingEvent.Output.Setting;
                
                if (setting != null)
                {
                    // We check that the setting type is correct before returning it.
                    if (setting is T s)
                    {
                        return s;
                    }

                    // If the setting is not the requested type, we log a message suggesting the type format of the setting so that the developer can
                    // request the setting in the correct format and use casting or an alternative conversion method to format the setting as required.
                    // An example here is that if the setting is parsed into a dictionary using JSON, the serializer may have parsed a numeric value
                    // into an incorrect value type. Where a double may be required, the setting may have been parsed into a float. In this case, the
                    // float setting must be requested using this method and then cast to a double after the setting value is retrieved.
                    var type = typeof(T);

                    Log(LoggingEvent.Severity.Warning,
                        new InvalidCastException(
                            string.Format(Constants._SettingTypeMismatch, setting.GetType(), type)),
                        getSettingEvent);

                    return @default;
                }

                return @default;
            }

            // If the event is not handled, we raise a LoggingEvent to inform the developer that no module exists to handle the event for the requested setting.
            // This can be suppressed by passing the suppressLogMessage parameter as true, or by a handling module setting a boolean metadata value to true on the
            // GetSettingEvent for the key "suppressLogMessage". This is useful for modules that may not be able to handle the event but do not want to log an error.
            if (!suppressLogMessage || getSettingEvent.GetMeta(Constants._MetaSurpressLogMessage, false))
            {
                Log(LoggingEvent.Severity.Error
                    , string.Format(Constants._SettingNotFound, getSettingEvent.Name)
                    , getSettingEvent);
            }

            return @default;
        }


        /// <summary>
        /// Implemented due to demand. An <see cref="IModule"/> may often require the ability to log debug data, errors, analytics or other information.
        /// This method is implemented in <see cref="Module"/> and acts as a wrapper for raising a <see cref="LoggingEvent"/> to <see cref="IModuleHost"/>
        /// for handling. If a <see cref="Module"/> is not loaded that can handle the <see cref="LoggingEvent"/> then this method will fail silently. This
        /// Functionality can be overridden on a per-module basis.
        /// </summary>
        /// <param name="severity">The severity of logging required. This is passed to the generated LoggingEvent for handling.</param>
        /// <param name="arguments">The arguments to be logged. These are passed to the generated <see cref="IEventInput"/>.</param>
        public virtual void Log(LoggingEvent.Severity severity, params object[] arguments)
        {
            /*
             * This overridable method creates a LoggingEvent, populates its properties and passes it to IModuleHost for handling.
             * If a module does not exist to handle an IEvent of type LoggingEvent logging will fail silently. If we were to attempt
             * logging of a failed LoggingEvent we would create an infinite loop of logging fails... I hear StackOverflowException.
             */

            if (arguments != null && arguments.Length > 0)
            {
                // We insert the module name at index 0 of the arguments array so that it can be output by the LoggingEvent event handler.
                var loggingEvent = new LoggingEvent
                {
                    Input = new LoggingEventInput
                    {
                        Severity = severity,
                        Arguments = arguments.ToList()
                    }
                };

                loggingEvent.Input.Arguments.Insert(0, this.ModuleAttributes.Name);
                Host.Handle(loggingEvent);
            }
        }
        

        /// <summary>
        /// This method is invoked by <see cref="IModuleHost"/> when the module is loading. Attempting raise an <see cref="IEvent"/> from the constructor
        /// will fail and any <see cref="IEvent"/> shoud be raised here. Raising an <see cref="IEvent"/> here may fail if the required <see cref="Module"/>
        /// is not yet loaded to handle the <see cref="IEvent"/>.
        /// </summary>
        public virtual void OnLoading()
        {
            // Empty method is designed to be overriden if required.
        }


        /// <summary>
        /// This method is invoked by <see cref="IModuleHost"/> when the <see cref="Module"/> is loaded. Any cross-module activity such as raising an
        /// <see cref="IEvent"/> to be handled via <see cref="IModuleHost"/> can proceed successfully here provided an <see cref="IModule"/> is loaded
        /// to handle the <see cref="IEvent"/>.
        /// </summary>
        public virtual void OnLoaded()
        {
            // Empty method is designed to be overriden if required.
        }

        /// <summary>
        /// This method is invoked by <see cref="IModuleHost"/> when all instances of <see cref="Module"/> have been loaded. Any cross-module activity such as raising an
        /// <see cref="IEvent"/> to be handled via <see cref="IModuleHost"/> can proceed successfully here provided an <see cref="IModule"/> is loaded
        /// to handle the <see cref="IEvent"/>.
        /// </summary>
        public virtual void OnAllModulesLoaded()
        {
            // Empty method is designed to be overriden if required.
        }

        /// <summary>
        /// This method is invoked by <see cref="IModuleHost"/> before a <see cref="Module"/> is unloaded. Any per-module finalization and cleanup
        /// should be done here.
        /// </summary>
        public virtual void OnUnloading()
        {
            // Empty method is designed to be overriden if required.
        }


        /// <summary>
        /// This method is invoked by <see cref="IModuleHost"/> when the module is unloaded.
        /// </summary>
        public virtual void OnUnloaded()
        {
            // Empty method is designed to be overriden if required.
        }


        #region Overrides


        /// <summary>
        /// Returns the <see cref="ModuleAttribute.Name"/>
        /// </summary>
        public override string ToString()
        {
            return ModuleAttributes.Name;
        }


        /// <summary>
        /// Checks to see if the objects are equal. If the comparing object is a <see cref="Module"/> then <see cref="ModuleAttribute.Name"/>
        /// is used for comparison.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is Module module)
            {
                return module.ModuleAttributes.Name.Equals(ModuleAttributes.Name);
            }

            return base.Equals(obj);
        }


        /// <summary>
        /// Returns the <see cref="ModuleAttribute.Name"/> HashCode for comparison.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ModuleAttributes.Name.GetHashCode();
        }


        /// <summary>
        /// Should we be able to implicitly convert a <see cref="Module"/> to a <see cref="string"/>?
        /// </summary>
        public static implicit operator string(Module m)
        {
            return m.ModuleAttributes.Name.ToString();
        }

        #endregion
    }
}

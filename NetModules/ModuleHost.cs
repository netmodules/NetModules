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
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NetModules.Interfaces;
using NetModules.Classes;
using NetModules.Events;

namespace NetModules
{
    /// <summary>
    /// <see cref="ModuleHost"/> implements <see cref="IModuleHost"/> interface and is designed to be inherited in your own project.
    /// Functionality is included to load modules from assemblies located within the <see cref="ModuleHost.WorkingDirectory"/> and
    /// one level deep via <see cref="ModuleHost.Modules"/> <see cref="IModuleCollection.LoadModules(IList{ModuleName})"/>. It is 
    /// possible to implement your own <see cref="IModuleHost"/> or extend <see cref="ModuleHost"/> if required.
    /// </summary>
    public abstract class ModuleHost : IModuleHost
    {
        readonly ModuleCollection _ModuleCollection;
        readonly EventCollection _EventCollection;
        readonly Dictionary<string, IEvent> _EventsInProgress;
        readonly ReadOnlyDictionary<string, IEvent> _ReadOnlyEventsInProgress;

        /// <summary>
        /// Creates a new instance of <see cref="ModuleHost"/>. When using startup args in your application, you can pass these args
        /// through to <see cref="ModuleHost"/> so they can be inspected by any <see cref="Module"/> that may require them.
        /// </summary>
        /// <param name="args">Arguments to pass to modules.</param>
        public ModuleHost(string[] args = null)
        {
            if (args != null)
            {
                Arguments = args;
            }
            else
            {
                Arguments = new List<string>();
            }

            var eventCollection = new EventCollection(this);
            var moduleCollection = new ModuleCollection(this);

            // Create the events tracking list and a readonly wrapper to pass in the IModuleHost.EventsInProgress property.
            // We do this here since Host starts throwing a few LoggingEvent events into the system while importing and
            // loading, etc...
            _EventsInProgress = new Dictionary<string, IEvent>();
            _ReadOnlyEventsInProgress = new ReadOnlyDictionary<string, IEvent>(_EventsInProgress);

            eventCollection.ImportEvents();
            moduleCollection.ImportModules();

            _EventCollection = eventCollection;
            _ModuleCollection = moduleCollection;
        }


        /// <summary>
        /// Destroy.
        /// </summary>
        ~ModuleHost()
        {
            Modules.UnloadModules(null);
        }


        /// <summary>
        /// The <see cref="IModuleCollection"/> contains any descovered <see cref="Module"/> types and methods to invoke instances
        /// of loaded modules.
        /// </summary>
        public virtual IModuleCollection Modules
        {
            get
            {
                return _ModuleCollection;
            }
        }


        /// <summary>
        /// The <see cref="IEventCollection"/> contains all known <see cref="IEvent"/> types and contains methods that can instantiate
        /// them.
        /// </summary>
        public virtual IEventCollection Events
        {
            get
            {
                return _EventCollection;
            }
        }


        /// <summary>
        /// If any arguments are passed to the <see cref="ModuleHost"/> while invoking its constructor, these arguments will be added to
        /// this property so that <see cref="Module"/> instances can inspect them for directives.
        /// </summary>
        public virtual IList<string> Arguments { get; } = new List<string>();


        Uri _WorkingDirectory;

        /// <summary>
        /// Returns the system directory from where this <see cref="ModuleHost"/> instance is running. This is useful for loading other
        /// resources or creating files and writing data alongside the <see cref="ModuleHost"/> instance.
        /// </summary>
        public virtual Uri WorkingDirectory
        {
            get
            {
                if (_WorkingDirectory == null)
                {
                    _WorkingDirectory = AssemblyTools.GetPathToAssembly(GetType());
                }

                return _WorkingDirectory;
            }
        }

        string _ApplicationName;

        /// <summary>
        /// Returns the invoking application name via <see cref="System.Reflection"/>.GetCallingAssembly().
        /// </summary>
        public virtual string ApplicationName
        {
            get
            {
                if (_ApplicationName == null)
                {
                    _ApplicationName = System.Reflection.Assembly.GetCallingAssembly().GetName().Name;
                }

                return _ApplicationName;
            }
        }


        /// <summary>
        /// Any <see cref="IEvent"/> passed to <see cref="ModuleHost.Handle(IEvent)"/> will be added to this dictionary
        /// and removed once it has been processed. This property allows other <see cref="IModule"/> instances to monitor
        /// events within the system. Any ghost events will not be visible here. Ghos events are <see cref="IEvent"/>
        /// instances that are passed to <see cref="Module.Handle(IEvent)"/> directly. Ghost events are not recommended.
        /// </summary>
        public virtual IReadOnlyDictionary<string, IEvent> EventsInProgress {
            get
            {
                return _ReadOnlyEventsInProgress;
            }
        }


        /// <summary>
        /// <see cref="ModuleHost"/> implements <see cref="IEventHandler"/> and exposes its methods so that a <see cref="IModule"/>
        /// instance can check to see if an <see cref="IEvent"/> instance can be handled and pass it to <see cref="ModuleHost.Handle(IEvent)"/>
        /// for handling. It is recommended to always use <see cref="IModuleHost"/> to handle events rather than invoking
        /// <see cref="Module.Handle(IEvent)"/> directly. Invoking <see cref="Module.Handle(IEvent)"/> will create a ghost event
        /// that will be invisible to the current instance of <see cref="IModuleHost"/> and all other modules.
        /// </summary>
        public virtual bool CanHandle(IEvent e)
        {
            if (_ModuleCollection != null)
            {
                return _ModuleCollection.CanHandle(e);
            }

            return false;
        }


        /// <summary>
        /// <see cref="ModuleHost"/> implements <see cref="IEventHandler"/> and exposes its methods so that a <see cref="IModule"/>
        /// instance can check to see if an <see cref="IEvent"/> instance can be handled and pass it to <see cref="ModuleHost.Handle(IEvent)"/>
        /// for handling. It is recommended to always use <see cref="IModuleHost"/> to handle events rather than invoking
        /// <see cref="Module.Handle(IEvent)"/> directly. Invoking <see cref="Module.Handle(IEvent)"/> will create a ghost event
        /// that will be invisible to the current instance of <see cref="IModuleHost"/> and all other modules.
        /// </summary>
        public virtual async Task<bool> CanHandleAsync(IEvent e)
        {
            return await Task.Run(() => CanHandle(e));
        }


        /// <summary>
        /// <see cref="ModuleHost"/> implements <see cref="IEventHandler"/> and exposes its methods so that a <see cref="IModule"/>
        /// instance can check to see if an <see cref="IEvent"/> instance can be handled and pass it to <see cref="ModuleHost.Handle(IEvent)"/>
        /// for handling. It is recommended to always use <see cref="IModuleHost"/> to handle events rather than invoking
        /// <see cref="Module.Handle(IEvent)"/> directly. Invoking <see cref="Module.Handle(IEvent)"/> will create a ghost event
        /// that will be invisible to the <see cref="IModuleHost"/> and all other modules.
        /// </summary>
        public virtual void Handle(IEvent e)
        {
            // We don't want to do any trace logging here if the event type is LoggingEvent as this will
            // cause a LoggingEvent trace loop (StackOverflowException)...
            var isLoggingEvent = e is LoggingEvent;

            if (!isLoggingEvent)
            {
                Log(LoggingEvent.Severity.Trace
                    , Constants._ModuleHostEventReceived
                    , e is ISensitiveEvent ? e.Name : e);
            }

            // We generate a unique ID for the event and add it to the IEvent.Meta dictionary. This unique ID can be used to
            // Track and monitor the event during the handling process through the exposed EventsInProgress property.
            var id = GenerateEventId(e);
            e.SetMetaValue(Constants._MetaId, id);

            lock (_EventsInProgress)
            {
                _EventsInProgress.Add(id, e);

                if (!isLoggingEvent)
                {
                    Log(LoggingEvent.Severity.Trace
                    , string.Format(Constants._ModuleHostTotalEventsInStack
                    , _EventsInProgress.Count));
                }
            }

            // Added try/finally block to ensure that events that throw an exception are still removed from the EventsInProgress
            // list. A try block without the catch will bubble any exceptions thrown.
            try
            {
                // Pass the event over to the ModuleCollection for handling. This keeps things more readable in this class.
                if (_ModuleCollection != null)
                {
                    _ModuleCollection.Handle(e);
                }
            }
            finally
            {
                if (!isLoggingEvent)
                {
                    Log(LoggingEvent.Severity.Trace
                        , Constants._ModuleHostEventProcessed, string.Format(Constants._ModuleHostEventHandled, e is IUnhandledEvent ? "Unhandleable" : e.Handled)
                        , e is ISensitiveEvent ? e.Name : e);
                }

                // Once the event is completed we need to remove it from the EventsInProgress list.
                lock (_EventsInProgress)
                {
                    _EventsInProgress.Remove(id);

                    if (!isLoggingEvent)
                    {
                        Log(LoggingEvent.Severity.Trace
                        , string.Format(Constants._ModuleHostTotalEventsInStack
                        , _EventsInProgress.Count));
                    }
                }
            }
        }


        /// <summary>
        /// <see cref="ModuleHost"/> implements <see cref="IEventHandler"/> and exposes its methods so that a <see cref="IModule"/>
        /// instance can check to see if an <see cref="IEvent"/> instance can be handled and pass it to <see cref="ModuleHost.Handle(IEvent)"/>
        /// for handling. It is recommended to always use <see cref="IModuleHost"/> to handle events rather than invoking
        /// <see cref="Module.Handle(IEvent)"/> directly. Invoking <see cref="Module.Handle(IEvent)"/> will create a ghost event
        /// that will be invisible to the <see cref="IModuleHost"/> and all other modules.
        /// </summary>
        public virtual async Task<IEvent> HandleAsync(IEvent e, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                Handle(e);
                return e;
            }, cancellationToken);
        }


        /// <summary>
        /// Internal method for generating a unique event id.
        /// </summary>
        private string GenerateEventId(IEvent e)
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Log(LoggingEvent.Severity severity, params object[] arguments)
        {
            /*
             * This overridable method creates a LoggingEvent, populates its properties and passes it to the Handle method for handling.
             * If a module does not exist to handle an IEvent of type LoggingEvent logging will fail silently. If we were to attempt
             * logging of a failed LoggingEvent we would create an infinite loop of logging fails... I hear StackOverflowException.
             */

            if (arguments != null && arguments.Length > 0)
            {
                // We insert the application name at index 0 of the arguments array, and raise a LoggingEvent
                // so that it can processed by any LoggingEvent event handlers.
                var loggingEvent = new LoggingEvent
                {
                    Input = new LoggingEventInput
                    {
                        Severity = severity,
                        Arguments = arguments.ToList()
                    }
                };

                loggingEvent.Input.Arguments.Insert(0, ApplicationName);
                Handle(loggingEvent);
            }
        }
    }
}

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
using NetModules.Events;

namespace NetModules.Interfaces
{
    /// <summary>
    /// This interface should be inherited by any class or struct that loads <see cref="IModule"/> types.
    /// </summary>
    public interface IModuleHost : IEventHandler
    {
        /// <summary>
        /// The arguments that are passed to the executing application while it is launching. This allows instances implementing IModule
        /// to check for specific instructions that were sent to the application when it was executed.
        /// </summary>
        IList<string> Arguments { get; }


        /// <summary>
        /// The IModuleCollection keeps track of all imported/loaded instances of IModule and exposes methods to manipulate instances of
        /// IModule via the current IModuleHost.
        /// </summary>
        IModuleCollection Modules { get; }


        /// <summary>
        /// The IModuleCollection keeps track of all imported/loaded instances of IModule and exposes methods to manipulate instances of
        /// IModule via the current IModuleHost.
        /// </summary>
        IEventCollection Events { get; }


        /// <summary>
        /// This should return a working directory that may be accessed by a requesting instance implementing IModule. This would usually
        /// be the absolute path to the executing application, but could be an AppData style path and adds the ability to potentially
        /// sandbox.
        /// </summary>
        Uri WorkingDirectory { get; }


        /// <summary>
        /// This should expose the executing application's name and make it available to all hosted instances of IModule.
        /// </summary>
        string ApplicationName { get; }


        /// <summary>
        /// Implemented due to demand. An object implementing IModule may often require the ability to log debug data and analytics. This method is implemented in Module
        /// and acts as a wrapper for pushing a LoggingEvent into the IModuleHost for handling. If a module is not provided to handle the LoggingEvent then logging will
        /// fail. This Functionality can be overridden on a per-module basis.
        /// </summary>
        /// <param name="severity">The severity of logging required. This is passed to the generated LoggingEvent for handling.</param>
        /// <param name="args">The arguments to be logged. These are passed to the generated LoggingEvent for handling.</param>
        void Log(LoggingEvent.Severity severity, params object[] args);


        /// <summary>
        /// This should return a list of all events that currently being handled.
        /// </summary>
        IReadOnlyDictionary<string, IEvent> EventsInProgress { get; }
    }
}

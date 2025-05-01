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

using NetModules.Events;
using NetModules.Interfaces;

namespace NetModules.BasicConsoleLogging
{
    /// <summary>
    /// A basic console logging module. This module is used to test the console logging functionality of the NetModules.Events.LoggingEvent. It is not intended to be used in production.
    /// </summary>
    [Module(LoadFirst = true, Description = "A basic console logging module. This module is used to test the console logging functionality of the NetModules.Events.LoggingEvent. It is not intended to be used in production.")]
    public class BasicConsoleLoggingModule : Module
    {
        /*
         * This testing Module is designed to simply log any LoggingEvent that is raised by either
         * Module.Log(), Host.Log(), or by raising a LoggingEvent to Host.Handle() directly, to the
         * console as an example.
         */
        public override bool CanHandle(IEvent e)
        {
            return e is LoggingEvent;
        }


        /*
         * This is a simple demonstration of how a Module can be configured to handle an event.
         * You can define as many modules to handle the same Event and they will all be called in
         * series until the Event is marked as handled (IEvent.Handled).
         * 
         * A good example here is that you may require a Module to log to Console, a Module that
         * logs to a local file, or a database, and another Module that logs to a 3rd party service,
         * such as Loggly.
         * 
         * Or what if at a later date you'd like to swap out the Console logging for a file logging?
         * 
         * All you need to do is write a fresh handling Module for a LoggingEvent, and job done!
         * Future scalability proof!
         * 
         * Because a LoggingEvent inherits from UnhandledEvent, this event will automatically be
         * sent to every loaded Module that returns true for CanHandle(IEvent e) where e is a
         * LoggingEvent.
         * 
         * Hashtag: #FutureProof
         * Hashtag: #Scalability
         * Hashtag: #Simple!
         */
        public override void Handle(IEvent e)
        {
            if (e is LoggingEvent l)
            {
                Console.ForegroundColor = l.Input.Severity == LoggingEvent.Severity.Error
                    || l.Input.Severity == LoggingEvent.Severity.Warning
                    ? ConsoleColor.Red : ConsoleColor.Cyan;
                Console.WriteLine($"{this.ModuleAttributes.Name} is writing to Console:\n>[{l.Input.Severity}] {string.Join("\n>", l.Input.Arguments.Select(a => a.ToString()))}");
                Console.ResetColor();
            }
        }
    }
}

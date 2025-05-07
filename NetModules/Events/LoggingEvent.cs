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

namespace NetModules.Events
{
    /// <summary>
    /// This event is built in to the NetModules core library and requires a module to be imported and loaded
    /// that can handle this event type for any logging to occur. If no module is loaded to handle this event
    /// logging will fail silently.
    /// </summary>
    [Serializable]
    public class LoggingEvent : UnhandledEvent<LoggingEventInput, EmptyEventOutput>
    {
        /// <summary>
        /// The severity of the LoggingEvent.
        /// </summary>
        public enum Severity
        {
            /// <summary>
            /// Raise this severity to log at the most granular level and step-by-step execution of a
            /// module if required
            /// </summary>
            Trace = -1,
            /// <summary>
            /// Raise this severity to log detailed information about a module's functionality for debugging
            /// purposes.
            /// </summary>
            Debug = 0,
            /// <summary>
            /// Raise this severity to log data from a module that provides a record of normal operation.
            /// </summary>
            Information = 1,
            /// <summary>
            /// Raise this severity for significant conditions in a module that may require monitoring.
            /// </summary>
            Notice = 2,
            /// <summary>
            /// Raise this severity to signify potential issues in a module that may lead to errors or
            /// unexpected behavior in the future if not addressed.
            /// </summary>
            Warning = 3,
            /// <summary>
            /// Raise this severity to indicate error conditions that impair some operation in a module but
            /// are less severe than critical situations.
            /// </summary>
            Error = 4,
            /// <summary>
            /// Raise this severity to signify critical conditions in a module that demand intervention to
            /// prevent system failure.
            /// </summary>
            Critical = 5,
            /// <summary>
            /// Raise this severity to indicate that a module requires necessary action to resolve a
            /// critical issue.
            /// </summary>
            Alert = Critical,
            /// <summary>
            /// Raise this severity to indicate that a module is unusable and requires immediate attention.
            /// </summary>
            Emergency = Alert,
        }


        /// <inheritdoc/>
        public override EventName Name { get; } = "NetModules.Events.LoggingEvent";
    }
}

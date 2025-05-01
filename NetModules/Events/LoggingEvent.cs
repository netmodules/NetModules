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
            /// Log a Debug message.
            /// </summary>
            Debug,
            /// <summary>
            /// Log an Error message.
            /// </summary>
            Error,
            /// <summary>
            /// Log an Information message.
            /// </summary>
            Information,
            /// <summary>
            /// Log a Warning message.
            /// </summary>
            Warning
        }


        /// <inheritdoc/>
        public override EventName Name { get; } = "NetModules.Events.LoggingEvent";
    }
}

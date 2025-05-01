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

using NetModules.Attributes;
using NetModules.Events;
using NetModules.Interfaces;

namespace NetModules.TestCancellableEvent
{
    [Serializable]
    [EventHandler("NetModules.TestCancellableEvent.CancellableEventModule")]
    public class CancellableEventModuleEvent : CancellableEvent<EmptyEventInput, EmptyEventOutput>
    {
        /// <summary>
        /// Each <see cref="IEvent"/> that is loaded into <see cref="ModuleHost"/> should have a unique
        /// <see cref="EventName"/> that can be used to identify the event type where the concrete type of the
        /// <see cref="IEvent"/> object is unknown.
        /// </summary>
        public override EventName Name { get; } = "NetModules.CancellableEvent";
    }
}

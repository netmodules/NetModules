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
using System.Collections.Generic;

namespace NetModules.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventCollection
    {
        /// <summary>
        /// Return a instance of an Event object that implements <see cref="IEvent"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Solid instance of the relevant type based on the string.</returns>
        IEvent GetSolidEventFromType(Type type);


        /// <summary>
        /// Return a instance of an Event object that implements <see cref="IEvent{I, O}"/>.
        /// </summary>
        /// <returns>Solid instance of the relevant type based on the string.</returns>
        IEvent GetSolidEventFromType<T, I, O>() where T : IEvent<I, O> where I : IEventInput where O : IEventOutput;


        /// <summary>
        /// Return a instance of an Event object that implements <see cref="IEvent"/> using an
        /// <see cref="EventName"/> identifier.
        /// </summary>
        /// <param name="name">The name of the event to instantiate.</param>
        /// <returns>Solid instance of the relevant type based on the specified name of the event.</returns>
        IEvent GetSolidEventFromName(EventName name);


        /// <summary>
        /// Return a list of all Events known to the current instance of <see cref="IModuleHost"/>.
        /// </summary>
        IList<EventName> GetKnownEvents();


        /// <summary>
        /// Should return a list of all event types known to the current instance of <see cref="IModuleHost"/>.
        /// </summary>
        IList<Type> GetKnownEventTypes();


        /// <summary>
        /// Return a list of all Events assembly locations to the current instance of <see cref="IModuleHost"/>.
        /// </summary>
        IList<Uri> GetEventAssemblyLocations();
    }
}

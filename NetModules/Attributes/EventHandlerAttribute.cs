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
using NetModules.Interfaces;

namespace NetModules.Attributes
{
    /// <summary>
    /// Using this attribute you can assign one or more strict <see cref="IEventHandler"/>s by their <see cref="ModuleName"/>
    /// This allows an event to strictly define its handlers and prevent any other <see cref="IEventHandler"/> from handling it,
    /// such as if an <see cref="IEventHandler"/> can global handle any <see cref="IEvent"/>, it will not recieve this event for
    /// handling. This also applies to any <see cref="IEventPostHandler"/> and any <see cref="IEventPreHandler"/>.
    /// 
    /// Be aware that if this attribute is used, and a <see cref="IModule"/>'s <see cref="ModuleName"/> is not in this list,
    /// it will no longer be able to handle this event. If a <see cref="ModuleName"/>'s changes, this attribute on any <see cref="IEvent"/>
    /// will also require updating.
    /// </summary>
    [Serializable, AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EventHandlerAttribute : Attribute, IEventHandlerAttribute
    {
        /// <summary>
        /// Contains an array of <see cref="string"/>s that define <see cref="ModuleName"/>s that are strictly allowed to handle this <see cref="IEvent"/>.
        /// Be aware that if this attribute is used, and a <see cref="IModule"/>'s <see cref="ModuleName"/> is not in this list,
        /// it will not be able to handle this event. If a <see cref="ModuleName"/>'s changes, this attribute on any <see cref="IEvent"/>
        /// will also require updating.
        /// </summary>
        public string[] ModuleNames { get; private set; }


        /// <summary>
        /// Using this attribute you can assign one or more strict <see cref="IEventHandler"/>s by their <see cref="ModuleName"/>
        /// This allows an event to strictly define its handlers and prevent any other <see cref="IEventHandler"/> from handling it,
        /// such as if an <see cref="IEventHandler"/> can global handle any <see cref="IEvent"/>, it will not recieve this event for
        /// handling. This also applies to any <see cref="IEventPostHandler"/> and any <see cref="IEventPreHandler"/>.
        /// 
        /// Be aware that if this attribute is used, and a <see cref="IModule"/>'s <see cref="ModuleName"/> is not in this list,
        /// it will no longer be able to handle this event. If a <see cref="ModuleName"/>'s changes, this attribute on any <see cref="IEvent"/>
        /// will also require updating.
        /// </summary>
        public EventHandlerAttribute(params string[] moduleNames)
        {
            ModuleNames = moduleNames;
        }
    }
}

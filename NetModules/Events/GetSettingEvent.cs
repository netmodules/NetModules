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
using System.Text;
using System.Collections.Generic;
using NetModules.Interfaces;

namespace NetModules.Events
{
    /// <summary>
    /// This event is built in to the NetModules core library and requires a module to be imported and loaded
    /// that can handle this event type for a setting to be returned.
    /// </summary>
    [Serializable]
    public class GetSettingEvent : Event<GetSettingEventInput, GetSettingEventOutput>
    {
        /// <inheritdoc/>
        public override EventName Name { get; } = "NetModules.Events.GetSettingEvent";
    }

}

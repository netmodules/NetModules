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

namespace NetModules
{
    /// <summary>
    /// This abstract event is built in to the NetModules core library and requires inheriting. This class
    /// is a wrapper for <see cref="IEvent{I, O}"/> that speeds up implementation when using NetModule's
    /// default architecture. UnhandledEvent will ignore the setter for <see cref="Handled"/>. This is by
    /// design so that an UnhandledEvent will always continue any Module that is assigned to handle it.
    /// </summary>
    [Serializable]
    public abstract class UnhandledEvent<I, O> : Event<I, O> where I : IEventInput where O : IEventOutput
    {
        /// <inheritdoc/>
        public override EventName Name { get; }


        /// <summary>
        /// While the Handled property of an instance of IEvent has a getter and a setter, UnhandledEvent will
        /// ignore the setter and will always return false for the Handled property. This is by design so that
        /// an UnhandledEvent will always continue any Module that is registered to handle it.
        /// </summary>
        public override bool Handled { get { return false; } set { } }
    }

}

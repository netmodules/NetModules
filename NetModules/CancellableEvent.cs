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
using System.Threading;
using NetModules.Interfaces;

namespace NetModules
{
    /// <summary>
    /// This abstract event is built in to the NetModules core library and requires inheriting. This class
    /// is a wrapper for <see cref="Event{I, O}"/> that exposes an additional option to set a <see cref="System.Threading.CancellationToken"/>
    /// that can be monitored for cancellation notification requests. This class can be used to speed up implementation
    /// when using NetModule's default architecture.
    /// </summary>
    [Serializable]
    public abstract class CancellableEvent<I, O> : Event<I, O>, ICancellable where I : IEventInput where O : IEventOutput
    {
        /// <inheritdoc/>
        public CancellationToken CancellationToken { get; private set; }

        /// <inheritdoc/>
        public void SetCancelToken(CancellationToken cancellationToken)
        {
            CancellationToken = cancellationToken;
        }
    }
}

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

using System.Threading;
using System.Threading.Tasks;

namespace NetModules.Interfaces
{
    /// <summary>
    /// An instance that implements IEventHandler can handle objects that implement IEvent.
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// CanHandle can be invoked by the <see cref="IEventHandler"/> to see if this handler is able to handle the requested event. This
        /// allows the handler to inspect the IEvent and inform the requester if it can be handled without further processing.
        /// </summary>
        /// <param name="e">The <see cref="IEvent"/> to inspect for handling.</param>
        /// <returns>True if the <see cref="IEventHandler"/> can handle this <see cref="IEvent"/>.</returns>
        bool CanHandle(IEvent e);


        /// <summary>
        /// Pass an <see cref="IEvent"/> to this <see cref="IEventHandler"/> for it to be processed. All EventHandlers that wish to handle an
        /// instance of <see cref="IEvent"/> should handle the <see cref="IEvent"/> within this method.
        /// </summary>
        /// <param name="e">The <see cref="IEvent"/> to be handled.</param>
        void Handle(IEvent e);
    }
}

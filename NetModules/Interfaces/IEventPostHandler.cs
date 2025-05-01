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

namespace NetModules.Interfaces
{
    /// <summary>
    /// An interface that allows you to inspect an instance of IEvent after it has been processed. It is not advised to modify
    /// any of the IEvents properties at this point as <see cref="IEventPostHandler.OnHandled(IEvent)"/> may be invoked using
    /// multi-threading.
    /// </summary>
    public interface IEventPostHandler<T> : IEventPostHandler
        where T : IEvent
    {
        ///// <summary>
        ///// This should be invoked by IModuleHost after its implementing IEventHandler handles an IEvent.
        ///// </summary>
        ///// <param name="e"></param>
        //void OnHandled(T e);
    }

    /// <summary>
    /// This interface should be used with a generic type argument that tells the module system what type of event you wish to monitor
    /// for pre-handle. Implementing this interface will not function correctly without a generic identifier.
    /// </summary>
    public interface IEventPostHandler
    {
        /// <summary>
        /// This should be invoked by IModuleHost after its implementing IEventHandler handles an IEvent.
        /// </summary>
        /// <param name="e"></param>
        void OnHandled(IEvent e);
    }
}

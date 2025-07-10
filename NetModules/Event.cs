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
using NetModules.Interfaces;

namespace NetModules
{
    /// <summary>
    /// This abstract event is built in to the NetModules core library and requires inheriting. This class
    /// is a wrapper for <see cref="IEvent{I, O}"/> that speeds up implementation when using NetModule's
    /// default architecture.
    /// </summary>
    [Serializable]
    public abstract class Event<I, O> : IEvent<I, O> where I : IEventInput where O : IEventOutput
    {
        /// <summary>
        /// Protecting existing <see cref="Meta"/> from being replaced, this field is used to store the
        /// reference to an existing meta dictionary.
        /// </summary>
        Dictionary<string, object> _Meta;


        /// <inheritdoc/>
        public virtual I Input { get; set; }


        /// <inheritdoc/>
        public virtual O Output { get; set; }


        /// <inheritdoc/>
        public abstract EventName Name { get; }


        /// <inheritdoc/>
        public Dictionary<string, object> Meta
        {
            get
            {
                if (_Meta == null)
                {
                    _Meta = new Dictionary<string, object>();
                }

                return _Meta;
            }
            set
            {
                if (_Meta == null)
                {
                    _Meta = value;
                }
                else
                {
                    foreach (var kvp in value)
                    {
                        _Meta.TryAdd(kvp.Key, kvp.Value);
                    }
                }
            }
        }

        
        /// <inheritdoc/>
        public virtual bool Handled { get; set; }


        /// <inheritdoc/>
        public virtual IEventInput GetEventInput()
        {
            return Input;
        }


        /// <inheritdoc/>
        public virtual IEventOutput GetEventOutput()
        {
            return Output;
        }


        /// <inheritdoc/>
        public virtual void SetEventOutput(IEventOutput output)
        {
            if (output is O o)
            {
                Output = o;
            }
        }
    }

}

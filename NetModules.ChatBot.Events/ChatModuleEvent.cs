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

namespace NetModules.ChatBot.Events
{
    [Serializable]
    public class ChatModuleEvent : IEvent<ChatModuleEventInput, ChatModuleEventOutput>
    {
        /// <summary>
        /// Each <see cref="IEvent"/> that is loaded into <see cref="ModuleHost"/> should have a unique
        /// <see cref="EventName"/> that can be used to identify the event type where the concrete type of the
        /// <see cref="IEvent"/> object is unknown.
        /// </summary>
        public EventName Name { get; } = "NetModules.ChatBot.ChatModuleEvent";


        /// <summary>
        /// The Meta dictionary can be used to hold and transfer any generic or event specific data between modules
        /// and return generic information to the requester.
        /// </summary>
        public Dictionary<string, object> Meta { get; set; }


        /// <summary>
        /// This property should return true only if the <see cref="IEvent"/> was completed. This must be set by the
        /// <see cref="IModule"/> that is handling the <see cref="IEvent"/>
        /// </summary>
        public bool Handled { get; set; }


        /// <summary>
        /// The Input property must inherit <see cref="IEventInput"/> and acts as a placeholder for any properties, fields or other data
        /// that can be passed to an <see cref="IEventHandler"/> as arguments.
        /// </summary>
        public ChatModuleEventInput Input { get; set; }


        /// <summary>
        /// The Output object inherits from <see cref="IEventOutput"/> and is used for returning any properties, fields or other data while
        /// an <see cref="IEvent"/> is being handled by an <see cref="IEventHandler"/>.
        /// </summary>
        public ChatModuleEventOutput Output { get; set; }


        /// <summary>
        /// It has been required by modules that are designed to handle generic type of IEvent and need access to IModuleEvent.Input when the generic
        /// type definition of the IEvent{} may be unknown at runtime and strict casting is unavailable. We must expose Input and Output objects via
        /// non-generic IEvent interface.
        /// </summary>
        public IEventInput GetEventInput()
        {
            return Input;
        }


        /// <summary>
        /// It has been required by modules that are designed to handle generic type of IEvent and need access to IModuleEvent.Output when the generic
        /// type definition of the IEvent{} may be unknown at runtime and strict casting is unavailable. We must expose Input and Output objects via
        /// non-generic IEvent interface.
        /// </summary>
        public IEventOutput GetEventOutput()
        {
            return Output;
        }


        /// <summary>
        /// It has been required by modules that are designed to handle generic type of IEvent and need access to set IModuleEvent.Output when the generic
        /// type definition of IEvent{} may be unknown at runtime and strict casting is unavailable. We must expose a method to set Output object via the
        /// non-generic IEvent interface.
        /// </summary>
        public void SetEventOutput(IEventOutput output)
        {
            if (output is ChatModuleEventOutput o)
            {
                Output = o;
            }
        }
    }
}

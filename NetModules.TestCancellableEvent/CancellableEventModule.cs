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

using NetModules.Interfaces;

namespace NetModules.TestCancellableEvent
{
    [Serializable]
    [Module]
    public class CancellableEventModule : Module, IEventPostHandler<CancellableEventModuleEvent>, IEventPreHandler<CancellableEventModuleEvent>
    {
        /*
         * This example module shows the most basic usage of a module or plugin. This is in the form of a late 1900s style chatbot so
         * don't expect anything special! If you want a really clever chatbot then use NLP, Tensorflow and GPT-2 and code it yourself.
         * Remember, this is just an example.
         */

        /// <summary>
        /// CanHandle is an abstract method and must be implemented.
        /// This method must return true for any event type this module wishes to handle.
        /// </summary>
        /// <param name="e">typeof(IEvent)</param>
        /// <returns></returns>
        public override bool CanHandle(IEvent e)
        {
            /*
             * CanHandle is invoked on a per module level by the IModuleHost to check if a module
             * exists that is able to handle a specific event type. You must return true for any
             * event type you wish to handle. ModuleHost selects modules to handle an event type
             * based on the return value of this method.
             */
            return e is CancellableEventModuleEvent;
        }


        /// <summary>
        /// Handle is an abstract method and must be implemented.
        /// This method should handle incomming event types that this module has returned true for in CanHandle.
        /// </summary>
        /// <param name="e">the instance typeof(<see cref="IEvent"/>) to be handled.</param>
        public override void Handle(IEvent e)
        {
            /*
             * Handle is invoked by the IModuleHost when an incoming event type can be handled by a module.
             * The event is passed to the module for handling. A module must specify what event types it can handle by
             * returning true for the event type in the CanHandle method. See the above method.
             */
            if (e is CancellableEventModuleEvent @event)
            {
                try
                {
                    while (true)
                    {
                        @event.CancellationToken.ThrowIfCancellationRequested();
                        Thread.Sleep(100);
                    }

                    @event.SetMeta("message", $"The event was completed.");
                    @event.Handled = true;
                    return;
                }
                catch (Exception ex)
                {
                    @event.SetMeta("message", $"The event was not completed: {ex.Message}");
                }
            }
        }


        /*
         * A Module that implements IEventPreHandler<> will receive the event before it is handled. This along with
         * IEventPostHandler<> are useful for monitoring or manipulating or inspecting events before and after they
         * have been processed.
         */
        public void OnBeforeHandle(IEvent e)
        {
            Console.WriteLine("...");
        }


        /*
         * A Module that implements IEventPostHandler<> will receive the event to this method after it has been handled
         * by Host.Handle method. This along with IEventPreHandler<> are useful for monitoring or manipulating or inspecting
         * events before and after they have been processed.
         */
        public void OnHandled(IEvent e)
        {
            Console.WriteLine("...");
        }

        public override void OnLoading()
        {
            Console.WriteLine($"{ModuleAttributes.Name} Loading...");
        }

        public override void OnLoaded()
        {
            Console.WriteLine($"{ModuleAttributes.Name} Loaded.");
        }

        public override void OnUnloading()
        {
            Console.WriteLine($"{ModuleAttributes.Name} Unloading...");
        }

        public override void OnUnloaded()
        {
            Console.WriteLine($"{ModuleAttributes.Name} Unloaded.");
        }
    }
}

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
using NetModules.ChatBot.Events;
using System.Collections.Generic;
using System.Linq;

namespace NetModules.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            //AppDomain.CurrentDomain.TypeResolve += CurrentDomain_TypeResolve;
            Launch(args);
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }

        private static System.Reflection.Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }

        static void Launch(string[] args)
        {
            
            // Create a new module host
            ModuleHost host = new BasicModuleHost();

            // Invoking ModuleHost.Modules.GetModuleNames method tells us what modules have been imported. We
            // are calling this method for information only. Modules have not been loaded yet.
            var names = host.Modules.GetModuleNames();

            // Now we load modules. Modules must be loaded otherwise they can not handle events.
            host.Modules.LoadModules();

            // Importing module happens by default when the default ModuleHost is initialized but you can call
            // ImportModules any time and any newly added modules will be loaded.
            host.Modules.ImportModules();

            var modulesList = host.Modules.GetModulesByType<IModule>();

            if (!host.Modules.GetLoadedModules().Select(m => m.ModuleAttributes.Name).Contains("NetModules.ChatBot.ChatModule"))
            {
                host.Modules.LoadModule("NetModules.ChatBot.ChatModule");
            }

            var chatBotModule = host.Modules.GetModulesByType<ChatBot.ChatModule>().FirstOrDefault();


            // Writing console lines here has nothing to do with the functionality of NetModules
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("...");
            Console.WriteLine("...");

            Console.WriteLine("Hello World! I'm a 1980's chatbot, {0}", Chat.GetOpener());

            Console.WriteLine("...");
            Console.WriteLine("...");
            Console.WriteLine();
            Console.WriteLine();

            // Purely for testing to ensure we have an event.
            var testSolidEvent = host.Events.GetSolidEventFromName("NetModules.ChatBot.ChatModuleEvent");
            
            if (testSolidEvent == null)
            {
                throw new Exception("We have no chat event loaded.");
            }

            while (true)
            {
                // Waiting for user input...
                var request = Console.ReadLine();

                if (request.Equals("unload modules", StringComparison.OrdinalIgnoreCase))
                {
                    host.Modules.UnloadModules();
                    continue;
                }

                if (request.Equals("load modules", StringComparison.OrdinalIgnoreCase))
                {
                    host.Modules.LoadModules();
                    continue;
                }

                if (request.Equals("gc.collect()", StringComparison.OrdinalIgnoreCase))
                {
                    GC.Collect();
                    continue;
                }


                var e = new ChatModuleEvent();

                // We created a new chat event and added the input text to the IEvent.Input object.
                e.Input = new ChatModuleEventInput()
                {
                    Request = request
                };

                // We don't really need to call CanHandle but currently used good for debugging.
                if (host.CanHandle(e))
                {
                    // Here's the magic...
                    host.Handle(e);
                }
                else
                {
                    Console.WriteLine("Unable to handle the event. No handling module loaded...");
                }

                // Testing to see if we can get the input and output from the IEvent object... 
                var input = e.GetEventInput();
                var output = e.GetEventOutput();
                e.SetEventOutput(output);

                // Was our event hndled in the call to host.method Handle above?
                if (e.Handled /*&& e.Output != null*/)
                {
                    // Yes, so write out the response to the console...
                    Console.WriteLine(e.Output.Response);
                }
                else
                {
                    Console.WriteLine("The event was not handled, unable to respond...");
                }
                
                // More random and not required console writing.
                Console.WriteLine("...");
                Console.WriteLine("...");
            }
        }
    }
}

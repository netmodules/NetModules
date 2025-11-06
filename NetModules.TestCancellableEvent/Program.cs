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

using NetModules;
using NetModules.Interfaces;
using NetModules.TestCancellableEvent;


// Create a new module host
var host = new BasicModuleHost();

// Invoking ModuleHost.Modules.GetModuleNames method tells us what modules have been imported. We
// are calling this method for information only. Modules have not been loaded yet.
var names = host.Modules.GetModuleNames();

// Now we load modules. Modules must be loaded otherwise they can not handle events.
host.Modules.LoadModules();

// Importing module happens by default when the default ModuleHost is initialized but you can call
// ImportModules any time and any newly added modules will be loaded.
host.Modules.ImportModules();

var modulesList = host.Modules.GetModulesByType<IModule>();
var cancellableEventModuleModule = host.Modules.GetModulesByType<CancellableEventModule>()[0];

var cancellationSource = new CancellationTokenSource();
cancellationSource.CancelAfter(5000);

var @event = new CancellableEventModuleEvent();
@event.SetCancelToken(cancellationSource.Token);


host.TryHandle<CancellableEventModuleEvent>(@event, out _);

while(!@event.HasMeta("message"))
{

}

Console.WriteLine(@event.GetMetaValue("message", "The event was processed with no output message."));
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

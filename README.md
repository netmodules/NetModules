# NetModules #

In a one-liner, the aim of the NetModules project is to make developing customizable applications as simple as possible with all the hard work done by the NetModules architecture.

This project is open source so please feel free to contribute suggestions, make modifications and pull requests back to the repository.
___

### What and why is NetModules? ###

NetModules is an [MIT license](https://tldrlegal.com/license/mit-license) .NET 6 C# Class Library that offers an open-source system for creating [event-driven](https://en.wikipedia.org/wiki/Event-driven_architecture), modular and [plugin-based](https://en.wikipedia.org/wiki/Plug-in_(computing)) applications. 

This project was started as a simple example project that demonstrated an implementation of [Managed Extensibility Framework (MEF)](https://msdn.microsoft.com/en-us/magazine/ee291628.aspx). A framework included in Microsoft .NET Framework 4.0 and is also available as an external component at [codeplex](https://mef.codeplex.com/) for older versions of .NET Framework.

It was to demonstrate the architecture and benifits of a strict modular design pattern for developing complex applications.

Although the project was originally built with MEF, not many elements of it were are actually used. MEF was used to load the modules and privately inject the ModuleHost into the Host property of each Module, instead of rolling our own Reflection based loader. Since porting the project to .NET Core, removing MEF references in favor of Reflection was required. (THIS PROJECT NO LONGER DEPENDS ON MICROSOFT EXTENSIBILITY FRAMEWORK).

The project has since grown into an advanced framework designed to simplify the development of cross-platform applications using a modular or plugin-based design pattern. It is compatible with .NET 6 and platforms that support .NET 6 class libraries. We now use this framework as the groundwork for all client projects.

* NetModules is a simple, clean and compact .NET 6 class library for creating plugin-based, modular and customizable applications.
___

### How do I get set up? ###

Take a look at the [NetModules.ChatModule](https://github.com/johnearnshaw/NetModules/tree/master/NetModules.ChatBot) project in the source code. The ChatModule demonstrates the implementation of a Module in the form of a late 1900s style chatbot. This module shows how to handle a [NetModules.ChatModule.Events.ChatModuleEvent](https://github.com/johnearnshaw/NetModules/tree/master/NetModules.ChatBot.Events) that implements the [IEvent](https://github.com/johnearnshaw/NetModules/blob/master/NetModules/Interfaces/IEvent.cs) interface.

The [NetModules.ChatModule](https://github.com/johnearnshaw/NetModules/tree/master/NetModules.ChatBot) and [NetModules.ChatModule.Events](https://github.com/johnearnshaw/NetModules/tree/master/NetModules.ChatBot.Events) class libraries are referenced in the demo project located at [NetModules.TestApplication](https://github.com/johnearnshaw/NetModules/tree/master/NetModules.TestApplication), that is a console application. If you run this application without any modification, a console window will be displayed in that you can chat with the ChatBot module by entering text and pressing the return key.

The NetModules.TestApplication contains a [BasicModuleHost](https://github.com/johnearnshaw/NetModules/tree/master/NetModules.TestApplication/Classes) class that inherits from [NetModules.ModuleHost](https://github.com/johnearnshaw/NetModules/blob/master/NetModules/ModuleHost.cs). This class implements the [NetModules.Interfaces.IModuleHost](https://github.com/johnearnshaw/NetModules/blob/master/NetModules/Interfaces/IModuleHost.cs) interface. The ModuleHost class and implemented interfaces are used for loading modules and invoking the handling of events. There are no other dependency requirements in this project to keep it as simple as possible.

The layout of the projects within the NetModules directory and solution is to demonstrate the design pattern of keeping modules and corresponding events is seperate class libraries, and then referencing the class libraries from within an application that depends on them.
  
The core project and examples are well documented and if you get stuck or have any questions, please contact me and I'll be glad to help out.

For further documentation please see the [repository wiki](https://github.com/johnearnshaw/NetModules/wiki).
___

### Contribution guidelines ###

* Fork [NetModules](https://github.com/johnearnshaw/NetModules), make some changes, make a pull request. Simple!
* Code will be reviewed when a pull request is made.
___

### Who do I talk to? ###

* NetModules repo owner via message or the [issues board](https://github.com/johnearnshaw/NetModules/issues).
___

### License ###

* [The MIT License (MIT)](https://tldrlegal.com/license/mit-license) - You are free to use NetModules in commercial projects and modify/redistribute the source code provided the copyright notice is not removed.
* If you use NetModules in your own project I would love to hear about it, so drop me a line (and even a credit to NetModules in your project if you feel like being really generous). I would be very happy to hear about your experiences using my NetModules class library in your projects and any suggestions you may have for me to make it better.

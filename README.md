# NetModules: A Scalable, Abstract Event-Driven Modular Framework

NetModules is an open-source architecture designed for constructing scalable, abstract, event-driven modular systems. Initially conceived as a basic demonstration of the Managed Extensibility Framework (MEF), it has since evolved beyond MEF to provide a robust and flexible architecture for developing cross-platform, plugin-based applications.

## Why NetModules?

Modern applications demand adaptability and modularity, and NetModules delivers precisely that by enforcing a strict event-handling paradigm. It allows developers to design highly scalable systems where:

- **Event**: Strictly adhere to a predefined interface.
- **Module**: Declare the capability to handle specific Event types.
- **ModuleHost**: Serves as intermediary, ensuring efficient communication and delegation of event-handling responsibilities.

### Deferred Responsibility & Scalability

This architecture simplifies application scaling and future-proofing—modifications only require changes within the event-handling modules rather than a complete system overhaul.

#### Example: Logging System Upgrade

Imagine your application currently logs data to a local file system. To migrate to an external logging service, instead of modifying every Module that logs data, you simply build or update a dedicated logging **Module**. Existing modules continue logging events as usual, unaware of the underlying change.

This follows the principle of **deferred responsibility** a Module raises an Event requesting data or a function call, but it does not need to handle the request itself or understand how it is processed. Instead, another Module, designed to handle that Event type, receives and processes the request, ensuring flexibility and separation of concerns.

#### Example: Data Requests Between Modules

A Module may trigger an Event requesting certain data. This Module does **not** need to know where the data comes from or how it is processed—its only concern is receiving the required response. Another Module is responsible for handling the request and determining how and where the data is fetched or computed.

## Key Features

- **Abstract Event Handling**: Events conform to an interface, ensuring consistency and interoperability.
- **Modular Design**: Modules register their event-handling capabilities dynamically.
- **Scalability & Future Development**: Easily replace or modify event-handling modules without affecting the broader system.
- **Cross-Platform Compatibility**: Designed to function across any .NET-supported platform.

## Getting Started

Explore the [NetModules.ChatModule](https://github.com/netmodules/netmodules/tree/master/NetModules.ChatBot), which showcases the framework's modular approach through a chatbot implementation. The example demonstrates Event handling using the [NetModules.ChatModule.Events.ChatModuleEvent](https://github.com/netmodules/netmodules/tree/master/NetModules.ChatBot.Events) and the [IEvent](https://github.com/netmodules/netmodules/blob/master/netmodules/Interfaces/IEvent.cs) interface.

There are other examples in the repository, including a simle Console logging Module [(BasicConsoleLoggingModule)](https://github.com/netmodules/netmodules/blob/main/NetModules.BasicConsoleLogging/BasicConsoleLoggingModule.cs) that handles the internal [LoggingEvent](https://github.com/netmodules/netmodules/blob/main/NetModules/Events/LoggingEvent.cs) and outputs to Console, and a basic Module that demonstrates how to work with the [CancellableEvent](https://github.com/netmodules/netmodules/blob/main/NetModules/CancellableEvent.cs).

For more detailed guidance and examples, visit our [repository wiki](https://github.com/netmodules/netmodules/wiki) or our [website](https://netmodules.net/) (coming soon).

## Contributing

We welcome contributions! To get involved:
1. Fork [NetModules](https://github.com/netmodules/NetModules), make improvements, and submit a pull request.
2. Code will be reviewed upon submission.
3. Join discussions via the [issues board](https://github.com/netmodules/netmodules/issues).

## License

NetModules is licensed under the [MIT License](https://tldrlegal.com/license/mit-license), allowing unrestricted use, modification, and distribution. If you use NetModules in your own project, we’d love to hear about your experience, and even featur you on our website!

[NetModules Foundation](https://netmodules.net/)
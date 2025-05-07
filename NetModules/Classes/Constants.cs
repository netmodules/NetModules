using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModules.Classes
{
    internal class Constants
    {
        internal const string _NameValueEmpty = "The name value can not be a null or empty string.";

        internal const string _TypeManagerNoModuleDecoration = "{0} Module must be decorated with a ModuleAttribute.";
        internal const string _TypeManagerNoIEventImplementation = "{0} must implement generic interface type of IEvent<IEventInput, IEventOutput>.";
        internal const string _TypeManagerInvalidPath = "Invalid starting path specified (does not exist): {0}";
        internal const string _TypeManagerUnableToIterateAssembly = "Unable to iterate for types in assembly, the assembly was not loaded. The main cause of this is that the assembly is not a valid .NET assembly but may be the result of an underlying exception.";
        internal const string _TypeManagerNoEventInputOutput = "A type implementing IEvent interface must use generic arguments IEvent<IEventInput, IEventOutput> to identify the event input and output types.";
        internal const string _TypeManagerNoEventConstructor = "A type implementing IEvent<IEventInput, IEventOutput> interface must contain a public parameterless constructor.";

        internal const string _EventsAlreadyImported = "Events already imported. Importing events multiple times will cause multiple instances of all known events.";
        internal const string _EventsImporting = "Importing {0} events...";
        internal const string _EventImportError = "An error occurred while attempting to import an event.";
        internal const string _EventInstantiateError = "An error occurred while attempting to instantiate an event.";
        internal const string _EventsImported = "Events imported.";

        internal const string _ModuleNotLoaded = "Module is not initialized or modules to load does not contain this module's name. Skipping {0}";
        internal const string _ModulesImporting = "Importing {0} modules...";
        internal const string _ModulesImported = "Modules imported.";
        internal const string _ModuleInitializing = "Initializing module...";
        internal const string _ModuleDeinitializing = "Denitializing module...";
        internal const string _ModuleImportError = "An error occurred while attempting to import a module.";
        internal const string _ModuleMissingDependencies = "Module has missing dependencies.";
        internal const string _ModuleLoadingError = "An error occurred while attempting to load a module.";
        internal const string _ModuleInstantiateError = "An error occurred while attempting to instantiate a module.";
        internal const string _ModuleLoading = "Module loading...";
        internal const string _ModuleLoaded = "Module loaded.";
        internal const string _ModulesLoaded = "Modules loaded.";
        internal const string _ModuleUnloading = "Module unloading...";
        internal const string _ModuleUnloaded = "Module unloaded.";
        internal const string _ModuleUnloadingError = "An error occurred while attempting to unload a module.";
        internal const string _ModuleRaisingAllLoaded = "Raising all modules loaded to module...";

        internal const string _MetaId = "id";
        internal const string _MetaHandlers = "handlers";
        internal const string _MetaSurpressLogMessage = "suppressLogMessage";

        internal const string _SettingTypeMismatch = "The object returned in GetSettingEvent.Output.Setting is {0}. Expected return type is {1}.";
        internal const string _SettingNotFound = "No module exists to handle event type of {0} or the handling module is not marking the event as handled.";

    }
}

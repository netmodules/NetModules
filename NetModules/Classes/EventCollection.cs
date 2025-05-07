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
using System.Linq;
using System.Collections.Generic;
using NetModules.Interfaces;
using NetModules.Events;

namespace NetModules.Classes
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class EventCollection : List<Type>, IEventCollection
    {
        internal IModuleHost Host { get; set; }
        private bool Imported;

        IDictionary<Type, IEvent> ImportedEvents;
        IList<Uri> EventPaths = new List<Uri>();

        internal EventCollection(IModuleHost host)
        {
            Host = host;
            ImportedEvents = new Dictionary<Type, IEvent>();
        }


        internal void ImportEvents()
        {
            if (Imported)
            {
                Host.Log(LoggingEvent.Severity.Error, Constants._EventsAlreadyImported);

                if (System.Diagnostics.Debugger.IsAttached)
                {
                    throw new Exception(Constants._EventsAlreadyImported);
                }

                return;
            }

            var events = TypeManager.FindEvents<IEvent>(Host, Host.WorkingDirectory, 1);

            Host.Log(LoggingEvent.Severity.Debug, string.Format(Constants._EventsImporting, events.Count));

            EventPaths = events.Select(e => new Uri(e.Assembly.Location)).Distinct().ToList();

            AddRange(events);

            foreach (var @event in events)
            {
                try
                {
                    ImportedEvents.Add(@event, TypeManager.InstantiateEvent(@event));
                }
                catch (Exception ex)
                {
                    Host.Log(LoggingEvent.Severity.Error
                        , Constants._EventImportError
                        , @event.FullName
                        , ex);
                }
            }

            Imported = true;
            Host.Log(LoggingEvent.Severity.Trace, Constants._EventsImported);
        }


        /// <inheritdoc/>
        public IList<Uri> GetEventAssemblyLocations()
        {
            return EventPaths;
        }


        /// <inheritdoc/>
        public IList<EventName> GetKnownEvents()
        {
            return ImportedEvents.Values.Select(i => i.Name).ToList();
        }


        /// <inheritdoc/>
        public IList<Type> GetKnownEventTypes()
        {
            return ImportedEvents.Keys.ToList();
        }


        /// <inheritdoc/>
        public IEvent GetSolidEventFromName(EventName name)
        {
            var imported = ImportedEvents.FirstOrDefault(i => i.Value.Name == name);
            
            if (imported.Equals(default(KeyValuePair<Type, IEvent>)))
            {
                return null;
            }

            try
            {
                return TypeManager.InstantiateEvent(imported.Key);
            }
            catch (Exception ex)
            {
                Host.Log(LoggingEvent.Severity.Error
                    , Constants._EventInstantiateError,
                    imported.Value.Name.ToString(),
                    imported.Key.FullName
                    , ex);
            }

            return null;
        }


        /// <inheritdoc/>
        public IEvent GetSolidEventFromType(Type type)
        {
            var imported = ImportedEvents.FirstOrDefault(i => i.Key == type);
            
            if (imported.Equals(default(KeyValuePair<Type, IEvent>)))
            {
                return null;
            }

            try
            {
                return TypeManager.InstantiateEvent(imported.Key);
            }
            catch (Exception ex)
            {
                Host.Log(LoggingEvent.Severity.Error
                    , Constants._EventInstantiateError,
                    imported.Value.Name.ToString(),
                    imported.Key.FullName
                    , ex);
            }

            return null;
        }


        /// <inheritdoc/>
        public IEvent GetSolidEventFromType<T, I, O>() where T : IEvent<I, O> where I : IEventInput where O : IEventOutput
        {
            var imported = ImportedEvents.FirstOrDefault(i => i.Key == typeof(T));
            
            if (imported.Equals(default(KeyValuePair<Type, IEvent>)))
            {
                return null;
            }

            try
            {
                return TypeManager.InstantiateEvent(imported.Key);
            }
            catch (Exception ex)
            {
                Host.Log(LoggingEvent.Severity.Error
                    , Constants._EventInstantiateError,
                    imported.Value.Name.ToString(),
                    imported.Key.FullName
                    , ex);
            }

            return null;
        }
    }
}

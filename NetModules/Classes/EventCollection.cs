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

        IDictionary<Type, IEvent> Instantiated;
        IList<Uri> EventPaths = new List<Uri>();

        internal EventCollection(IModuleHost host)
        {
            Host = host;
            Instantiated = new Dictionary<Type, IEvent>();
        }


        internal void ImportEvents()
        {
            if (Imported)
            {
                throw new Exception("Events already imported. Importing events multiple times would cause multiple instances of all known events.");
            }

            var events = TypeManager.FindEvents<IEvent>(Host, Host.WorkingDirectory, 1);

            Host.Log(LoggingEvent.Severity.Debug, $"Importing {events.Count} events...");

            EventPaths = events.Select(e => new Uri(e.Assembly.Location)).Distinct().ToList();

            AddRange(events);

            foreach (var @event in events)
            {
                try
                {
                    Instantiated.Add(@event, TypeManager.InstantiateEvent(@event));
                }
                catch (Exception ex)
                {
                    Host.Log(LoggingEvent.Severity.Error
                        , "An error occurred while attempting to import an event."
                        , ex);
                }
            }

            Imported = true;
            Host.Log(LoggingEvent.Severity.Debug, $"Events imported.");
        }


        /// <inheritdoc/>
        public IList<Uri> GetEventAssemblyLocations()
        {
            return EventPaths;
        }


        /// <inheritdoc/>
        public IList<EventName> GetKnownEvents()
        {
            return Instantiated.Values.Select(i => i.Name).ToList();
        }


        /// <inheritdoc/>
        public IList<Type> GetKnownEventTypes()
        {
            return Instantiated.Keys.ToList();
        }


        /// <inheritdoc/>
        public IEvent GetSolidEventFromName(EventName name)
        {
            var instantiated = Instantiated.FirstOrDefault(i => i.Value.Name == name);
            
            if (instantiated.Equals(default(KeyValuePair<Type, IEvent>)))
            {
                return null;
            }

            try
            {
                return TypeManager.InstantiateEvent(instantiated.Key);
            }
            catch (Exception ex)
            {
                Host.Log(LoggingEvent.Severity.Error
                    , "An error occurred while attempting to instantiate an event."
                    , ex);
            }

            return null;
        }


        /// <inheritdoc/>
        public IEvent GetSolidEventFromType(Type type)
        {
            var instantiated = Instantiated.FirstOrDefault(i => i.Key == type);
            
            if (instantiated.Equals(default(KeyValuePair<Type, IEvent>)))
            {
                return null;
            }

            try
            {
                return TypeManager.InstantiateEvent(instantiated.Key);
            }
            catch (Exception ex)
            {
                Host.Log(LoggingEvent.Severity.Error
                    , "An error occurred while attempting to instantiate an event."
                    , ex);
            }

            return null;
        }


        /// <inheritdoc/>
        public IEvent GetSolidEventFromType<T, I, O>() where T : IEvent<I, O> where I : IEventInput where O : IEventOutput
        {
            var instantiated = Instantiated.FirstOrDefault(i => i.Key == typeof(T));
            
            if (instantiated.Equals(default(KeyValuePair<Type, IEvent>)))
            {
                return null;
            }

            try
            {
                return TypeManager.InstantiateEvent(instantiated.Key);
            }
            catch (Exception ex)
            {
                Host.Log(LoggingEvent.Severity.Error
                    , "An error occurred while attempting to instantiate an event."
                    , ex);
            }

            return null;
        }
    }
}

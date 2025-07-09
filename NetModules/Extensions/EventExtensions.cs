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
using System.Linq;
using NetModules.Classes;
using NetModules.Interfaces;

namespace NetModules
{
    /// <summary>
    /// Helpful extension methods for working with Event metadata.
    /// </summary>
    [Serializable]
    public static class EventExtensions
    {
        /// <summary>
        /// Reusing constants here for a quick check to ensure protected meta keys that are set internally by
        /// <see cref="ModuleHost"/> and <see cref="ModuleCollection"/> aren't overwritten by
        /// <see cref="SetMetaValue(IEvent, string, object, bool)"/>.
        /// 
        /// This doesn't stop someone replacing the entire Event.Meta object on an event, due to the <see cref="IEvent"/>
        /// interface allowing <see cref="IEvent.Meta"/> to be set, but it does prevent accidental overwriting of the
        /// internal values via these extension methods.
        /// </summary>
        static readonly string[] _ProtectedMetaKeys = { Constants._MetaId, Constants._MetaHandlers };


        /// <summary>
        /// This is the equivalent of <see cref="SetMetaValue(IEvent, string, object, bool)"/>, but allows internal classes
        /// that are using this extension method to set meta keys that are used internally, namely id, and handlers.
        /// <see cref="SetMetaValue(IEvent, string, object, bool)"/> is now a wrapper for this method.
        /// </summary>
        internal static void SetMetaValueInternal(this IEvent @this, string key, object value, bool forceOverwrite = false, bool setProtected = false)
        {
            // Initialize if null, while copying locally fixes any issues with cross-process/cross-domain EventHandlers.
            var localMeta = @this.Meta == null ? new Dictionary<string, object>() : new Dictionary<string, object>(@this.Meta);

            if (string.IsNullOrEmpty(key) || (_ProtectedMetaKeys.Contains(key) && !setProtected))
            {
                @this.Meta = localMeta;
                return;
            }

            if (localMeta.ContainsKey(key))
            {
                if (forceOverwrite)
                {
                    localMeta[key] = value;
                }
            }
            else
            {
                localMeta.Add(key, value);
            }

            @this.Meta = localMeta;
        }


        /// <summary>
        /// Gets a meta value on an event.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="defaultValue">A value to return if the metadata key or value is not found.</param>
        public static object GetMeta(this IEvent @this, string key, object defaultValue = null)
        {
            return GetMetaValue(@this, key, defaultValue);
        }


        /// <summary>
        /// Gets a meta value on an event.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="defaultValue">A value to return if the metadata key or value is not found.</param>
        public static object GetMetaValue(this IEvent @this, string key, object defaultValue = null)
        {
            return GetMetaValue<object>(@this, key, defaultValue);
        }


        /// <summary>
        /// Gets a meta value on an event.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="defaultValue">A value to return if the metadata key or value is not found.</param>
        public static T GetMetaValue<T>(this IEvent @this, string key, T defaultValue = default(T))
        {
            object val;

            if (@this == null || @this.Meta == null || !@this.Meta.TryGetValue(key, out val))
            {
                return defaultValue;
            }

            if (val is T t)
            {
                return t;
            }

            var type = typeof(T);

            if (val is IConvertible conv && type is IConvertible)
            {
                try
                {
                    return (T)conv.ToType(type, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch { }
            }
            
            return defaultValue;
        }


        /// <summary>
        /// Gets a meta value on an event.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="defaultValue">A value to return if the metadata key or value is not found.</param>
        public static T GetMeta<T>(this IEvent @this, string key, T defaultValue = default(T))
        {
            return GetMetaValue(@this, key, defaultValue);
        }


        /// <summary>
        /// Gets a meta value on an event.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="parser">Allows you to specify a parser for a matching metadata value to ensure the value is returned in the correct format.</param>
        /// <param name="defaultValue">A value to return if the metadata key or value is not found.</param>
        public static T GetMetaValue<T>(this IEvent @this, string key, Func<object, T> parser, T defaultValue = default(T))
        {
            object val;

            if (@this.Meta == null || !@this.Meta.TryGetValue(key, out val))
            {
                return defaultValue;
            }

            return parser(val);
        }


        /// <summary>
        /// Gets a meta value on an event.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="parser">Allows you to specify a parser for a matching metadata value to ensure the value is returned in the correct format.</param>
        /// <param name="defaultValue">A value to return if the metadata key or value is not found.</param>
        public static T GetMeta<T>(this IEvent @this, string key, Func<object, T> parser, T defaultValue = default(T))
        {
            return GetMetaValue(@this, key, parser, defaultValue);
        }


        /// <summary>
        /// Check if a key exists in the meta data.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        public static bool HasMeta(this IEvent @this, string key)
        {
            return @this.Meta != null && @this.Meta.ContainsKey(key);
        }


        /// <summary>
        /// Remove a key from meta data. returns true if a metadata item is removed, and false if not.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        public static bool DeleteMeta(this IEvent @this, string key)
        {
            if (@this.Meta != null && @this.Meta.ContainsKey(key))
            {
                @this.Meta.Remove(key);
                return true;
            }

            return false;
        }


        /// <summary>
        /// Remove a key from meta data. returns true if a metadata item is removed, and false if not.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        public static bool RemoveMeta(this IEvent @this, string key)
        {
            return DeleteMeta(@this, key);
        }



        /// <summary>
        /// Sets a metadata key/value pair on an Event.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to set a metadata value on.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="value">The metadata value to set for the key or identifier, or metavalue name.</param>
        /// <param name="forceOverwrite">If the metadata value already exists, this must be true if you wish to overwrite it.</param>
        public static void SetMetaValue(this IEvent @this, string key, object value, bool forceOverwrite = false)
        {
            SetMetaValueInternal(@this, key, value, forceOverwrite, false);
        }


        /// <summary>
        /// Sets a metadata key/value pair on an Event.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to set a metadata value on.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="value">The metadata value to set for the key or identifier, or metavalue name.</param>
        /// <param name="forceOverwrite">If the metadata value already exists, this must be true if you wish to overwrite it.</param>
        public static void SetMeta(this IEvent @this, string key, object value, bool forceOverwrite = false)
        {
            SetMetaValue(@this, key, value, forceOverwrite);
        }


        /// <summary>
        /// Sets a metadata key/value pair on an Event.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to set a metadata value on.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="value">The metadata value to set for the key or identifier, or metavalue name.</param>
        /// <param name="forceOverwrite">If the metadata value already exists, this must be true if you wish to overwrite it.</param>
        public static void AddMeta(this IEvent @this, string key, object value, bool forceOverwrite = false)
        {
            SetMetaValue(@this, key, value, forceOverwrite);
        }


        /// <summary>
        /// Sets a metadata key/value pair on an <see cref="IEvent"/> and returns the <see cref="IEvent"/> for processing.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to set a metadata value on.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="value">The metadata value to set for the key or identifier, or metavalue name.</param>
        public static IEvent WithMeta(this IEvent @this, string key, object value)
        {
            SetMetaValue(@this, key, value, true);
            return @this;
        }


        /// <summary>
        /// Sets a metadata key/value pair on an <see cref="IEvent"/> and returns the <see cref="IEvent"/> for processing.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to set a metadata value on.</param>
        /// <param name="values">The metadata values to set for the keys or identifiers, or metavalue names.</param>
        public static IEvent WithMeta(this IEvent @this, Dictionary<string, object> values)
        {
            if (values == null || values.Count == 0)
            {
                return @this;
            }

            return WithMeta(@this, values.ToArray());
        }


        /// <summary>
        /// Sets a metadata key/value pair on an <see cref="IEvent"/> and returns the <see cref="IEvent"/> for processing.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to set a metadata value on.</param>
        /// <param name="values">The metadata values to set for the keys or identifiers, or metavalue names.</param>
        public static IEvent WithMeta(this IEvent @this, params KeyValuePair<string, object>[] values)
        {
            if (values == null || values.Length == 0)
            {
                return @this;
            }

            foreach (var kvp in values)
            {
                SetMetaValue(@this, kvp.Key, kvp.Value, true);
            }
            
            return @this;
        }
    }
}

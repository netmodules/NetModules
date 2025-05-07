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

namespace NetModules
{
    /// <summary>
    /// Helpful extension methods for working with Event metadata.
    /// </summary>
    [Serializable]
    public static class EventExtensions
    {
        /// <summary>
        /// Gets a meta value on an event.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="defaultValue">A value to return if the metadata key or value is not found.</param>
        public static object GetMetaValue(this IEvent @this, string key, object defaultValue = null)
        {
            object val = defaultValue;

            if (@this.Meta == null || !@this.Meta.TryGetValue(key, out val))
            {
                return defaultValue;
            }

            return val;
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
        public static T GetMetaValue<T>(this IEvent @this, string key, T defaultValue = default(T))
        {
            object val;

            if (@this.Meta == null || !@this.Meta.TryGetValue(key, out val))
            {
                return defaultValue;
            }

            try
            {
                return (T)val; // This will likely fail when used with HandleJson method and deserializing since json is passed around as strings will throw invalid cast. Requires bullet proofing
            }
            catch
            {
                return defaultValue;
            }
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
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="value">The metadata value to set for the key or identifier, or metavalue name.</param>
        /// <param name="forceOverwrite">If the metadata value already exists, this must be true if you wish to overwrite it.</param>
        public static void SetMetaValue(this IEvent @this, string key, object value, bool forceOverwrite = false)
        {
            if (@this.Meta == null)
            {
                @this.Meta = new Dictionary<string, object>();
            }

            // Copying locally fixes any issues with cross-process/cross-domain EventHandlers.
            var local = new Dictionary<string, object>(@this.Meta);

            if (local.ContainsKey(key))
            {
                if (forceOverwrite)
                {
                    local[key] = value;
                }
            }
            else
            {
                local.Add(key, value);
            }

            @this.Meta = local;
        }


        /// <summary>
        /// Sets a metadata key/value pair on an Event.
        /// </summary>
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
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
        /// <param name="this">The <see cref="IEvent"/> to select a metadata value from.</param>
        /// <param name="key">The key, identifier, or name of the metadata item.</param>
        /// <param name="value">The metadata value to set for the key or identifier, or metavalue name.</param>
        /// <param name="forceOverwrite">If the metadata value already exists, this must be true if you wish to overwrite it.</param>
        public static void AddMeta(this IEvent @this, string key, object value, bool forceOverwrite = false)
        {
            SetMetaValue(@this, key, value, forceOverwrite);
        }
    }
}

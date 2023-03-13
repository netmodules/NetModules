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

using System;

namespace NetModules
{
    /// <summary>
    /// <see cref="EventName"/> This class acts as a simple string wrapper to offer a more self descriptive usage type.
    /// </summary>
    [Serializable]
    public struct EventName
    {
        /// <summary>
        /// Holds the string value of the event name.
        /// </summary>
        readonly string Value;


        /// <summary>
        /// Creates a new <see cref="EventName"/> with a string value.
        /// </summary>
        /// <param name="value">The string value for this <see cref="EventName"/></param>
        public EventName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("The event name can not be a null or empty string.", "value");
            }

            Value = value;
        }

        #region Override IEqualityComparer methods

        /// <summary>
        /// 
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is EventName name)
            {
                return Value.Equals(name.Value);
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// 
        /// </summary>
        public static EventName FromString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("The event name can not be a null or empty string.", "value");
            }

            return new EventName(value);
        }

        #region Override operators.

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator EventName(string s)
        {
            return new EventName(s);
        }

        /// <summary>
        /// 
        /// </summary>
        public static implicit operator string(EventName s)
        {
            return s.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool operator == (EventName x, EventName y)
        {
            return x.Value == y.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool operator != (EventName x, EventName y)
        {
            return x.Value != y.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool operator ==(EventName x, string y)
        {
            return x.Value == y;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool operator !=(EventName x, string y)
        {
            return x.Value != y;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool operator ==(string x, EventName y)
        {
            return x == y.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool operator !=(string x, EventName y)
        {
            return x != y.Value;
        }

        #endregion
    }
}

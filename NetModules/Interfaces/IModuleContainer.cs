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
using NetModules.Classes;

namespace NetModules.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModuleContainer
    {
        /// <summary>
        /// The <see cref="IModuleHost"/> that has loaded this module
        /// </summary>
        IModuleHost Host { get; }
        
        
        /// <summary>
        /// Returns true if this <see cref="IModule"/> instance has been initialized.
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        /// Returns the file path to this <see cref="IModule"/> instance.
        /// </summary>
        Uri Path { get; }
        
        
        /// <summary>
        /// The <see cref="AssemblyLoader"/> that is/was used to load this <see cref="IModule"/> instance.
        /// </summary>
        AssemblyLoader LoadContext { get; }


        /// <summary>
        /// The <see cref="Type"/> returned by this <see cref="IModule"/> instance's GetType() method.
        /// </summary>
        Type ModuleType { get; }


        /// <summary>
        /// This <see cref="IModule"/> instance's attributes.
        /// </summary>
        IModuleAttribute ModuleAttributes { get; }
        
        
        /// <summary>
        /// The <see cref="IModule"/> instance loaded in the current <see cref="IModuleContainer"/>
        /// </summary>
        Module Module { get; }


        /// <summary>
        /// Initialize the instance of this <see cref="IModule"/>
        /// </summary>
        void InitializeModule();


        /// <summary>
        /// Deinitialize the instance of this <see cref="IModule"/>
        /// </summary>
        void DeinitializeModule();
    }
}

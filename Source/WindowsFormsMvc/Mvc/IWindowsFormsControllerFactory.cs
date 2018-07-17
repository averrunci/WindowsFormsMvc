// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Charites.Windows.Mvc
{
    /// <summary>
    /// Provides the function to create a Windows Forms controller.
    /// </summary>
    public interface IWindowsFormsControllerFactory
    {
        /// <summary>
        /// Creates a Windows Forms controller of the specified type.
        /// </summary>
        /// <param name="controllerType">The type of a Windows Forms controller.</param>
        /// <returns>The new instance of a Windows Forms controller.</returns>
        object Create(Type controllerType);
    }
}

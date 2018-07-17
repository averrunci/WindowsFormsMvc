// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Windows.Forms;

namespace Charites.Windows.Mvc
{
    /// <summary>
    /// Provides the function to find a data context defined in a view.
    /// </summary>
    public interface IWindowsFormsDataContextFinder : IDataContextFinder<Control>
    {
        /// <summary>
        /// Finds a <see cref="DataContextSource"/> of a data context defined in the specified view.
        /// </summary>
        /// <param name="view">The view in which a data context is defined.</param>
        /// <returns>
        /// The <see cref="DataContextSource"/> of a data context found in the view.
        /// If not found in the view, <c>null</c> is returned.
        /// If some data contexts are found, one of the is returned.
        /// </returns>
        DataContextSource FindSource(Control view);
    }
}

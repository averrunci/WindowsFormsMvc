// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc
{
    /// <summary>
    /// Represents the method that handles the <see cref="DataContextSource.DataContextChanged"/> event.
    /// </summary>
    /// <param name="sender">The object where the event handler is attached.</param>
    /// <param name="e">The event data.</param>
    public delegate void DataContextChangedEventHandler(object? sender, DataContextChangedEventArgs e);

    /// <summary>
    /// Provides data for the <see cref="DataContextSource.DataContextChanged"/> event.
    /// </summary>
    public class DataContextChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the old data context value.
        /// </summary>
        public object? OldValue { get; }

        /// <summary>
        /// Gets the new data context value.
        /// </summary>
        public object? NewValue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextChangedEventArgs"/> class
        /// with the specified old data context value and new data context value.
        /// </summary>
        /// <param name="oldValue">The old data context value.</param>
        /// <param name="newValue">The new data context value.</param>
        public DataContextChangedEventArgs(object? oldValue, object? newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}

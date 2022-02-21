// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

namespace Charites.Windows.Forms;

/// <summary>
/// Represents the method that handles the <see cref="ListViewItemsSource{TItem}.ItemBound"/> event.
/// </summary>
/// <typeparam name="TItem">The type of the item.</typeparam>
/// <param name="sender">The object where the event handler is attached.</param>
/// <param name="e">The event data.</param>
public delegate void ListViewItemBoundEventHandler<TItem>(object? sender, ListViewItemBoundEventArgs<TItem> e);

/// <summary>
/// Provides data for the <see cref="ListViewItemsSource{TItem}.ItemBound"/> event.
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class ListViewItemBoundEventArgs<TItem> : EventArgs
{
    /// <summary>
    /// Gets the context of the item of the list view.
    /// </summary>
    public ListViewItemContext<TItem> ListViewItemContext { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListViewItemBoundEventArgs{TItem}"/> class
    /// with the specified context of the item of the list view.
    /// </summary>
    /// <param name="listViewItemContext">The context of the item of the list view.</param>
    public ListViewItemBoundEventArgs(ListViewItemContext<TItem> listViewItemContext)
    {
        ListViewItemContext = listViewItemContext;
    }
}
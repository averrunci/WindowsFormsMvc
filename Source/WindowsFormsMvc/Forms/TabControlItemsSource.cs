// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms;

/// <summary>
/// Represents a source of items that are bound to a tab control.
/// </summary>
public class TabControlItemsSource : ItemsSource<TabControl>
{
    /// <summary>
    /// Gets the path of the property that represents the header.
    /// </summary>
    public string? HeaderMember { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TabControlItemsSource"/> class
    /// with the specified tab control to which items are bound.
    /// </summary>
    /// <param name="tabControl">The tab control to which items are bound.</param>
    public TabControlItemsSource(TabControl tabControl) : base(tabControl)
    {
    }

    /// <summary>
    /// Inserts the specified item into the collection within the tab control at the specified index.
    /// </summary>
    /// <param name="index">The location in the collection within the tab control to insert the item.</param>
    /// <param name="item">The item to add to the collection within the tab control.</param>
    protected override void InsertItem(int index, object item)
    {
        Control.TabPages.Insert(index, GetHeader(item));
        Control.TabPages[index].Controls.Add(new ContentControl
        {
            Dock = DockStyle.Fill,
            Content = item
        });
    }

    /// <summary>
    /// Removes the specified item at the specified index.
    /// </summary>
    /// <param name="index">The location in the collection within the tab control to remove the item.</param>
    /// <param name="item">The item to remove from the collection with in the tab control.</param>
    protected override void RemoveItem(int index, object item)
    {
        Control.TabPages.RemoveAt(index);
    }

    /// <summary>
    /// Replaces the specified item at the specified index.
    /// </summary>
    /// <param name="index">The index to replace the item at.</param>
    /// <param name="newItem">The item to replace.</param>
    /// <param name="oldItem">The item to be replaced.</param>
    protected override void ReplaceItem(int index, object newItem, object oldItem)
    {
        Control.TabPages[index].Text = GetHeader(newItem);
        Control.TabPages[index].Controls.OfType<ContentControl>().ForEach(contentControl => contentControl.Content = newItem);
    }

    /// <summary>
    /// Clears all items from the collection within the tab control.
    /// </summary>
    protected override void ClearItems()
    {
        Control.TabPages.Clear();
    }

    /// <summary>
    /// Gets the header value of the specified item.
    /// </summary>
    /// <param name="item">The item that has the header value.</param>
    /// <returns>The header value of the item.</returns>
    protected string GetHeader(object item)
        => (HeaderMember is null ? item.ToString() : item.GetType().GetProperty(HeaderMember)?.GetValue(item) as string) ?? string.Empty;
}

/// <summary>
/// Represents a source of items that are bound to a tab control.
/// Each item has a header for the tab page.
/// </summary>
/// <typeparam name="TItem">The type of the item.</typeparam>
public class TabControlItemsSource<TItem> : ValuedItemsSource<TabControl, TItem, string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TabControlItemsSource{TItem}"/> class
    /// with the specified tab control to which items are bound and selector to select
    /// the header value of each item.
    /// </summary>
    /// <param name="tabControl">The tab control to which items are bound.</param>
    /// <param name="headerSelector">The selector to select the header value of each item.</param>
    public TabControlItemsSource(TabControl tabControl, Func<TItem, ObservableProperty<string>> headerSelector) : base(tabControl, headerSelector)
    {
    }

    /// <summary>
    /// Inserts the specified value into the collection within the tab control at the specified index.
    /// </summary>
    /// <param name="index">The location in the collection within the tab control to insert the value.</param>
    /// <param name="value">The value to add to the collection within the tab control.</param>
    protected override void InsertItem(int index, string value)
    {
        Control.TabPages.Insert(index, value);
    }

    /// <summary>
    /// Inserts the specified item into the collection within the tab control at the specified index.
    /// </summary>
    /// <param name="index">The location in the collection within the tab control to insert the item.</param>
    /// <param name="item">The item to add to the collection within the tab control.</param>
    protected override void InsertItem(int index, TItem item)
    {
        base.InsertItem(index, item);

        Control.TabPages[index].Controls.Add(new ContentControl
        {
            Dock = DockStyle.Fill,
            Content = item
        });
    }

    /// <summary>
    /// Removes the specified value at the specified index.
    /// </summary>
    /// <param name="index">The location in the collection within the tab control to remove the value.</param>
    /// <param name="value">The value to remove from the collection with in the tab control.</param>
    protected override void RemoveItem(int index, string value)
    {
        Control.TabPages.RemoveAt(index);
    }

    /// <summary>
    /// Replaces the specified value at the specified index.
    /// </summary>
    /// <param name="index">The index to replace the value at.</param>
    /// <param name="newValue">The value to replace.</param>
    /// <param name="oldValue">The value to be replaced.</param>
    protected override void ReplaceItem(int index, string newValue, string oldValue)
    {
        Control.TabPages[index].Text = newValue;
    }

    /// <summary>
    /// Replaces the specified item at the specified index.
    /// </summary>
    /// <param name="index">The index to replace the item at.</param>
    /// <param name="newItem">The item to replace.</param>
    /// <param name="oldItem">The item to be replaced.</param>
    protected override void ReplaceItem(int index, TItem newItem, TItem oldItem)
    {
        base.ReplaceItem(index, newItem, oldItem);

        Control.TabPages[index].Controls.OfType<ContentControl>().ForEach(contentControl => contentControl.Content = newItem);
    }
}
// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;
using System.Collections.Specialized;

namespace Charites.Windows.Forms;

/// <summary>
/// Represents a source of items that are bound to a control.
/// </summary>
/// <typeparam name="TControl">The type of the control.</typeparam>
/// <typeparam name="TItem">The type of the item.</typeparam>
public abstract class ItemsSource<TControl, TItem> where TControl : Control
{
    /// <summary>
    /// Gets the control to which items are bound.
    /// </summary>
    protected TControl Control { get; }

    /// <summary>
    /// Gets the value that indicates whether the item is bound.
    /// </summary>
    protected bool IsItemBound { get; private set; }

    private IEnumerable? items;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemsSource{TControl,TItem}"/> class
    /// with the specified control to which items are bound.
    /// </summary>
    /// <param name="control">The control to which items are bound.</param>
    protected ItemsSource(TControl control)
    {
        Control = control;
    }

    /// <summary>
    /// Binds the specified items.
    /// </summary>
    /// <param name="items">The items to be bound.</param>
    /// <exception cref="InvalidOperationException">
    /// This control has already bound another items.
    /// </exception>
    public void Bind(IEnumerable items)
    {
        if (IsItemBound) throw new InvalidOperationException("This control has already bound another items.");

        this.items = items;
        if (this.items is INotifyCollectionChanged notifyCollectionChanged) notifyCollectionChanged.CollectionChanged += OnItemCollectionChanged;

        OnItemsBinding(GetItems());

        IsItemBound = true;
    }

    /// <summary>
    /// Unbinds the bound items.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// This control has not bound any items yet.
    /// </exception>
    public void Unbind()
    {
        if (!IsItemBound || items is null) throw new InvalidOperationException("This control has not bound any items yet.");

        if (items is INotifyCollectionChanged notifyCollectionChanged) notifyCollectionChanged.CollectionChanged -= OnItemCollectionChanged;

        OnItemsUnbinding(GetItems());

        items = null;
        IsItemBound = false;
    }

    /// <summary>
    /// Inserts the specified item into the collection within the control at the specified index.
    /// </summary>
    /// <param name="index">The location in the collection within the control to insert the item.</param>
    /// <param name="item">The item to add to the collection within the control.</param>
    protected abstract void InsertItem(int index, TItem item);

    /// <summary>
    /// Removes the specified item at the specified index.
    /// </summary>
    /// <param name="index">The location in the collection within the control to remove the item.</param>
    /// <param name="item">The item to remove from the collection with in the control.</param>
    protected abstract void RemoveItem(int index, TItem item);

    /// <summary>
    /// Moves the specified item from the specified index to the specified index.
    /// </summary>
    /// <param name="newIndex">The index to move the item to.</param>
    /// <param name="oldIndex">The index to move the item from.</param>
    /// <param name="item">The item to move within the collection.</param>
    protected virtual void MoveItem(int newIndex, int oldIndex, TItem item)
    {
        RemoveItem(oldIndex, item);
        InsertItem(newIndex, item);
    }

    /// <summary>
    /// Replaces the specified item at the specified index.
    /// </summary>
    /// <param name="index">The index to replace the item at.</param>
    /// <param name="newItem">The item to replace.</param>
    /// <param name="oldItem">The item to be replaced.</param>
    protected abstract void ReplaceItem(int index, TItem newItem, TItem oldItem);

    /// <summary>
    /// Clears all items from the collection within the control.
    /// </summary>
    protected abstract void ClearItems();

    /// <summary>
    /// Handles the specified items when they are binding.
    /// </summary>
    /// <param name="items">The items that are binding.</param>
    protected virtual void OnItemsBinding(IList<TItem> items)
    {
        ClearItems();
        for (var index = 0; index < items.Count; ++index)
        {
            InsertItem(index, items[index]);
        }
    }

    /// <summary>
    /// Handles the specified items when they are unbinding.
    /// </summary>
    /// <param name="items">The items that are unbinding.</param>
    protected virtual void OnItemsUnbinding(IList<TItem> items)
    {
    }

    /// <summary>
    /// Gets the bound items.
    /// </summary>
    /// <returns>The bound items.</returns>
    protected IList<TItem> GetItems() => items?.OfType<TItem>().ToList() ?? new List<TItem>();

    private void InsertItems(int startIndex, IList<TItem> items)
    {
        if (Control.InvokeRequired)
        {
            Control.BeginInvoke(new Action<int, IList<TItem>>(InsertItems), startIndex, items);
            return;
        }

        Enumerable.Range(0, items.Count).ForEach(itemIndex => InsertItem(startIndex + itemIndex, items[itemIndex]));
    }

    private void RemoveItems(int startIndex, IList<TItem> items)
    {
        if (Control.InvokeRequired)
        {
            Control.BeginInvoke(new Action<int, IList<TItem>>(RemoveItems), startIndex, items);
            return;
        }

        Enumerable.Range(0, items.Count).ForEach(itemIndex => RemoveItem(startIndex + itemIndex, items[itemIndex]));
    }

    private void MoveItems(int newStartIndex, int oldStartIndex, IList<TItem> items)
    {
        if (Control.InvokeRequired)
        {
            Control.BeginInvoke(new Action<int, int, IList<TItem>>(MoveItems), newStartIndex, oldStartIndex, items);
            return;
        }

        Enumerable.Range(0, items.Count).ForEach(itemIndex => MoveItem(newStartIndex + itemIndex, oldStartIndex + itemIndex, items[itemIndex]));
    }

    private void ReplaceItems(int startIndex, IList<TItem> newItems, IList<TItem> oldItems)
    {
        if (Control.InvokeRequired)
        {
            Control.BeginInvoke(new Action<int, IList<TItem>, IList<TItem>>(ReplaceItems), startIndex, newItems, oldItems);
            return;
        }

        Enumerable.Range(0, oldItems.Count).ForEach(itemIndex => ReplaceItem(startIndex + itemIndex, newItems[itemIndex], oldItems[itemIndex]));
    }

    private void ClearAllItems()
    {
        if (Control.InvokeRequired)
        {
            Control.BeginInvoke(ClearAllItems);
            return;
        }

        ClearItems();
    }

    private void OnItemCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (IsValidNewItems(e)) InsertItems(e.NewStartingIndex, e.NewItems?.OfType<TItem>().ToList() ?? new List<TItem>());
                break;
            case NotifyCollectionChangedAction.Remove:
                if (IsValidOldItems(e)) RemoveItems(e.OldStartingIndex, e.OldItems?.OfType<TItem>().ToList() ?? new List<TItem>());
                break;
            case NotifyCollectionChangedAction.Move:
                if (IsValidMoveItems(e)) MoveItems(e.NewStartingIndex, e.OldStartingIndex, e.NewItems?.OfType<TItem>().ToList() ?? new List<TItem>());
                break;
            case NotifyCollectionChangedAction.Replace:
                if (IsValidReplaceItems(e)) ReplaceItems(e.NewStartingIndex, e.NewItems?.OfType<TItem>().ToList() ?? new List<TItem>(), e.OldItems?.OfType<TItem>().ToList() ?? new List<TItem>());
                break;
            case NotifyCollectionChangedAction.Reset:
                ClearAllItems();
                break;
        }
    }

    private bool IsValidNewItems(NotifyCollectionChangedEventArgs e) => e.NewItems is not null && e.NewStartingIndex > -1;
    private bool IsValidOldItems(NotifyCollectionChangedEventArgs e) => e.OldItems is not null && e.OldStartingIndex > -1;
    private bool IsValidMoveItems(NotifyCollectionChangedEventArgs e) => IsValidNewItems(e) && IsValidOldItems(e) && ItemSequenceEqual(e.NewItems ?? new List<TItem>(), e.OldItems ?? new List<TItem>());
    private bool IsValidReplaceItems(NotifyCollectionChangedEventArgs e) => IsValidNewItems(e) && IsValidOldItems(e) && e.NewStartingIndex == e.OldStartingIndex;

    private bool ItemSequenceEqual(IList newItems, IList oldItems) =>
        newItems.Count == oldItems.Count && !newItems.Cast<object>().Where((t, index) => !Equals(t, oldItems[index])).Any();
}

/// <summary>
/// Represents a source of items that are bound to a control.
/// </summary>
/// <typeparam name="TControl">The type of the control.</typeparam>
public abstract class ItemsSource<TControl> : ItemsSource<TControl, object> where TControl : Control
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemsSource{TControl}"/> class
    /// with the specified control to which items are bound.
    /// </summary>
    /// <param name="control">The control to which items are bound.</param>
    protected ItemsSource(TControl control) : base(control)
    {
    }
}
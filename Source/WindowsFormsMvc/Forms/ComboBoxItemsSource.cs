// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms;

/// <summary>
/// Represents a source of items that are bound to a combo box.
/// </summary>
public class ComboBoxItemsSource : ItemsSource<ComboBox>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ComboBoxItemsSource"/> class
    /// with the specified combo box to which items are bound.
    /// </summary>
    /// <param name="comboBox">The combo box to which items are bound.</param>
    public ComboBoxItemsSource(ComboBox comboBox) : base(comboBox)
    {
    }

    /// <summary>
    /// Inserts the specified item into the collection within the combo box at the specified index.
    /// </summary>
    /// <param name="index">The location in the collection within the combo box to insert the item.</param>
    /// <param name="item">The item to add to the collection within the combo box.</param>
    protected override void InsertItem(int index, object item) => Control.Items.Insert(index, item);

    /// <summary>
    /// Removes the specified item at the specified index.
    /// </summary>
    /// <param name="index">The location in the collection within the combo box to remove the item.</param>
    /// <param name="item">The item to remove from the collection with in the combo box.</param>
    protected override void RemoveItem(int index, object item) => Control.Items.RemoveAt(index);

    /// <summary>
    /// Replaces the specified item at the specified index.
    /// </summary>
    /// <param name="index">The index to replace the item at.</param>
    /// <param name="newItem">The item to replace.</param>
    /// <param name="oldItem">The item to be replaced.</param>
    protected override void ReplaceItem(int index, object newItem, object oldItem) => Control.Items[index] = newItem;

    /// <summary>
    /// Clears all items from the collection within the combo box.
    /// </summary>
    protected override void ClearItems() => Control.Items.Clear();
}

/// <summary>
/// Represents a source of items that are bound to a combo box.
/// Each item has a value to display its representation.
/// </summary>
/// <typeparam name="TItem">The type of the item.</typeparam>
/// <typeparam name="TValue">The type of the value of the item.</typeparam>
public class ComboBoxItemsSource<TItem, TValue> : ValuedItemsSource<ComboBox, TItem, TValue> where TItem : notnull where TValue : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ComboBoxItemsSource{TItem,TValue}"/> class
    /// with the specified combo box to which items are bound and selector to select the value of each item.
    /// </summary>
    /// <param name="comboBox">The combo box to which items are bound.</param>
    /// <param name="valueSelector">The selector to select the value of each item.</param>
    public ComboBoxItemsSource(ComboBox comboBox, Func<TItem, ObservableProperty<TValue>> valueSelector) : base(comboBox, valueSelector)
    {
    }

    /// <summary>
    /// Inserts the specified value into the collection within the combo box at the specified index.
    /// </summary>
    /// <param name="index">The location in the collection within the combo box to insert the value.</param>
    /// <param name="value">The value to add to the collection within the combo box.</param>
    protected override void InsertItem(int index, TValue value) => Control.Items.Insert(index, value);

    /// <summary>
    /// Removes the specified value at the specified index.
    /// </summary>
    /// <param name="index">The location in the collection within the combo box to remove the value.</param>
    /// <param name="value">The value to remove from the collection with in the combo box.</param>
    protected override void RemoveItem(int index, TValue value) => Control.Items.RemoveAt(index);

    /// <summary>
    /// Replaces the specified value at the specified index.
    /// </summary>
    /// <param name="index">The index to replace the value at.</param>
    /// <param name="newValue">The value to replace.</param>
    /// <param name="oldValue">The value to be replaced.</param>
    protected override void ReplaceItem(int index, TValue newValue, TValue oldValue) => Control.Items[index] = newValue;
}

/// <summary>
/// Represents a source of items that are bound to a combo box.
/// Each item has a string value to display its representation.
/// </summary>
/// <typeparam name="TItem">The type of the item.</typeparam>
public class ComboBoxItemsSource<TItem> : ComboBoxItemsSource<TItem, string> where TItem : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ComboBoxItemsSource{TItem,TValue}"/> class
    /// with the specified combo box to which items are bound and selector to select the string
    /// value of each item.
    /// </summary>
    /// <param name="comboBox">The combo box to which items are bound.</param>
    /// <param name="valueSelector">The selector to select the string value of each item.</param>
    public ComboBoxItemsSource(ComboBox comboBox, Func<TItem, ObservableProperty<string>> valueSelector) : base(comboBox, valueSelector)
    {
    }
}
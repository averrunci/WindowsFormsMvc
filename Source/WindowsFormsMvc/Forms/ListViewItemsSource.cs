// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms
{
    /// <summary>
    /// Represents a source of items that are bound to a list view.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    public class ListViewItemsSource<TItem> : ItemsSource<ListView, TItem>
    {
        /// <summary>
        /// Occurs when each item is bound.
        /// </summary>
        public event ListViewItemBoundEventHandler<TItem> ItemBound;

        /// <summary>
        /// Gets or sets the path of the property that represents the value
        /// that indicates whether to be checked.
        /// </summary>
        public string CheckedMember { get; set; }

        /// <summary>
        /// Gets the selector to select the observable property for the checked value.
        /// </summary>
        public Func<TItem, ObservableProperty<bool>> CheckedSelector { get; set; }

        private readonly List<ListViewItemContext<TItem>> listViewItemContexts = new List<ListViewItemContext<TItem>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ListViewItemsSource{TItem}"/> class.
        /// </summary>
        /// <param name="listView">The list view to which items are bound.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="listView"/> is <c>null</c>.
        /// </exception>
        public ListViewItemsSource(ListView listView) : base(listView)
        {
        }

        /// <summary>
        /// Inserts the specified item into the collection within the list view at the specified index.
        /// </summary>
        /// <param name="index">The location in the collection within the list view to insert the item.</param>
        /// <param name="item">The item to add to the collection within the list view.</param>
        protected override void InsertItem(int index, TItem item)
        {
            var listViewItem = new ListViewItem();
            if (IsItemBound) Control.ItemChecked -= OnListViewItemChecked;
            Control.Items.Insert(index, listViewItem);
            UpdateChecked(listViewItem, item);
            if (IsItemBound) Control.ItemChecked += OnListViewItemChecked;

            var listViewItemContext = new ListViewItemContext<TItem>(item, listViewItem);
            listViewItemContexts.Insert(index, listViewItemContext);

            OnItemBound(new ListViewItemBoundEventArgs<TItem>(listViewItemContext));

            CheckedSelector.IfPresent(_ => listViewItemContext.BindChecked(CheckedSelector));
        }

        /// <summary>
        /// Removes the specified item at the specified index.
        /// </summary>
        /// <param name="index">The location in the collection within the list view to remove the item.</param>
        /// <param name="item">The item to remove from the collection with in the list view.</param>
        protected override void RemoveItem(int index, TItem item)
        {
            listViewItemContexts[index].Dispose();
            listViewItemContexts.RemoveAt(index);

            Control.Items.RemoveAt(index);
        }

        /// <summary>
        /// Replaces the specified item at the specified index.
        /// </summary>
        /// <param name="index">The index to replace the item at.</param>
        /// <param name="newItem">The item to replace.</param>
        /// <param name="oldItem">The item to be replaced.</param>
        protected override void ReplaceItem(int index, TItem newItem, TItem oldItem)
        {
            listViewItemContexts[index].Dispose();
            listViewItemContexts[index] = new ListViewItemContext<TItem>(newItem, Control.Items[index]);
            UpdateChecked(Control.Items[index], newItem);

            OnItemBound(new ListViewItemBoundEventArgs<TItem>(listViewItemContexts[index]));
        }

        /// <summary>
        /// Clears all items from the collection within the list view.
        /// </summary>
        protected override void ClearItems()
        {
            for (var index = listViewItemContexts.Count - 1; index >= 0; --index)
            {
                RemoveItem(index, listViewItemContexts[index].Item);
            }
        }

        /// <summary>
        /// Handles the specified items when they are binding.
        /// </summary>
        /// <param name="items">The items that are binding.</param>
        protected override void OnItemsBinding(IList<TItem> items)
        {
            base.OnItemsBinding(items);

            Control.ItemChecked += OnListViewItemChecked;
        }

        /// <summary>
        /// Handles the specified items when they are unbinding.
        /// </summary>
        /// <param name="items">The items that are unbinding.</param>
        protected override void OnItemsUnbinding(IList<TItem> items)
        {
            base.OnItemsUnbinding(items);

            Control.ItemChecked -= OnListViewItemChecked;
        }

        /// <summary>
        /// Raises the <see cref="ListViewItemsSource{TItem}.ItemBound"/> event
        /// with the specified event data.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnItemBound(ListViewItemBoundEventArgs<TItem> e) => ItemBound?.Invoke(this, e);

        private void UpdateChecked(ListViewItem listViewItem, TItem item)
        {
            CheckedSelector.IfPresent(_ => listViewItem.Checked = CheckedSelector(item).Value);
            CheckedMember.IfPresent(_ =>
            {
                var checkedValue = item?.GetType().GetProperty(CheckedMember)?.GetValue(item);
                if (!(checkedValue is bool @checked)) return;

                listViewItem.Checked = @checked;
            });
        }

        private void OnListViewItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CheckedSelector.IfPresent(_ => CheckedSelector(GetItems()[e.Item.Index]).Value = e.Item.Checked);
            CheckedMember.IfPresent(_ =>
            {
                var item = GetItems()[e.Item.Index];
                item.GetType().GetProperty(CheckedMember)?.SetValue(item, e.Item.Checked);
            });
        }
    }
}

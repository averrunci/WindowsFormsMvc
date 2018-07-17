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
    /// Represents a source of items that are bound to a control. Each item has a value to display its representation.
    /// </summary>
    /// <typeparam name="TControl">The type of the control.</typeparam>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TValue">The type of the value of the item.</typeparam>
    public abstract class ValuedItemsSource<TControl, TItem, TValue> : ItemsSource<TControl, TItem> where TControl : Control
    {
        private readonly Func<TItem, ObservableProperty<TValue>> valueSelector;
        private readonly IList<ItemContext> itemContexts = new List<ItemContext>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuedItemsSource{TControl,TItem,TValue}"/> class
        /// with the specified control to which items are bound and the specified selector to select the value of each item.
        /// </summary>
        /// <param name="control">The control to which items are bound.</param>
        /// <param name="valueSelector">The selector to select the value of each item.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="control"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="valueSelector"/> is <c>null</c>.
        /// </exception>
        protected ValuedItemsSource(TControl control, Func<TItem, ObservableProperty<TValue>> valueSelector) : base(control)
        {
            this.valueSelector = valueSelector.RequireNonNull(nameof(valueSelector));
        }

        /// <summary>
        /// Inserts the specified value into the collection within the control at the specified index.
        /// </summary>
        /// <param name="index">The location in the collection within the control to insert the value.</param>
        /// <param name="value">The value to add to the collection within the control.</param>
        protected abstract void InsertItem(int index, TValue value);

        /// <summary>
        /// Removes the specified value at the specified index.
        /// </summary>
        /// <param name="index">The location in the collection within the control to remove the value.</param>
        /// <param name="value">The value to remove from the collection with in the control.</param>
        protected abstract void RemoveItem(int index, TValue value);

        /// <summary>
        /// Replaces the specified value at the specified index.
        /// </summary>
        /// <param name="index">The index to replace the value at.</param>
        /// <param name="newValue">The value to replace.</param>
        /// <param name="oldValue">The value to be replaced.</param>
        protected abstract void ReplaceItem(int index, TValue newValue, TValue oldValue);

        /// <summary>
        /// Inserts the specified item into the collection within the control at the specified index.
        /// </summary>
        /// <param name="index">The location in the collection within the control to insert the item.</param>
        /// <param name="item">The item to add to the collection within the control.</param>
        protected override void InsertItem(int index, TItem item)
        {
            var valueObservableProperty = GetValueObservableProperty(item);
            itemContexts.Insert(index, new ItemContext(Control, itemContexts, item, valueObservableProperty, ReplaceItem));

            InsertItem(index, valueObservableProperty.Value);
        }

        /// <summary>
        /// Removes the specified item at the specified index.
        /// </summary>
        /// <param name="index">The location in the collection within the control to remove the item.</param>
        /// <param name="item">The item to remove from the collection with in the control.</param>
        protected override void RemoveItem(int index, TItem item)
        {
            itemContexts[index].Dispose();
            itemContexts.RemoveAt(index);

            RemoveItem(index, GetValueObservableProperty(item).Value);
        }

        /// <summary>
        /// Replaces the specified item at the specified index.
        /// </summary>
        /// <param name="index">The index to replace the item at.</param>
        /// <param name="newItem">The item to replace.</param>
        /// <param name="oldItem">The item to be replaced.</param>
        protected override void ReplaceItem(int index, TItem newItem, TItem oldItem)
        {
            var newValueObservableProperty = GetValueObservableProperty(newItem);
            itemContexts[index].Dispose();
            itemContexts[index] = new ItemContext(Control, itemContexts, newItem, newValueObservableProperty, ReplaceItem);

            ReplaceItem(index, newValueObservableProperty.Value, GetValueObservableProperty(oldItem).Value);
        }

        /// <summary>
        /// Clears all items from the collection within the control.
        /// </summary>
        protected override void ClearItems()
        {
            for (var index = itemContexts.Count - 1; index >= 0; --index)
            {
                RemoveItem(index, itemContexts[index].Item);
            }
        }

        /// <summary>
        /// Gets the <see cref="ObservableProperty{T}"/> for the value of the specified item.
        /// </summary>
        /// <param name="item">The item that has the value.</param>
        /// <returns>The <see cref="ObservableProperty{T}"/> for the value of the item.</returns>
        protected ObservableProperty<TValue> GetValueObservableProperty(TItem item) => valueSelector(item);

        /// <summary>
        /// Represents a content of an item.
        /// </summary>
        protected class ItemContext : IDisposable
        {
            /// <summary>
            /// Gets the item.
            /// </summary>
            public TItem Item { get; }

            private readonly TControl control;
            private readonly IList<ItemContext> owner;
            private readonly ObservableProperty<TValue> valueObservableProperty;
            private readonly Action<int, TValue, TValue> valueChangedAction;

            /// <summary>
            /// Initializes a new instance of the <see cref="ItemContext"/> class
            /// with the specified control to which items are bound, owner that contains
            /// this context, item, observable property for the value of the item,
            /// and the action that is performed when the value is changed.
            /// </summary>
            /// <param name="control">The control to which items are bound.</param>
            /// <param name="owner">The list that contains this context.</param>
            /// <param name="item">The item in the control.</param>
            /// <param name="valueObservableProperty">The observable property for the value of the item.</param>
            /// <param name="valueChangedAction">The action that is performed when the value is changed.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="control"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="owner"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="item"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="valueObservableProperty"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="valueChangedAction"/> is <c>null</c>.
            /// </exception>
            public ItemContext(TControl control, IList<ItemContext> owner, TItem item, ObservableProperty<TValue> valueObservableProperty, Action<int, TValue, TValue> valueChangedAction)
            {
                this.control = control.RequireNonNull(nameof(control));
                this.owner = owner.RequireNonNull(nameof(owner));
                Item = item.RequireNonNull(nameof(item));
                this.valueObservableProperty = valueObservableProperty.RequireNonNull(nameof(valueObservableProperty));
                this.valueChangedAction = valueChangedAction.RequireNonNull(nameof(valueChangedAction));

                this.valueObservableProperty.PropertyValueChanged += OnValueChanged;
            }

            /// <summary>
            /// Releases all resources used by this context.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Releases the <see cref="ObservableProperty{T}.PropertyValueChanged"/> event handler.
            /// </summary>
            /// <param name="disposing">
            /// <c>true</c> to release the <see cref="ObservableProperty{T}.PropertyValueChanged"/> event handler.
            /// </param>
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    valueObservableProperty.PropertyValueChanged -= OnValueChanged;
                }
            }

            private void OnValueChanged(object sender, PropertyValueChangedEventArgs<TValue> e)
            {
                var index = owner.IndexOf(this);
                if (index < 0) return;

                if (control.InvokeRequired)
                {
                    control.BeginInvoke(new Action<object, PropertyValueChangedEventArgs<TValue>>(OnValueChanged), sender, e);
                    return;
                }

                valueChangedAction(index, e.NewValue, e.OldValue);
            }
        }
    }
}

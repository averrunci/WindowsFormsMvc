// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms
{
    /// <summary>
    /// Represents a source of items that are bound to a checked list box.
    /// </summary>
    public class CheckedListBoxItemsSource : ItemsSource<CheckedListBox>
    {
        /// <summary>
        /// Gets or sets the path of the property that represents the value.
        /// </summary>
        public string ValueMember { get; set; }

        /// <summary>
        /// Gets or sets the path of the property that represents the value
        /// that indicates whether to be checked.
        /// </summary>
        public string CheckedMember { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedListBoxItemsSource"/> class
        /// with the specified checked list box to which items are bound.
        /// </summary>
        /// <param name="checkedListBox">The checked list box to which items are bound.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedListBox"/> is <c>null</c>.
        /// </exception>
        public CheckedListBoxItemsSource(CheckedListBox checkedListBox) : base(checkedListBox)
        {
        }

        /// <summary>
        /// Inserts the specified item into the collection within the checked list box at the specified index.
        /// </summary>
        /// <param name="index">The location in the collection within the checked list box to insert the item.</param>
        /// <param name="item">The item to add to the collection within the checked list box.</param>
        protected override void InsertItem(int index, object item)
        {
            Control.Items.Insert(index, GetValue(item));
            Control.SetItemCheckState(index, GetCheckState(item));
        }

        /// <summary>
        /// Removes the specified item at the specified index.
        /// </summary>
        /// <param name="index">The location in the collection within the checked list box to remove the item.</param>
        /// <param name="item">The item to remove from the collection with in the checked list box.</param>
        protected override void RemoveItem(int index, object item)
        {
            Control.Items.RemoveAt(index);
        }

        /// <summary>
        /// Replaces the specified item at the specified index.
        /// </summary>
        /// <param name="index">The index to replace the item at.</param>
        /// <param name="newItem">The item to replace.</param>
        /// <param name="oldItem">The item to be replaced.</param>
        protected override void ReplaceItem(int index, object newItem, object oldItem)
        {
            Control.Items[index] = GetValue(newItem);
            Control.SetItemCheckState(index, GetCheckState(newItem));
        }

        /// <summary>
        /// Clears all items from the collection within the checked list box.
        /// </summary>
        protected override void ClearItems()
        {
            Control.Items.Clear();
        }

        /// <summary>
        /// Handles the specified items when they are binding.
        /// </summary>
        /// <param name="items">The items that are binding.</param>
        protected override void OnItemsBinding(IList<object> items)
        {
            base.OnItemsBinding(items);

            Control.ItemCheck += OnCheckedListBoxItemCheck;
        }

        /// <summary>
        /// Handles the specified items when they are unbinding.
        /// </summary>
        /// <param name="items">The items that are unbinding.</param>
        protected override void OnItemsUnbinding(IList<object> items)
        {
            base.OnItemsUnbinding(items);

            Control.ItemCheck -= OnCheckedListBoxItemCheck;
        }

        /// <summary>
        /// Gets the value of the specified item.
        /// </summary>
        /// <param name="item">The item that has the value.</param>
        /// <returns>The value of the item.</returns>
        protected object GetValue(object item)
            => ValueMember == null ? item : item?.GetType().GetProperty(ValueMember)?.GetValue(item);

        /// <summary>
        /// Gets the check state of the specified item.
        /// </summary>
        /// <param name="item">The item that defines the check state.</param>
        /// <returns>The check state of the item.</returns>
        protected CheckState GetCheckState(object item)
        {
            var checkedProperty = GetCheckedProperty(item);
            if (checkedProperty == null) return CheckState.Unchecked;

            if (checkedProperty.PropertyType == typeof(bool)) return ((bool)checkedProperty.GetValue(item)).ToCheckState();
            if (checkedProperty.PropertyType == typeof(bool?)) return ((bool?)checkedProperty.GetValue(item)).ToCheckState();
            if (checkedProperty.PropertyType == typeof(CheckState)) return (CheckState)checkedProperty.GetValue(item);

            return CheckState.Unchecked;
        }

        /// <summary>
        /// Sets the specified check state to the specified item.
        /// </summary>
        /// <param name="item">The item that defines the check state.</param>
        /// <param name="checkState">The check state to set.</param>
        protected void SetCheckState(object item, CheckState checkState)
        {
            var checkedProperty = GetCheckedProperty(item);
            if (checkedProperty == null) return;

            if (checkedProperty.PropertyType == typeof(bool)) checkedProperty.SetValue(item, checkState.ToBoolean());
            if (checkedProperty.PropertyType == typeof(bool?)) checkedProperty.SetValue(item, checkState.ToNullableOfBoolean());
            if (checkedProperty.PropertyType == typeof(CheckState)) checkedProperty.SetValue(item, checkState);
        }

        private PropertyInfo GetCheckedProperty(object item)
            => CheckedMember == null ? null : item?.GetType().GetProperty(CheckedMember);

        private void OnCheckedListBoxItemCheck(object sender, ItemCheckEventArgs e)
        {
            SetCheckState(GetItems()[e.Index], e.NewValue);
        }
    }

    /// <summary>
    /// Represents a source of items that are bound to a checked list box.
    /// Each item has a value to display its representation.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TValue">The type of the value of the item.</typeparam>
    public class CheckedListBoxItemsSource<TItem, TValue> : ValuedItemsSource<CheckedListBox, TItem, TValue>
    {
        private readonly CheckStateContextFactory checkStateContextFactory;

        private readonly List<CheckStateContext> checkStateContexts = new List<CheckStateContext>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedListBoxItemsSource{TItem,TValue}"/> class
        /// with the specified checked list box to which items are bound, selector to select the value
        /// of each item, and selector to select the boolean checked value of each item.
        /// </summary>
        /// <param name="checkedListBox">The checked list box to which items are bound.</param>
        /// <param name="valueSelector">The selector to select the value of each item.</param>
        /// <param name="checkedValueSelector">The selector to select the boolean checked value of each item.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedListBox"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="valueSelector"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedValueSelector"/> is <c>null</c>.
        /// </exception>
        public CheckedListBoxItemsSource(CheckedListBox checkedListBox, Func<TItem, ObservableProperty<TValue>> valueSelector, Func<TItem, ObservableProperty<bool>> checkedValueSelector) : base(checkedListBox, valueSelector)
        {
            checkStateContextFactory = new CheckStateContextFactory(checkedValueSelector);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedListBoxItemsSource{TItem,TValue}"/> class
        /// with the specified checked list box to which items are bound, selector to select the value
        /// of each item, and selector to select the nullable of the boolean checked value of each item.
        /// </summary>
        /// <param name="checkedListBox">The checked list box to which items are bound.</param>
        /// <param name="valueSelector">The selector to select the value of each item.</param>
        /// <param name="checkedValueSelector">
        /// The selector to select the nullable of the boolean checked value of each item.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedListBox"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="valueSelector"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedValueSelector"/> is <c>null</c>.
        /// </exception>
        public CheckedListBoxItemsSource(CheckedListBox checkedListBox, Func<TItem, ObservableProperty<TValue>> valueSelector, Func<TItem, ObservableProperty<bool?>> checkedValueSelector) : base(checkedListBox, valueSelector)
        {
            checkStateContextFactory = new CheckStateContextFactory(checkedValueSelector);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedListBoxItemsSource{TItem,TValue}"/> class
        /// with the specified checked list box to which items are bound, selector to select the value
        /// of each item, and selector to select the check state of each item.
        /// </summary>
        /// <param name="checkedListBox">The checked list box to which items are bound.</param>
        /// <param name="valueSelector">The selector to select the value of each item.</param>
        /// <param name="checkStateSelector">The selector to select the check state of each item.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedListBox"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="valueSelector"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkStateSelector"/> is <c>null</c>.
        /// </exception>
        public CheckedListBoxItemsSource(CheckedListBox checkedListBox, Func<TItem, ObservableProperty<TValue>> valueSelector, Func<TItem, ObservableProperty<CheckState>> checkStateSelector) : base(checkedListBox, valueSelector)
        {
            checkStateContextFactory = new CheckStateContextFactory(checkStateSelector);
        }

        /// <summary>
        /// Inserts the specified value into the collection within the checked list box at the specified index.
        /// </summary>
        /// <param name="index">The location in the collection within the checked list box to insert the value.</param>
        /// <param name="value">The value to add to the collection within the checked list box.</param>
        protected override void InsertItem(int index, TValue value)
        {
            Control.Items.Insert(index, value);
        }

        /// <summary>
        /// Inserts the specified item into the collection within the checked list box at the specified index.
        /// </summary>
        /// <param name="index">The location in the collection within the checked list box to insert the item.</param>
        /// <param name="item">The item to add to the collection within the checked list box.</param>
        protected override void InsertItem(int index, TItem item)
        {
            base.InsertItem(index, item);

            var checkStateContext = checkStateContextFactory.Create(Control, checkStateContexts, item, SetItemCheckState);
            checkStateContexts.Insert(index, checkStateContext);
            Control.SetItemCheckState(index, checkStateContext.Value);
        }

        /// <summary>
        /// Removes the specified value at the specified index.
        /// </summary>
        /// <param name="index">The location in the collection within the checked list box to remove the value.</param>
        /// <param name="value">The value to remove from the collection with in the checked list box.</param>
        protected override void RemoveItem(int index, TValue value)
        {
            Control.Items.RemoveAt(index);
        }

        /// <summary>
        /// Removes the specified item at the specified index.
        /// </summary>
        /// <param name="index">The location in the collection within the checked list box to remove the item.</param>
        /// <param name="item">The item to remove from the collection with in the checked list box.</param>
        protected override void RemoveItem(int index, TItem item)
        {
            base.RemoveItem(index, item);

            checkStateContexts[index].Dispose();
            checkStateContexts.RemoveAt(index);
        }

        /// <summary>
        /// Replaces the specified value at the specified index.
        /// </summary>
        /// <param name="index">The index to replace the value at.</param>
        /// <param name="newValue">The value to replace.</param>
        /// <param name="oldValue">The value to be replaced.</param>
        protected override void ReplaceItem(int index, TValue newValue, TValue oldValue)
        {
            Control.Items[index] = newValue;
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

            checkStateContexts[index].Dispose();
            checkStateContexts[index] = checkStateContextFactory.Create(Control, checkStateContexts, newItem, SetItemCheckState);
            Control.SetItemCheckState(index, checkStateContexts[index].Value);
        }

        /// <summary>
        /// Handles the specified items when they are binding.
        /// </summary>
        /// <param name="items">The items that are binding.</param>
        protected override void OnItemsBinding(IList<TItem> items)
        {
            base.OnItemsBinding(items);

            Control.ItemCheck += OnCheckedListBoxItemCheck;
        }

        /// <summary>
        /// Handles the specified items when they are unbinding.
        /// </summary>
        /// <param name="items">The items that are unbinding.</param>
        protected override void OnItemsUnbinding(IList<TItem> items)
        {
            base.OnItemsUnbinding(items);

            Control.ItemCheck -= OnCheckedListBoxItemCheck;
        }

        private void OnCheckedListBoxItemCheck(object sender, ItemCheckEventArgs e)
        {
            checkStateContexts[e.Index].SetCheckState(e.NewValue);
        }

        /// <summary>
        /// Sets the specified check state of the item at the specified index.
        /// </summary>
        /// <param name="index">The index of the item to set the check state to.</param>
        /// <param name="newCheckState">The check state to set.</param>
        protected virtual void SetItemCheckState(int index, CheckState newCheckState) => Control.SetItemCheckState(index, newCheckState);

        /// <summary>
        /// Represents a context of a check state.
        /// </summary>
        protected class CheckStateContext : IDisposable
        {
            /// <summary>
            /// Gets the check state.
            /// </summary>
            public CheckState Value => booleanCheckedValue?.Value.ToCheckState() ?? nullableOfBooleanCheckedValue?.Value.ToCheckState() ?? checkState?.Value ?? CheckState.Unchecked;

            private readonly CheckedListBox checkedListBox;
            private readonly IList<CheckStateContext> owner;

            private readonly ObservableProperty<bool> booleanCheckedValue;
            private readonly ObservableProperty<bool?> nullableOfBooleanCheckedValue;
            private readonly ObservableProperty<CheckState> checkState;

            private readonly Action<int, CheckState> checkStateChangedAction;

            /// <summary>
            /// Initializes a new instance of the <see cref="CheckStateContext"/> class
            /// with the specified checked list box to which items are bound, owner that
            /// contains this context, the observable property of te boolean value that
            /// indicates whether to be checked, and action that is performed when the
            /// check state is changed.
            /// </summary>
            /// <param name="checkedListBox">The checked list box to which items are bound.</param>
            /// <param name="owner">The list that contains this context.</param>
            /// <param name="checkedValue">
            /// The observable property of the boolean value that indicates whether to be checked.
            /// </param>
            /// <param name="checkStateChangedAction">
            /// The action that is performed when the check state is changed.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="checkedListBox"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="owner"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="checkedValue"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="checkStateChangedAction"/> is <c>null</c>.
            /// </exception>
            public CheckStateContext(CheckedListBox checkedListBox, IList<CheckStateContext> owner, ObservableProperty<bool> checkedValue, Action<int, CheckState> checkStateChangedAction) : this(checkedListBox, owner, checkStateChangedAction)
            {
                booleanCheckedValue = checkedValue.RequireNonNull(nameof(checkedValue));
                booleanCheckedValue.PropertyValueChanged += OnBooleanCheckedValueChanged;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="CheckStateContext"/> class
            /// with the specified checked list box to which items are bound, owner that
            /// contains this context, the observable property of the nullable of the boolean
            /// value that indicates whether to be checked, and action that is performed when
            /// the check state is changed.
            /// </summary>
            /// <param name="checkedListBox">The checked list box to which items are bound.</param>
            /// <param name="owner">The list that contains this context.</param>
            /// <param name="checkedValue">
            /// The observable property of the nullable of the boolean value that indicates
            /// whether to be checked.
            /// </param>
            /// <param name="checkStateChangedAction">
            /// The action that is performed when the check state is changed.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="checkedListBox"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="owner"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="checkedValue"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="checkStateChangedAction"/> is <c>null</c>.
            /// </exception>
            public CheckStateContext(CheckedListBox checkedListBox, IList<CheckStateContext> owner, ObservableProperty<bool?> checkedValue, Action<int, CheckState> checkStateChangedAction) : this(checkedListBox, owner, checkStateChangedAction)
            {
                nullableOfBooleanCheckedValue = checkedValue.RequireNonNull(nameof(checkedValue));
                nullableOfBooleanCheckedValue.PropertyValueChanged += OnNullableOfBooleanCheckedValueChanged;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="CheckStateContext"/> class
            /// with the specified checked list box to which items are bound, owner that
            /// contains this context, the observable property of the check state value
            /// that indicates whether to be checked, and action that is performed when
            /// the check state is changed.
            /// </summary>
            /// <param name="checkedListBox">The checked list box to which items are bound.</param>
            /// <param name="owner">The list that contains this context.</param>
            /// <param name="checkState">
            /// The observable property of the check state value that indicates
            /// whether to be checked.
            /// </param>
            /// <param name="checkStateChangedAction">
            /// The action that is performed when the check state is changed.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="checkedListBox"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="owner"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="checkState"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="checkStateChangedAction"/> is <c>null</c>.
            /// </exception>
            public CheckStateContext(CheckedListBox checkedListBox, IList<CheckStateContext> owner, ObservableProperty<CheckState> checkState, Action<int, CheckState> checkStateChangedAction) : this(checkedListBox, owner, checkStateChangedAction)
            {
                this.checkState = checkState.RequireNonNull(nameof(checkState));
                this.checkState.PropertyValueChanged += OnCheckStateChanged;
            }

            private CheckStateContext(CheckedListBox checkedListBox, IList<CheckStateContext> owner, Action<int, CheckState> checkStateChangedAction)
            {
                this.checkedListBox = checkedListBox.RequireNonNull(nameof(checkedListBox));
                this.owner = owner.RequireNonNull(nameof(owner));
                this.checkStateChangedAction = checkStateChangedAction.RequireNonNull(nameof(checkStateChangedAction));
            }

            /// <summary>
            /// Sets the specified check state.
            /// </summary>
            /// <param name="newCheckState">The check state to set.</param>
            public void SetCheckState(CheckState newCheckState)
            {
                booleanCheckedValue.IfPresent(_ => booleanCheckedValue.Value = newCheckState.ToBoolean());
                nullableOfBooleanCheckedValue.IfPresent(_ => nullableOfBooleanCheckedValue.Value = newCheckState.ToNullableOfBoolean());
                checkState.IfPresent(_ => checkState.Value = newCheckState);
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
                    booleanCheckedValue.IfPresent(_ => booleanCheckedValue.PropertyValueChanged -= OnBooleanCheckedValueChanged);
                    nullableOfBooleanCheckedValue.IfPresent(_ => nullableOfBooleanCheckedValue.PropertyValueChanged -= OnNullableOfBooleanCheckedValueChanged);
                    checkState.IfPresent(_ => checkState.PropertyValueChanged -= OnCheckStateChanged);
                }
            }

            private void OnCheckStateChanged(CheckState newCheckState)
            {
                var index = owner.IndexOf(this);
                if (index < 0) return;

                checkStateChangedAction(index, newCheckState);
            }

            private void OnBooleanCheckedValueChanged(object sender, PropertyValueChangedEventArgs<bool> e)
            {
                if (checkedListBox.InvokeRequired)
                {
                    checkedListBox.BeginInvoke(new Action<object, PropertyValueChangedEventArgs<bool>>(OnBooleanCheckedValueChanged), sender, e);
                    return;
                }

                OnCheckStateChanged(e.NewValue.ToCheckState());
            }

            private void OnNullableOfBooleanCheckedValueChanged(object sender, PropertyValueChangedEventArgs<bool?> e)
            {
                if (checkedListBox.InvokeRequired)
                {
                    checkedListBox.BeginInvoke(new Action<object, PropertyValueChangedEventArgs<bool?>>(OnNullableOfBooleanCheckedValueChanged), sender, e);
                    return;
                }

                OnCheckStateChanged(e.NewValue.ToCheckState());
            }

            private void OnCheckStateChanged(object sender, PropertyValueChangedEventArgs<CheckState> e)
            {
                if (checkedListBox.InvokeRequired)
                {
                    checkedListBox.BeginInvoke(new Action<object, PropertyValueChangedEventArgs<CheckState>>(OnCheckStateChanged), sender, e);
                    return;
                }

                OnCheckStateChanged(e.NewValue);
            }
        }

        /// <summary>
        /// Provides the function to create a new instance of the <see cref="CheckStateContext"/> class.
        /// </summary>
        protected class CheckStateContextFactory
        {
            private readonly Func<TItem, ObservableProperty<bool>> booleanCheckedValueSelector;
            private readonly Func<TItem, ObservableProperty<bool?>> nullableOfBooleanCheckedValueSelector;
            private readonly Func<TItem, ObservableProperty<CheckState>> checkStateSelector;

            /// <summary>
            /// Initializes a new instance of the <see cref="CheckStateContextFactory"/> class
            /// with the specified selector to select the boolean checked value of each item.
            /// </summary>
            /// <param name="checkedValueSelector">The selector to select the boolean checked value of each item.</param>
            public CheckStateContextFactory(Func<TItem, ObservableProperty<bool>> checkedValueSelector) => booleanCheckedValueSelector = checkedValueSelector;

            /// <summary>
            /// Initializes a new instance of the <see cref="CheckStateContextFactory"/> class
            /// with the specified selector to select the nullable of the boolean checked value of each item.
            /// </summary>
            /// <param name="checkedValueSelector">The selector to select the nullable of the boolean checked value of each item.</param>
            public CheckStateContextFactory(Func<TItem, ObservableProperty<bool?>> checkedValueSelector) => nullableOfBooleanCheckedValueSelector = checkedValueSelector;

            /// <summary>
            /// Initializes a new instance of the <see cref="CheckStateContextFactory"/> class
            /// with the specified selector to select the check state of each item.
            /// </summary>
            /// <param name="checkStateSelector">The selector to select the check state of each item.</param>
            public CheckStateContextFactory(Func<TItem, ObservableProperty<CheckState>> checkStateSelector) => this.checkStateSelector = checkStateSelector;

            /// <summary>
            /// Creates a new instance of the <see cref="CheckStateContext"/> class
            /// with the specified checked list box to which items are bound, owner
            /// that contains the created context, item that is bound to the checked
            /// list box, and action that is performed when the check state is changed.
            /// </summary>
            /// <param name="checkedListBox">The checked list box to which items are bound.</param>
            /// <param name="owner">The list that contains the created context.</param>
            /// <param name="item">The item that is bound to the checked list box.</param>
            /// <param name="checkStateChangedAction">
            /// The action that is performed when the check state is changed.
            /// </param>
            /// <returns>The new instance of the <see cref="CheckStateContext"/> class.</returns>
            /// <exception cref="InvalidOperationException">
            /// Any selectors for the check state are not specified.
            /// </exception>
            public CheckStateContext Create(CheckedListBox checkedListBox, IList<CheckStateContext> owner, object item, Action<int, CheckState> checkStateChangedAction)
            {
                if (booleanCheckedValueSelector != null) return new CheckStateContext(checkedListBox, owner, item is TItem typedItem ? booleanCheckedValueSelector(typedItem) : new ObservableProperty<bool>(), checkStateChangedAction);
                if (nullableOfBooleanCheckedValueSelector != null) return new CheckStateContext(checkedListBox, owner, item is TItem typedItem ? nullableOfBooleanCheckedValueSelector(typedItem) : new ObservableProperty<bool?>(), checkStateChangedAction);
                if (checkStateSelector != null) return new CheckStateContext(checkedListBox, owner, item is TItem typedItem ? checkStateSelector(typedItem) : new ObservableProperty<CheckState>(), checkStateChangedAction);

                throw new InvalidOperationException("The selector must be specified.");
            }
        }
    }

    /// <summary>
    /// Represents a source of items that are bound to a checked list box.
    /// Each item has a string value to display its representation.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    public class CheckedListBoxItemsSource<TItem> : CheckedListBoxItemsSource<TItem, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedListBoxItemsSource{TItem}"/> class
        /// with the specified checked list box to which items are bound, selector to select the
        /// string value of each item, and selector to select the boolean checked value of each item.
        /// </summary>
        /// <param name="checkedListBox">The checked list box to which items are bound.</param>
        /// <param name="valueSelector">The selector to select the string value of each item.</param>
        /// <param name="checkedValueSelector">The selector to select the boolean checked value of each item.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedListBox"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="valueSelector"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedValueSelector"/> is <c>null</c>.
        /// </exception>
        public CheckedListBoxItemsSource(CheckedListBox checkedListBox, Func<TItem, ObservableProperty<string>> valueSelector, Func<TItem, ObservableProperty<bool>> checkedValueSelector) : base(checkedListBox, valueSelector, checkedValueSelector)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedListBoxItemsSource{TItem}"/> class
        /// with the specified checked list box to which items are bound, selector to select the string
        /// value of each item, and selector to select the nullable of the boolean checked value of each item.
        /// </summary>
        /// <param name="checkedListBox">The checked list box to which items are bound.</param>
        /// <param name="valueSelector">The selector to select the string value of each item.</param>
        /// <param name="checkedValueSelector">
        /// The selector to select the nullable of the boolean checked value of each item.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedListBox"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="valueSelector"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedValueSelector"/> is <c>null</c>.
        /// </exception>
        public CheckedListBoxItemsSource(CheckedListBox checkedListBox, Func<TItem, ObservableProperty<string>> valueSelector, Func<TItem, ObservableProperty<bool?>> checkedValueSelector) : base(checkedListBox, valueSelector, checkedValueSelector)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedListBoxItemsSource{TItem}"/> class
        /// with the specified checked list box to which items are bound, selector to select the
        /// string value of each item, and selector to select the check state of each item.
        /// </summary>
        /// <param name="checkedListBox">The checked list box to which items are bound.</param>
        /// <param name="valueSelector">The selector to select the string value of each item.</param>
        /// <param name="checkStateSelector">The selector to select the check state of each item.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedListBox"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="valueSelector"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkStateSelector"/> is <c>null</c>.
        /// </exception>
        public CheckedListBoxItemsSource(CheckedListBox checkedListBox, Func<TItem, ObservableProperty<string>> valueSelector, Func<TItem, ObservableProperty<CheckState>> checkStateSelector) : base(checkedListBox, valueSelector, checkStateSelector)
        {
        }
    }
}

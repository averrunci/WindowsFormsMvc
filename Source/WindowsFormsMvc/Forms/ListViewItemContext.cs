// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms
{
    /// <summary>
    /// Represents a context of an item of a list view.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    public class ListViewItemContext<TItem> : IDisposable
    {
        /// <summary>
        /// Gets the item of the list view.
        /// </summary>
        public TItem Item { get; }

        /// <summary>
        /// Gets the <see cref="System.Windows.Forms.ListViewItem"/> of the list view.
        /// </summary>
        public ListViewItem ListViewItem { get; }

        private readonly List<ListViewSubItemContext> listViewSubItemContexts = new List<ListViewSubItemContext>();

        private IDisposable checkedBinding;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListViewItemContext{TItem}"/> class
        /// with the specified item and <see cref="System.Windows.Forms.ListViewItem"/> of the list view.
        /// </summary>
        /// <param name="item">The item of the list view.</param>
        /// <param name="listViewItem">The <see cref="System.Windows.Forms.ListViewItem"/> of the list view.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="item"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="listViewItem"/> is <c>null</c>.
        /// </exception>
        public ListViewItemContext(TItem item, ListViewItem listViewItem)
        {
            Item = item.RequireNonNull(nameof(item));
            ListViewItem = listViewItem.RequireNonNull(nameof(listViewItem));
        }

        /// <summary>
        /// Binds the observable property selected using the specified selector
        /// to the checked property of the <see cref="System.Windows.Forms.ListViewItem"/>.
        /// </summary>
        /// <param name="checkedSelector">The selector to select the observable property for the checked value.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedSelector"/> is <c>null</c>.
        /// </exception>
        public void BindChecked(Func<TItem, ObservableProperty<bool>> checkedSelector)
            => BindChecked(new ListViewItemBinding<bool>(checkedSelector.RequireNonNull(nameof(checkedSelector))(Item), OnCheckedChanged));

        /// <summary>
        /// Binds the observable property selected using the specified selector and converter
        /// to the checked property of the <see cref="System.Windows.Forms.ListViewItem"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value of the observable property.</typeparam>
        /// <param name="checkedSelector">The selector to select the observable property for the checked value.</param>
        /// <param name="converter">
        /// The converter to convert the value of the observable property to the boolean checked value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="checkedSelector"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="converter"/> is <c>null</c>.
        /// </exception>
        public void BindChecked<T>(Func<TItem, ObservableProperty<T>> checkedSelector, Func<T, bool> converter)
            => BindChecked(new ListViewItemBinding<T, bool>(checkedSelector.RequireNonNull(nameof(checkedSelector))(Item), converter.RequireNonNull(nameof(converter)), OnCheckedChanged));

        /// <summary>
        /// Binds the checked value using the specified <see cref="checkedBinding"/>.
        /// </summary>
        /// <param name="checkedBinding">The binding implemented <see cref="IDisposable"/> that is used to bind the checked value.</param>
        protected void BindChecked(IDisposable checkedBinding)
        {
            this.checkedBinding.IfPresent(_ => this.checkedBinding.Dispose());
            this.checkedBinding = checkedBinding;
        }

        private void OnCheckedChanged(bool newChecked) => ListViewItem.Checked = newChecked;

        /// <summary>
        /// Binds the observable property selected using the specified selector
        /// to the text property of the sub item of the <see cref="System.Windows.Forms.ListViewItem"/>
        /// at the specified index.
        /// </summary>
        /// <param name="index">
        /// The location of the sub item in the <see cref="System.Windows.Forms.ListViewItem"/> to bind the text property.
        /// </param>
        /// <param name="textSelector">The selector to select the observable property for the text value.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="textSelector"/> is <c>null</c>.
        /// </exception>
        public void BindText(int index, Func<TItem, ObservableProperty<string>> textSelector)
            => EnsureListViewSubItemContext(index).BindText(textSelector.RequireNonNull(nameof(textSelector)));

        /// <summary>
        /// Binds the observable property selected using the specified selector and converter
        /// to the text property of the sub item of the <see cref="System.Windows.Forms.ListViewItem"/>
        /// at the specified index.
        /// </summary>
        /// <typeparam name="T">The type of the value of the observable property.</typeparam>
        /// <param name="index">
        /// The location of the sub item in the <see cref="System.Windows.Forms.ListViewItem"/> to bind the text property.
        /// </param>
        /// <param name="textSelector">The selector to select the observable property for the text value.</param>
        /// <param name="converter">
        /// The converter to convert the value of the observable property to the string text value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="textSelector"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="converter"/> is <c>null</c>.
        /// </exception>
        public void BindText<T>(int index, Func<TItem, ObservableProperty<T>> textSelector, Func<T, string> converter)
            => EnsureListViewSubItemContext(index).BindText(textSelector.RequireNonNull(nameof(textSelector)), converter.RequireNonNull(nameof(converter)));

        /// <summary>
        /// Binds the observable property selected using the specified selector
        /// to the back color property of the sub item of the <see cref="System.Windows.Forms.ListViewItem"/>
        /// at the specified index.
        /// </summary>
        /// <param name="index">
        /// The location of the sub item in the <see cref="System.Windows.Forms.ListViewItem"/> to bind the back color property.
        /// </param>
        /// <param name="backColorSelector">The selector to select the observable property for the back color value.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="backColorSelector"/> is <c>null</c>.
        /// </exception>
        public void BindBackColor(int index, Func<TItem, ObservableProperty<Color>> backColorSelector)
            => EnsureListViewSubItemContext(index).BindBackColor(backColorSelector.RequireNonNull(nameof(backColorSelector)));

        /// <summary>
        /// Binds the observable property selected using the specified selector and converter
        /// to the back color property of the sub item of the <see cref="System.Windows.Forms.ListViewItem"/>
        /// at the specified index.
        /// </summary>
        /// <typeparam name="T">The type of the value of the observable property.</typeparam>
        /// <param name="index">
        /// The location of the sub item in the <see cref="System.Windows.Forms.ListViewItem"/> to bind the back color property.
        /// </param>
        /// <param name="backColorSelector">The selector to select the observable property for the back color value.</param>
        /// <param name="converter">
        /// The converter to convert the value of the observable property to the back color value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="backColorSelector"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="converter"/> is <c>null</c>.
        /// </exception>
        public void BindBackColor<T>(int index, Func<TItem, ObservableProperty<T>> backColorSelector, Func<T, Color> converter)
            => EnsureListViewSubItemContext(index).BindBackColor(backColorSelector.RequireNonNull(nameof(backColorSelector)), converter.RequireNonNull(nameof(converter)));

        /// <summary>
        /// Binds the observable property selected using the specified selector
        /// to the fore color property of the sub item of the <see cref="System.Windows.Forms.ListViewItem"/>
        /// at the specified index.
        /// </summary>
        /// <param name="index">
        /// The location of the sub item in the <see cref="System.Windows.Forms.ListViewItem"/> to bind the fore color property.
        /// </param>
        /// <param name="foreColorSelector">The selector to select the observable property for the fore color value.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="foreColorSelector"/> is <c>null</c>.
        /// </exception>
        public void BindForeColor(int index, Func<TItem, ObservableProperty<Color>> foreColorSelector)
            => EnsureListViewSubItemContext(index).BindForeColor(foreColorSelector.RequireNonNull(nameof(foreColorSelector)));

        /// <summary>
        /// Binds the observable property selected using the specified selector and converter
        /// to the fore color property of the sub item of the <see cref="System.Windows.Forms.ListViewItem"/>
        /// at the specified index.
        /// </summary>
        /// <typeparam name="T">The type of the value of the observable property.</typeparam>
        /// <param name="index">
        /// The location of the sub item in the <see cref="System.Windows.Forms.ListViewItem"/> to bind the fore color property.
        /// </param>
        /// <param name="foreColorSelector">The selector to select the observable property for the fore color value.</param>
        /// <param name="converter">
        /// The converter to convert the value of the observable property to the fore color value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="foreColorSelector"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="converter"/> is <c>null</c>.
        /// </exception>
        public void BindForeColor<T>(int index, Func<TItem, ObservableProperty<T>> foreColorSelector, Func<T, Color> converter)
            => EnsureListViewSubItemContext(index).BindForeColor(foreColorSelector.RequireNonNull(nameof(foreColorSelector)), converter.RequireNonNull(nameof(converter)));

        /// <summary>
        /// Binds the observable property selected using the specified selector
        /// to the font property of the sub item of the <see cref="System.Windows.Forms.ListViewItem"/>
        /// at the specified index.
        /// </summary>
        /// <param name="index">
        /// The location of the sub item in the <see cref="System.Windows.Forms.ListViewItem"/> to bind the font property.
        /// </param>
        /// <param name="fontSelector">The selector to select the observable property for the font value.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fontSelector"/> is <c>null</c>.
        /// </exception>
        public void BindFont(int index, Func<TItem, ObservableProperty<Font>> fontSelector)
            => EnsureListViewSubItemContext(index).BindFont(fontSelector.RequireNonNull(nameof(fontSelector)));

        /// <summary>
        /// Binds the observable property selected using the specified selector and converter
        /// to the font property of the sub item of the <see cref="System.Windows.Forms.ListViewItem"/>
        /// at the specified index.
        /// </summary>
        /// <typeparam name="T">The type of the value of the observable property.</typeparam>
        /// <param name="index">
        /// The location of the sub item in the <see cref="System.Windows.Forms.ListViewItem"/> to bind the font property.
        /// </param>
        /// <param name="fontSelector">The selector to select the observable property for the font value.</param>
        /// <param name="converter">
        /// The converter to convert the value of the observable property to the font value.
        /// </param>
        public void BindFont<T>(int index, Func<TItem, ObservableProperty<T>> fontSelector, Func<T, Font> converter)
            => EnsureListViewSubItemContext(index).BindFont(fontSelector, converter);

        /// <summary>
        /// Releases all resources used by this context
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all <see cref="ObservableProperty{T}.PropertyValueChanged"/> event handlers.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release all <see cref="ObservableProperty{T}.PropertyValueChanged"/> event handlers.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                checkedBinding.IfPresent(_ =>
                {
                    checkedBinding.Dispose();
                    checkedBinding = null;
                });
                listViewSubItemContexts.ForEach(listViewSubItemContext => listViewSubItemContext.Dispose());
            }
        }

        /// <summary>
        /// Ensures the <see cref="ListViewSubItemContext"/> at the specified index.
        /// </summary>
        /// <remarks>
        /// If the <see cref="ListViewSubItemContext"/> is not found at the specified index,
        /// a new instance of the <see cref="System.Windows.Forms.ListViewItem.ListViewSubItem"/>
        /// and <see cref="ListViewSubItemContext"/> are created and inserted into each collection.
        /// </remarks>
        /// <param name="index">The location of the sub item to ensure.</param>
        /// <returns>The <see cref="ListViewSubItemContext"/> at the specified index.</returns>
        protected ListViewSubItemContext EnsureListViewSubItemContext(int index)
        {
            if (listViewSubItemContexts.Count > index) return listViewSubItemContexts[index];

            var listViewSubItem = new ListViewItem.ListViewSubItem();
            ListViewItem.SubItems.Insert(index, listViewSubItem);

            var listViewSubItemContext = new ListViewSubItemContext(Item, listViewSubItem);
            EnsureListViewSubItemContextSpace(index);
            listViewSubItemContexts.Insert(index, listViewSubItemContext);

            return listViewSubItemContext;
        }

        private void EnsureListViewSubItemContextSpace(int insertedIndex)
        {
            if (listViewSubItemContexts.Count >= insertedIndex) return;

            for (var index = listViewSubItemContexts.Count; index < insertedIndex; ++index)
            {
                listViewSubItemContexts.Insert(index, new ListViewSubItemContext(Item, ListViewItem.SubItems[index]));
            }
        }

        /// <summary>
        /// Represents a context of the sub item of the item of the list view.
        /// </summary>
        protected class ListViewSubItemContext : IDisposable
        {
            /// <summary>
            /// Gets the item of the list view.
            /// </summary>
            public TItem Item { get; }

            /// <summary>
            /// Gets the <see cref="ListViewItem.ListViewSubItem"/> of the <see cref="System.Windows.Forms.ListViewItem"/>.
            /// </summary>
            public ListViewItem.ListViewSubItem ListViewSubItem { get; }

            private IDisposable textBinding;
            private IDisposable backColorBinding;
            private IDisposable foreColorBinding;
            private IDisposable fontBinding;

            /// <summary>
            /// Initializes a new instance of the <see cref="ListViewSubItemContext"/> class
            /// with the specified item and <see cref="ListViewItem.ListViewSubItem"/>.
            /// </summary>
            /// <param name="item">The item of the list view.</param>
            /// <param name="listViewSubItem">
            /// The <see cref="ListViewItem.ListViewSubItem"/> of the <see cref="System.Windows.Forms.ListViewItem"/>.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="item"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="listViewSubItem"/> is <c>null</c>.
            /// </exception>
            public ListViewSubItemContext(TItem item, ListViewItem.ListViewSubItem listViewSubItem)
            {
                Item = item.RequireNonNull(nameof(item));
                ListViewSubItem = listViewSubItem.RequireNonNull(nameof(listViewSubItem));
            }

            /// <summary>
            /// Binds the observable property selected using the specified selector
            /// to the text property of the <see cref="System.Windows.Forms.ListViewItem.ListViewSubItem"/>.
            /// </summary>
            /// <param name="textSelector">The selector to select the observable property for the text value.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="textSelector"/> is <c>null</c>.
            /// </exception>
            public void BindText(Func<TItem, ObservableProperty<string>> textSelector)
                => BindText(new ListViewItemBinding<string>(textSelector.RequireNonNull(nameof(textSelector))(Item), OnTextChanged));

            /// <summary>
            /// Binds the observable property selected using the specified selector and converter
            /// to the text property of the <see cref="System.Windows.Forms.ListViewItem.ListViewSubItem"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value of the observable property.</typeparam>
            /// <param name="textSelector">The selector to select the observable property for the text value.</param>
            /// <param name="converter">
            /// The converter to convert the value of the observable property to the string text value.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="textSelector"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="converter"/> is <c>null</c>.
            /// </exception>
            public void BindText<T>(Func<TItem, ObservableProperty<T>> textSelector, Func<T, string> converter)
                => BindText(new ListViewItemBinding<T, string>(textSelector.RequireNonNull(nameof(textSelector))(Item), converter.RequireNonNull(nameof(converter)), OnTextChanged));

            /// <summary>
            /// Binds the text value using the specified <see cref="textBinding"/>.
            /// </summary>
            /// <param name="textBinding">The binding implemented <see cref="IDisposable"/> that is used to bind the text value.</param>
            protected void BindText(IDisposable textBinding)
            {
                this.textBinding.IfPresent(_ => this.textBinding.Dispose());
                this.textBinding = textBinding;
            }

            private void OnTextChanged(string newText) => ListViewSubItem.Text = newText;

            /// <summary>
            /// Binds the observable property selected using the specified selector
            /// to the back color property of the <see cref="System.Windows.Forms.ListViewItem.ListViewSubItem"/>.
            /// </summary>
            /// <param name="backColorSelector">The selector to select the observable property for the back color value.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="backColorSelector"/> is <c>null</c>.
            /// </exception>
            public void BindBackColor(Func<TItem, ObservableProperty<Color>> backColorSelector)
                => BindBackColor(new ListViewItemBinding<Color>(backColorSelector.RequireNonNull(nameof(backColorSelector))(Item), OnBackColorChanged));

            /// <summary>
            /// Binds the observable property selected using the specified selector and converter
            /// to the back color property of the <see cref="System.Windows.Forms.ListViewItem.ListViewSubItem"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value of the observable property.</typeparam>
            /// <param name="backColorSelector">The selector to select the observable property for the back color value.</param>
            /// <param name="converter">
            /// The converter to convert the value of the observable property to the back color value.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="backColorSelector"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="converter"/> is <c>null</c>.
            /// </exception>
            public void BindBackColor<T>(Func<TItem, ObservableProperty<T>> backColorSelector, Func<T, Color> converter)
                => BindBackColor(new ListViewItemBinding<T, Color>(backColorSelector.RequireNonNull(nameof(backColorSelector))(Item), converter.RequireNonNull(nameof(converter)), OnBackColorChanged));

            /// <summary>
            /// Binds the back color value using the specified <see cref="backColorBinding"/>.
            /// </summary>
            /// <param name="backColorBinding">The binding implemented <see cref="IDisposable"/> that is used to bind the back color value.</param>
            protected void BindBackColor(IDisposable backColorBinding)
            {
                this.backColorBinding.IfPresent(_ => this.backColorBinding.Dispose());
                this.backColorBinding = backColorBinding;
            }

            private void OnBackColorChanged(Color newBackColor) => ListViewSubItem.BackColor = newBackColor;

            /// <summary>
            /// Binds the observable property selected using the specified selector
            /// to the fore color property of the <see cref="System.Windows.Forms.ListViewItem.ListViewSubItem"/>.
            /// </summary>
            /// <param name="foreColorSelector">The selector to select the observable property for the fore color value.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="foreColorSelector"/> is <c>null</c>.
            /// </exception>
            public void BindForeColor(Func<TItem, ObservableProperty<Color>> foreColorSelector)
                => BindForeColor(new ListViewItemBinding<Color>(foreColorSelector.RequireNonNull(nameof(foreColorSelector))(Item), OnForeColorChanged));

            /// <summary>
            /// Binds the observable property selected using the specified selector and converter
            /// to the fore color property of the <see cref="System.Windows.Forms.ListViewItem.ListViewSubItem"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value of the observable property.</typeparam>
            /// <param name="foreColorSelector">The selector to select the observable property for the fore color value.</param>
            /// <param name="converter">
            /// The converter to convert the value of the observable property to the fore color value.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="foreColorSelector"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="converter"/> is <c>null</c>.
            /// </exception>
            public void BindForeColor<T>(Func<TItem, ObservableProperty<T>> foreColorSelector, Func<T, Color> converter)
                => BindForeColor(new ListViewItemBinding<T, Color>(foreColorSelector.RequireNonNull(nameof(foreColorSelector))(Item), converter.RequireNonNull(nameof(converter)), OnForeColorChanged));

            /// <summary>
            /// Binds the fore color value using the specified <see cref="foreColorBinding"/>.
            /// </summary>
            /// <param name="foreColorBinding">The binding implemented <see cref="IDisposable"/> that is used to bind the fore color value.</param>
            protected void BindForeColor(IDisposable foreColorBinding)
            {
                this.foreColorBinding.IfPresent(_ => this.foreColorBinding.Dispose());
                this.foreColorBinding = foreColorBinding;
            }

            private void OnForeColorChanged(Color newForeColor) => ListViewSubItem.ForeColor = newForeColor;

            /// <summary>
            /// Binds the observable property selected using the specified selector
            /// to the font property of the <see cref="System.Windows.Forms.ListViewItem.ListViewSubItem"/>.
            /// </summary>
            /// <param name="fontSelector">The selector to select the observable property for the font value.</param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="fontSelector"/> is <c>null</c>.
            /// </exception>
            public void BindFont(Func<TItem, ObservableProperty<Font>> fontSelector)
                => BindFont(new ListViewItemBinding<Font>(fontSelector.RequireNonNull(nameof(fontSelector))(Item), OnFontChanged));

            /// <summary>
            /// Binds the observable property selected using the specified selector and converter
            /// to the font property of the <see cref="System.Windows.Forms.ListViewItem.ListViewSubItem"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value of the observable property.</typeparam>
            /// <param name="fontSelector">The selector to select the observable property for the font value.</param>
            /// <param name="converter">
            /// The converter to convert the value of the observable property to the font value.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="fontSelector"/> is <c>null</c>.
            /// </exception>
            /// <exception cref="ArgumentNullException">
            /// <paramref name="converter"/> is <c>null</c>.
            /// </exception>
            public void BindFont<T>(Func<TItem, ObservableProperty<T>> fontSelector, Func<T, Font> converter)
                => BindFont(new ListViewItemBinding<T, Font>(fontSelector.RequireNonNull(nameof(fontSelector))(Item), converter.RequireNonNull(nameof(converter)), OnFontChanged));

            /// <summary>
            /// Binds the font value using the specified <see cref="fontBinding"/>.
            /// </summary>
            /// <param name="fontBinding">The binding implemented <see cref="IDisposable"/> that is used to the font value.</param>
            protected void BindFont(IDisposable fontBinding)
            {
                this.fontBinding.IfPresent(_ => this.fontBinding.Dispose());
                this.fontBinding = fontBinding;
            }

            private void OnFontChanged(Font newFont) => ListViewSubItem.Font = newFont;

            /// <summary>
            /// Releases all resources used by this context.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Releases all <see cref="ObservableProperty{T}.PropertyValueChanged"/> event handlers.
            /// </summary>
            /// <param name="disposing">
            /// <c>true</c> to release all <see cref="ObservableProperty{T}.PropertyValueChanged"/> event handlers.
            /// </param>
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    textBinding.IfPresent(_ =>
                    {
                        textBinding.Dispose();
                        textBinding = null;
                    });
                    backColorBinding.IfPresent(_ =>
                    {
                        backColorBinding.Dispose();
                        backColorBinding = null;
                    });
                    foreColorBinding.IfPresent(_ =>
                    {
                        foreColorBinding.Dispose();
                        foreColorBinding = null;
                    });
                    fontBinding.IfPresent(_ =>
                    {
                        fontBinding.Dispose();
                        fontBinding = null;
                    });
                }
            }
        }
    }
}

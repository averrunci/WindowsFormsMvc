// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms
{
    internal class ListViewItemBinding<TSource, TTarget> : ListViewItemBinding<TSource>
    {
        private readonly Func<TSource, TTarget> converter;
        private readonly Action<TTarget> valueChangedAction;

        public ListViewItemBinding(ObservableProperty<TSource> observableProperty, Func<TSource, TTarget> converter, Action<TTarget> valueChangedAction) : base(observableProperty, null)
        {
            this.converter = converter.RequireNonNull(nameof(converter));
            this.valueChangedAction = valueChangedAction;

            this.valueChangedAction?.Invoke(converter(observableProperty.Value));
        }

        protected override void OnValueChanged(TSource newValue)
        {
            valueChangedAction?.Invoke(converter(newValue));
        }
    }

    internal class ListViewItemBinding<T> : IDisposable
    {
        private readonly ObservableProperty<T> observableProperty;
        private readonly Action<T> valueChangedAction;

        public ListViewItemBinding(ObservableProperty<T> observableProperty, Action<T> valueChangedAction)
        {
            this.observableProperty = observableProperty.RequireNonNull(nameof(observableProperty));
            this.valueChangedAction = valueChangedAction;

            this.observableProperty.PropertyValueChanged += OnValueChanged;

            this.valueChangedAction?.Invoke(observableProperty.Value);
        }

        protected virtual void OnValueChanged(T newValue)
        {
            valueChangedAction?.Invoke(newValue);
        }

        private void OnValueChanged(object sender, PropertyValueChangedEventArgs<T> e)
        {
            OnValueChanged(e.NewValue);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                observableProperty.PropertyValueChanged -= OnValueChanged;
            }
        }
    }
}

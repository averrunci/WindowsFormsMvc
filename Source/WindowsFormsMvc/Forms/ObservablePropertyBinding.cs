// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms
{
    internal sealed class ObservablePropertyBinding<TSource, TTarget> : IObservablePropertyBinding
    {
        private readonly Control control;
        private readonly ObservableProperty<TSource> observableProperty;
        private readonly Func<TSource, TTarget> converter;
        private readonly Func<TTarget, TSource> backConverter;
        private readonly PropertyDescriptor controlPropertyDescriptor;

        private bool bound;

        public ObservablePropertyBinding(ObservableProperty<TSource> observableProperty, Control control, string controlPropertyName) : this(observableProperty, control, controlPropertyName, null)
        {
        }

        public ObservablePropertyBinding(ObservableProperty<TSource> observableProperty, Control control, string controlPropertyName, Func<TSource, TTarget> converter) : this(observableProperty, control, controlPropertyName, converter, null)
        {
        }

        public ObservablePropertyBinding(ObservableProperty<TSource> observableProperty, Control control, string controlPropertyName, Func<TSource, TTarget> converter, Func<TTarget, TSource> backConverter)
        {
            this.observableProperty = observableProperty.RequireNonNull(nameof(observableProperty));
            this.control = control.RequireNonNull(nameof(control));
            this.converter = converter;
            this.backConverter = backConverter;

            controlPropertyDescriptor = TypeDescriptor.GetProperties(control).OfType<PropertyDescriptor>().FirstOrDefault(descriptor => string.Equals(descriptor.Name, controlPropertyName, StringComparison.OrdinalIgnoreCase));
        }

        public void Bind()
        {
            if (bound || controlPropertyDescriptor == null) return;

            observableProperty.PropertyValueChanged += OnObservablePropertyValueChanged;
            SetValueToControl(observableProperty.Value);

            bound = true;
        }

        public void BindTwoWay()
        {
            if (converter != null && backConverter == null) throw new InvalidOperationException("When the converter is specified, the back converter must be specified.");
            if (converter == null && backConverter != null) throw new InvalidOperationException("When the back converter is specified, the converter must be specified.");
            if (bound || controlPropertyDescriptor == null) return;

            observableProperty.PropertyValueChanged += OnObservablePropertyValueChanged;
            controlPropertyDescriptor.AddValueChanged(control, OnControlPropertyValueChanged);
            SetValueToControl(observableProperty.Value);

            bound = true;
        }

        public void BindToSource()
        {
            if (bound || controlPropertyDescriptor == null) return;

            controlPropertyDescriptor.AddValueChanged(control, OnControlPropertyValueChanged);
            SetValueToObservableProperty(controlPropertyDescriptor.GetValue(control));

            bound = true;
        }

        public void Unbind()
        {
            if (!bound || controlPropertyDescriptor == null) return;

            observableProperty.PropertyValueChanged -= OnObservablePropertyValueChanged;
            controlPropertyDescriptor.RemoveValueChanged(control, OnControlPropertyValueChanged);

            bound = false;
        }

        private void SetValueToControl(TSource observablePropertyValue)
        {
            if (converter == null)
            {
                controlPropertyDescriptor.SetValue(control, observablePropertyValue);
                return;
            }

            controlPropertyDescriptor.SetValue(control, converter(observablePropertyValue));
        }

        private void SetValueToObservableProperty(object value)
        {
            if (backConverter == null)
            {
                observableProperty.Value = (TSource)value;
                return;
            }

            observableProperty.Value = backConverter((TTarget)value);
        }

        private void OnObservablePropertyValueChanged(object sender, PropertyValueChangedEventArgs<TSource> e)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(new Action<object, PropertyValueChangedEventArgs<TSource>>(OnObservablePropertyValueChanged), sender, e);
                return;
            }

            SetValueToControl(e.NewValue);
        }

        private void OnControlPropertyValueChanged(object sender, EventArgs e)
        {
            SetValueToObservableProperty(controlPropertyDescriptor.GetValue(control));
        }
    }
}

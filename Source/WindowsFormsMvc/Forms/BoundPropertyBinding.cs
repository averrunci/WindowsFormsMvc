// Copyright (C) 2019 Fievus
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
    internal class BoundPropertyBinding<TSource, TTarget> : IBindablePropertyBinding
    {
        private readonly Control control;
        private readonly BoundProperty<TSource> boundProperty;
        private readonly Func<TSource, TTarget> converter;
        private readonly PropertyDescriptor controlPropertyDescriptor;

        private bool bound;

        public BoundPropertyBinding(BoundProperty<TSource> boundProperty, Control control, string controlPropertyName) : this(boundProperty, control, controlPropertyName, null)
        {
        }

        public BoundPropertyBinding(BoundProperty<TSource> boundProperty, Control control, string controlPropertyName, Func<TSource, TTarget> converter)
        {
            this.boundProperty = boundProperty.RequireNonNull(nameof(boundProperty));
            this.control = control.RequireNonNull(nameof(control));
            this.converter = converter;

            controlPropertyDescriptor = TypeDescriptor.GetProperties(control).OfType<PropertyDescriptor>().FirstOrDefault(descriptor => string.Equals(descriptor.Name, controlPropertyName, StringComparison.OrdinalIgnoreCase));
        }

        public void Bind()
        {
            if (bound || controlPropertyDescriptor == null) return;

            boundProperty.PropertyValueChanged += OnBoundPropertyValueChanged;
            SetValueToControl(boundProperty.Value);

            bound = true;
        }

        public void Unbind()
        {
            if (!bound || controlPropertyDescriptor == null) return;

            boundProperty.PropertyValueChanged -= OnBoundPropertyValueChanged;

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


        private void OnBoundPropertyValueChanged(object sender, PropertyValueChangedEventArgs<TSource> e)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(new Action<object, PropertyValueChangedEventArgs<TSource>>(OnBoundPropertyValueChanged), sender, e);
                return;
            }

            SetValueToControl(e.NewValue);
        }
    }
}

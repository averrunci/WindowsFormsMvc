// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms;

internal class BoundPropertyBinding<TSource, TTarget> : IBindablePropertyBinding
{
    private readonly Control control;
    private readonly BoundProperty<TSource> boundProperty;
    private readonly Func<TSource, TTarget?>? converter;
    private readonly PropertyDescriptor? controlPropertyDescriptor;

    private bool bound;

    public BoundPropertyBinding(BoundProperty<TSource> boundProperty, Control control, string controlPropertyName)
    {
        this.boundProperty = boundProperty;
        this.control = control;

        controlPropertyDescriptor = TypeDescriptor.GetProperties(control).OfType<PropertyDescriptor>().FirstOrDefault(descriptor => string.Equals(descriptor.Name, controlPropertyName, StringComparison.OrdinalIgnoreCase));
    }

    public BoundPropertyBinding(BoundProperty<TSource> boundProperty, Control control, string controlPropertyName, Func<TSource, TTarget?> converter) : this(boundProperty, control, controlPropertyName)
    {
        this.converter = converter;
    }

    public void Bind()
    {
        if (controlPropertyDescriptor is null) return;

        if (bound) Unbind();

        boundProperty.PropertyValueChanged += OnBoundPropertyValueChanged;
        SetValueToControl(boundProperty.Value);

        bound = true;
    }

    public void Unbind()
    {
        if (!bound || controlPropertyDescriptor is null) return;

        boundProperty.PropertyValueChanged -= OnBoundPropertyValueChanged;

        bound = false;
    }

    private void SetValueToControl(TSource observablePropertyValue)
    {
        controlPropertyDescriptor?.SetValue(control, converter is null ? observablePropertyValue : converter(observablePropertyValue));
    }


    private void OnBoundPropertyValueChanged(object? sender, PropertyValueChangedEventArgs<TSource> e)
    {
        if (control.InvokeRequired)
        {
            control.BeginInvoke(new Action<object?, PropertyValueChangedEventArgs<TSource>>(OnBoundPropertyValueChanged), sender, e);
            return;
        }

        SetValueToControl(e.NewValue);
    }
}
// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms;

internal sealed class ObservablePropertyBinding<TSource, TTarget> : IObservablePropertyBinding
{
    private readonly Control control;
    private readonly ObservableProperty<TSource> observableProperty;
    private readonly Func<TSource, TTarget>? converter;
    private readonly Func<TTarget, TSource>? backConverter;
    private readonly PropertyDescriptor? controlPropertyDescriptor;

    private bool bound;

    public ObservablePropertyBinding(ObservableProperty<TSource> observableProperty, Control control, string controlPropertyName)
    {
        this.observableProperty = observableProperty;
        this.control = control;

        controlPropertyDescriptor = TypeDescriptor.GetProperties(control).OfType<PropertyDescriptor>().FirstOrDefault(descriptor => string.Equals(descriptor.Name, controlPropertyName, StringComparison.OrdinalIgnoreCase));
    }

    public ObservablePropertyBinding(ObservableProperty<TSource> observableProperty, Control control, string controlPropertyName, Func<TSource, TTarget> converter) : this(observableProperty, control, controlPropertyName)
    {
        this.converter = converter;
    }

    public ObservablePropertyBinding(ObservableProperty<TSource> observableProperty, Control control, string controlPropertyName, Func<TTarget, TSource> backConverter) : this(observableProperty, control, controlPropertyName)
    {
        this.backConverter = backConverter;
    }

    public ObservablePropertyBinding(ObservableProperty<TSource> observableProperty, Control control, string controlPropertyName, Func<TSource, TTarget> converter, Func<TTarget, TSource> backConverter) : this(observableProperty, control, controlPropertyName)
    {
        this.converter = converter;
        this.backConverter = backConverter;
    }

    public void Bind()
    {
        if (bound || controlPropertyDescriptor is null) return;

        observableProperty.PropertyValueChanged += OnObservablePropertyValueChanged;
        SetValueToControl(observableProperty.Value);

        bound = true;
    }

    public void BindTwoWay()
    {
        if (converter is not null && backConverter is null) throw new InvalidOperationException("When the converter is specified, the back converter must be specified.");
        if (converter is null && backConverter is not null) throw new InvalidOperationException("When the back converter is specified, the converter must be specified.");
        if (bound || controlPropertyDescriptor is null) return;

        observableProperty.PropertyValueChanged += OnObservablePropertyValueChanged;
        controlPropertyDescriptor.AddValueChanged(control, OnControlPropertyValueChanged);
        SetValueToControl(observableProperty.Value);

        bound = true;
    }

    public void BindToSource()
    {
        if (bound || controlPropertyDescriptor is null) return;

        controlPropertyDescriptor.AddValueChanged(control, OnControlPropertyValueChanged);
        SetValueToObservableProperty(controlPropertyDescriptor.GetValue(control));

        bound = true;
    }

    public void Unbind()
    {
        if (!bound || controlPropertyDescriptor is null) return;

        observableProperty.PropertyValueChanged -= OnObservablePropertyValueChanged;
        controlPropertyDescriptor.RemoveValueChanged(control, OnControlPropertyValueChanged);

        bound = false;
    }

    private void SetValueToControl(TSource observablePropertyValue)
    {
        controlPropertyDescriptor?.SetValue(control, converter is null ? observablePropertyValue : converter(observablePropertyValue));
    }

    private void SetValueToObservableProperty(object? value)
    {
        observableProperty.Value = backConverter is null ? (TSource)value! : backConverter((TTarget)value!);
    }

    private void OnObservablePropertyValueChanged(object? sender, PropertyValueChangedEventArgs<TSource> e)
    {
        if (control.InvokeRequired)
        {
            control.BeginInvoke(new Action<object?, PropertyValueChangedEventArgs<TSource>>(OnObservablePropertyValueChanged), sender, e);
            return;
        }

        SetValueToControl(e.NewValue);
    }

    private void OnControlPropertyValueChanged(object? sender, EventArgs e)
    {
        if (controlPropertyDescriptor is null) return;

        SetValueToObservableProperty(controlPropertyDescriptor.GetValue(control));
    }
}
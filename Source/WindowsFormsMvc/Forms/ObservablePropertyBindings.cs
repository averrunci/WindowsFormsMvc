// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms;

/// <summary>
/// Provides the binding between the value of an observable property and the property value of a control.
/// </summary>
public class ObservablePropertyBindings
{
    private readonly List<IBindablePropertyBinding> bindings = new();

    /// <summary>
    /// Binds the specified bound property to the specified property of the control.
    /// </summary>
    /// <typeparam name="T">The type of the binding value.</typeparam>
    /// <param name="boundProperty">The bound property that is bound to the property of the control.</param>
    /// <param name="control">The control that has the property to which the bound property is bound.</param>
    /// <param name="controlPropertyName">The name of the property to which the bound property is bound.</param>
    /// <returns>
    /// The <see cref="IBindablePropertyBinding"/> that provides the binding the value of the bound property
    /// and the property value of the control.
    /// </returns>
    public IBindablePropertyBinding Bind<T>(BoundProperty<T> boundProperty, Control control, string controlPropertyName)
        => Bind(new BoundPropertyBinding<T, T>(boundProperty, control, controlPropertyName));

    /// <summary>
    /// Binds the specified bound property to the specified property of the control
    /// using the specified converter that converts the value of the bound property
    /// to the value of the property of the control.
    /// </summary>
    /// <typeparam name="TSource">The type of the binding source value.</typeparam>
    /// <typeparam name="TTarget">The type of the binding target value.</typeparam>
    /// <param name="boundProperty">The bound property that is bound to the property of the control.</param>
    /// <param name="control">The control that has the property to which the bound property is bound.</param>
    /// <param name="controlPropertyName">The name of the property to which the bound property is bound.</param>
    /// <param name="converter">The converter to convert the binding source value to the binding target value.</param>
    /// <returns>
    /// The <see cref="IBindablePropertyBinding"/> that provides the binding the value of the bound property
    /// and the property value of the control.
    /// </returns>
    public IBindablePropertyBinding Bind<TSource, TTarget>(BoundProperty<TSource> boundProperty, Control control, string controlPropertyName, Func<TSource, TTarget> converter)
        => Bind(new BoundPropertyBinding<TSource, TTarget>(boundProperty, control, controlPropertyName, converter));

    /// <summary>
    /// Binds the specified observable property to the specified property of the control.
    /// </summary>
    /// <typeparam name="T">The type of the binding value.</typeparam>
    /// <param name="observableProperty">The observable property that is bound to the property of the control.</param>
    /// <param name="control">The control that has the property to which the observable property is bound.</param>
    /// <param name="controlPropertyName">The name of the property to which the observable property is bound.</param>
    /// <returns>
    /// The <see cref="IBindablePropertyBinding"/> that provides the binding the value of the observable property
    /// and the property value of the control.
    /// </returns>
    public IBindablePropertyBinding Bind<T>(ObservableProperty<T> observableProperty, Control control, string controlPropertyName)
        => Bind(new ObservablePropertyBinding<T, T>(observableProperty, control, controlPropertyName));

    /// <summary>
    /// Binds the specified observable property to the specified property of the control
    /// using the specified converter that converts the value of the observable property
    /// to the value of the property of the control.
    /// </summary>
    /// <typeparam name="TSource">The type of the binding source value.</typeparam>
    /// <typeparam name="TTarget">The type of the binding target value.</typeparam>
    /// <param name="observableProperty">The observable property that is bound to the property of the control.</param>
    /// <param name="control">The control that has the property to which the observable property is bound.</param>
    /// <param name="controlPropertyName">The name of the property to which the observable property is bound.</param>
    /// <param name="converter">The converter to convert the binding source value to the binding target value.</param>
    /// <returns>
    /// The <see cref="IBindablePropertyBinding"/> that provides the binding the value of the observable property
    /// and the property value of the control.
    /// </returns>
    public IBindablePropertyBinding Bind<TSource, TTarget>(ObservableProperty<TSource> observableProperty, Control control, string controlPropertyName, Func<TSource, TTarget> converter)
        => Bind(new ObservablePropertyBinding<TSource, TTarget>(observableProperty, control, controlPropertyName, converter));

    /// <summary>
    /// Binds the values using the specified <see cref="IBindablePropertyBinding"/>.
    /// </summary>
    /// <param name="binding">
    /// The <see cref="IBindablePropertyBinding"/> that is used to bind the value.
    /// </param>
    /// <returns>
    /// The <see cref="IBindablePropertyBinding"/> that provides the binding the value of the bindable property
    /// and the property value of the control.
    /// </returns>
    public IBindablePropertyBinding Bind(IBindablePropertyBinding binding)
    {
        binding.Bind();
        bindings.Add(binding);
        return binding;
    }

    /// <summary>
    /// Binds the specified observable property to the specified property of the control
    /// to update the other when either the value of the observable property or the value
    /// of the property of the control is changed.
    /// </summary>
    /// <typeparam name="T">The type of the binding value.</typeparam>
    /// <param name="observableProperty">The observable property that is bound to the property of the control.</param>
    /// <param name="control">The control that has the property to which the observable property is bound.</param>
    /// <param name="controlPropertyName">The name of the property to which the observable property is bound.</param>
    /// <returns>
    /// The <see cref="IObservablePropertyBinding"/> that provides the binding the value of the observable property
    /// and the property value of the control.
    /// </returns>
    public IObservablePropertyBinding BindTwoWay<T>(ObservableProperty<T> observableProperty, Control control, string controlPropertyName)
        => BindTwoWay(new ObservablePropertyBinding<T, T>(observableProperty, control, controlPropertyName));

    /// <summary>
    /// Binds the specified observable property to the specified property of the control
    /// to update the other when either the value of the observable property or the value
    /// of the property of the control is changed using the specified converter that converts
    /// the value of the observable property to the value of the property of the control and
    /// the specified back converter that converts the value of the property of the control
    /// back to the value of the observable property.
    /// </summary>
    /// <typeparam name="TSource">The type of the binding source value.</typeparam>
    /// <typeparam name="TTarget">The type of the binding target value.</typeparam>
    /// <param name="observableProperty">The observable property that is bound to the property of the control.</param>
    /// <param name="control">The control that has the property to which the observable property is bound.</param>
    /// <param name="controlPropertyName">The name of the property to which the observable property is bound.</param>
    /// <param name="converter">The converter to convert the binding source value to the binding target value.</param>
    /// <param name="backConverter">The converter to convert the binding target value back to the binding source value.</param>
    /// <returns>
    /// The <see cref="IObservablePropertyBinding"/> that provides the binding the value of the observable property
    /// and the property value of the control.
    /// </returns>
    public IObservablePropertyBinding BindTwoWay<TSource, TTarget>(ObservableProperty<TSource> observableProperty, Control control, string controlPropertyName, Func<TSource, TTarget> converter, Func<TTarget, TSource> backConverter)
        => BindTwoWay(new ObservablePropertyBinding<TSource, TTarget>(observableProperty, control, controlPropertyName, converter, backConverter));

    /// <summary>
    /// Binds the values in two way direction using the specified <see cref="IObservablePropertyBinding"/>.
    /// </summary>
    /// <param name="binding">
    /// The <see cref="IObservablePropertyBinding"/> that is used to bind the value.
    /// </param>
    /// <returns>
    /// The <see cref="IObservablePropertyBinding"/> that provides the binding the value of the observable property
    /// and the property value of the control.
    /// </returns>
    public IObservablePropertyBinding BindTwoWay(IObservablePropertyBinding binding)
    {
        binding.BindTwoWay();
        bindings.Add(binding);
        return binding;
    }

    /// <summary>
    /// Binds the specified observable property to the specified property of the control
    /// to update the value of the observable property when the value of the property of
    /// the control is changed.
    /// </summary>
    /// <typeparam name="T">The type of the binding value.</typeparam>
    /// <param name="observableProperty">The observable property that is bound to the property of the control.</param>
    /// <param name="control">The control that has the property to which the observable property is bound.</param>
    /// <param name="controlPropertyName">The name of the property to which the observable property is bound.</param>
    /// <returns>
    ///  The <see cref="IObservablePropertyBinding"/> that provides the binding the value of the observable property
    /// and the property value of the control.
    /// </returns>
    public IObservablePropertyBinding BindToSource<T>(ObservableProperty<T> observableProperty, Control control, string controlPropertyName)
        => BindToSource(new ObservablePropertyBinding<T, T>(observableProperty, control, controlPropertyName));

    /// <summary>
    /// Binds the specified observable property to the specified property of the control
    /// to update the value of the observable property when the value of the property of
    /// the control is changed using the specified back converter that converts the value
    /// of the property of the control back to the value of the observable property.
    /// </summary>
    /// <typeparam name="TSource">The type of the binding source value.</typeparam>
    /// <typeparam name="TTarget">The type of the binding target value.</typeparam>
    /// <param name="observableProperty">The observable property that is bound to the property of the control.</param>
    /// <param name="control">The control that has the property to which the observable property is bound.</param>
    /// <param name="controlPropertyName">The name of the property to which the observable property is bound.</param>
    /// <param name="backConverter">The converter to convert the binding target value back to the binding source value.</param>
    /// <returns>
    /// The <see cref="IObservablePropertyBinding"/> that provides the binding the value of the observable property
    /// and the property value of the control.
    /// </returns>
    public IObservablePropertyBinding BindToSource<TSource, TTarget>(ObservableProperty<TSource> observableProperty, Control control, string controlPropertyName, Func<TTarget, TSource> backConverter)
        => BindToSource(new ObservablePropertyBinding<TSource, TTarget>(observableProperty, control, controlPropertyName, backConverter));

    /// <summary>
    /// Binds the values in the source direction using the specified <see cref="IObservablePropertyBinding"/>.
    /// </summary>
    /// <param name="binding">
    /// The <see cref="IObservablePropertyBinding"/> that is used to bind the value.
    /// </param>
    /// <returns>
    /// The <see cref="IObservablePropertyBinding"/> that provides the binding the value of the observable property
    /// and the property value of the control.
    /// </returns>
    public IObservablePropertyBinding BindToSource(IObservablePropertyBinding binding)
    {
        binding.BindToSource();
        bindings.Add(binding);
        return binding;
    }

    /// <summary>
    /// Unbinds all bindings.
    /// </summary>
    public void Unbind()
    {
        bindings.ForEach(binding => binding.Unbind());
    }
}
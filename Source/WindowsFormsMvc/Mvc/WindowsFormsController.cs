// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using System.Reflection;

namespace Charites.Windows.Mvc;

/// <summary>
/// Provides functions for the controllers; a data context injection,
/// controls injection, and event handlers injection.
/// </summary>
public class WindowsFormsController : Component
{
    /// <summary>
    /// Occurs when an exception is not handled in event handlers.
    /// </summary>
    public static event UnhandledExceptionEventHandler? UnhandledException;

    /// <summary>
    /// Gets or sets the default finder to find a data context defined in a view.
    /// </summary>
    public static IWindowsFormsDataContextFinder DefaultDataContextFinder
    {
        get => defaultDataContextFinder;
        set
        {
            defaultDataContextFinder = value;
            EnsureDefaultControllerTypeFinder();
        }
    }
    private static IWindowsFormsDataContextFinder defaultDataContextFinder = new WindowsFormsDataContextFinder();

    /// <summary>
    /// Gets or sets the default injector to inject a data context to a controller.
    /// </summary>
    public static IDataContextInjector DefaultDataContextInjector { get; set; } = new DataContextInjector();

    /// <summary>
    /// Gets or sets the default finder to find a control in a view.
    /// </summary>
    public static IWindowsFormsControlFinder DefaultControlFinder
    {
        get => defaultControlFinder;
        set
        {
            defaultControlFinder = value;
            EnsureDefaultControlInjector();
        }
    }
    private static IWindowsFormsControlFinder defaultControlFinder = new WindowsFormsControlFinder();

    /// <summary>
    /// Gets or sets the default finder to find a key of a control.
    /// </summary>
    public static IWindowsFormsControlKeyFinder DefaultControlKeyFinder
    {
        get => defaultControlKeyFinder;
        set
        {
            defaultControlKeyFinder = value;
            EnsureDefaultControllerTypeFinder();
        }
    }
    private static IWindowsFormsControlKeyFinder defaultControlKeyFinder = new WindowsFormsControlKeyFinder();

    /// <summary>
    /// Gets or sets the default injector to inject a elements in a view to a controller.
    /// </summary>
    public static IWindowsFormsControlInjector DefaultControlInjector { get; set; } = new WindowsFormsControlInjector(DefaultControlFinder);

    /// <summary>
    /// Gets or sets the default finder to find a type of a controller that controls a view.
    /// </summary>
    public static IWindowsFormsControllerTypeFinder DefaultControllerTypeFinder { get; set; } = new WindowsFormsControllerTypeFinder(DefaultControlKeyFinder, DefaultDataContextFinder);

    /// <summary>
    /// Gets or sets the default factory to create a controller.
    /// </summary>
    public static IWindowsFormsControllerFactory DefaultControllerFactory { get; set; } = new WindowsFormsControllerFactory();

    /// <summary>
    /// Gets the extensions.
    /// </summary>
    protected static List<IWindowsFormsControllerExtension> Extensions { get; } = new();

    /// <summary>
    /// Gets or sets the finder to find a data context defined in a view.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IWindowsFormsDataContextFinder DataContextFinder
    {
        get => dataContextFinder;
        set
        {
            dataContextFinder = value;
            EnsureControllerTypeFinder();
        }
    }
    private IWindowsFormsDataContextFinder dataContextFinder = DefaultDataContextFinder;

    /// <summary>
    /// Gets or sets the injector to inject a data context to a controller.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IDataContextInjector DataContextInjector { get; set; } = DefaultDataContextInjector;

    /// <summary>
    /// Gets or set the finder to find a control in a view.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IWindowsFormsControlFinder ControlFinder
    {
        get => controlFinder;
        set
        {
            controlFinder = value;
            EnsureControlInjector();
        }
    }
    private IWindowsFormsControlFinder controlFinder = DefaultControlFinder;

    /// <summary>
    /// Gets or sets the finder to find a key of a control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IWindowsFormsControlKeyFinder ControlKeyFinder
    {
        get => controlKeyFinder;
        set
        {
            controlKeyFinder = value;
            EnsureControllerTypeFinder();
        }
    }
    private IWindowsFormsControlKeyFinder controlKeyFinder = DefaultControlKeyFinder;

    /// <summary>
    /// Gets or sets the injector to inject controls in a view to a controller.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IWindowsFormsControlInjector ControlInjector { get; set; } = DefaultControlInjector;

    /// <summary>
    /// Gets or sets the finder to find a type of a controller that controls a view.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IWindowsFormsControllerTypeFinder ControllerTypeFinder { get; set; } = DefaultControllerTypeFinder;

    /// <summary>
    /// Gets or sets the factory to create a controller.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IWindowsFormsControllerFactory ControllerFactory { get; set; } = DefaultControllerFactory;

    /// <summary>
    /// Gets or sets the view controlled by a controller.
    /// </summary>
    public Control? View
    {
        get => view;
        set
        {
            DetachControllersFrom(view);
            view = value;
            AttachControllersTo(view);
        }
    }
    private Control? view;

    /// <summary>
    /// Gets or sets the collection of the controllers.
    /// </summary>
    protected WindowsFormsControllerCollection? Controllers { get; set; }

    static WindowsFormsController()
    {
        Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(IWindowsFormsControllerExtension).IsAssignableFrom(t))
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => Activator.CreateInstance(t) as IWindowsFormsControllerExtension)
            .Where(extension => extension is not null)
            .ForEach(extension => AddExtension(extension!));
    }

    private static void EnsureDefaultControlInjector()
    {
        DefaultControlInjector = new WindowsFormsControlInjector(DefaultControlFinder);
    }
    private static void EnsureDefaultControllerTypeFinder()
    {
        DefaultControllerTypeFinder = new WindowsFormsControllerTypeFinder(DefaultControlKeyFinder, DefaultDataContextFinder);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowsFormsController"/> class.
    /// </summary>
    public WindowsFormsController()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowsFormsController"/> class
    /// and adds the <see cref="WindowsFormsController"/> to the specified container.
    /// </summary>
    /// <param name="container">The <see cref="IContainer"/> to add the current <see cref="WindowsFormsController"/> to.</param>
    public WindowsFormsController(IContainer container) : this()
    {
        container.Add(this);
    }

    /// <summary>
    /// Adds an extension of a controller.
    /// </summary>
    /// <param name="extension">The extension of a controller to be added.</param>
    public static void AddExtension(IWindowsFormsControllerExtension extension) => Extensions.Add(extension);

    /// <summary>
    /// Removes an extension of a controller.
    /// </summary>
    /// <param name="extension">The extension of a controller to be removed.</param>
    public static void RemoveExtension(IWindowsFormsControllerExtension extension) => Extensions.Remove(extension);

    /// <summary>
    /// Gets a container of an extension that specified controller has.
    /// </summary>
    /// <typeparam name="TExtension">The type of the extension.</typeparam>
    /// <typeparam name="T">The type of the container of the extension.</typeparam>
    /// <param name="controller">The controller that has the extension.</param>
    /// <returns>The container of the extension that the specified controller has.</returns>
    public static T Retrieve<TExtension, T>(object controller) where TExtension : IWindowsFormsControllerExtension where T : class, new()
        => Extensions.OfType<TExtension>().FirstOrDefault()?.Retrieve(controller) as T ?? new T();

    /// <summary>
    /// Gets the collection of the controllers.
    /// </summary>
    /// <returns>The collection of the controllers.</returns>
    public WindowsFormsControllerCollection GetControllers() => EnsureControllers();

    /// <summary>
    /// Sets the specified data context to the specified controller.
    /// </summary>
    /// <param name="dataContext">The data context that is set to the controller.</param>
    /// <param name="controller">The controller to which the data context is set.</param>
    public void SetDataContext(object? dataContext, object controller) => DataContextInjector.Inject(dataContext, controller);

    /// <summary>
    /// Sets the specified control to the specified controller.
    /// </summary>
    /// <param name="control">The control that is set to the controller.</param>
    /// <param name="controller">The controller to which the control is set.</param>
    /// <param name="foundControlOnly">
    /// If <c>true</c>, a control is not set to the controller when it is not found in the specified control;
    /// otherwise, <c>null</c> is set.
    /// </param>
    public void SetControl(Control? control, object controller, bool foundControlOnly = false) => ControlInjector.Inject(control, controller, foundControlOnly);

    /// <summary>
    /// Gets event handlers that the specified controller has.
    /// </summary>
    /// <param name="controller">The controller that has event handlers.</param>
    /// <returns>The event handlers that the specified controller has.</returns>
    public EventHandlerBase<Component, WindowsFormsEventHandlerItem> EventHandlersOf(object controller)
        => Retrieve<WindowsFormsEventHandlerExtension, EventHandlerBase<Component, WindowsFormsEventHandlerItem>>(controller);

    /// <summary>
    /// Attaches controllers to the specified view.
    /// </summary>
    /// <param name="view">The view to which controllers is attached.</param>
    protected virtual void AttachControllersTo(Control? view)
    {
        if (DesignMode) return;
        if (view is null) return;

        if (DataContextFinder.Find(view) is null)
        {
            view.AddDataContextChangedHandler(OnControlDataContextChanged, DataContextFinder);
            return;
        }

        var currentControllers = Controllers;
        Controllers = EnsureControllers(true);
        if (currentControllers is not null) Controllers.AddRange(currentControllers);
        Controllers.AddRange(ControllerTypeFinder.Find(view).Select(ControllerFactory.Create).OfType<object>());
        Controllers.AttachTo(view);
    }

    /// <summary>
    /// Detaches controllers from the specified view.
    /// </summary>
    /// <param name="view">The view from which controllers is detached.</param>
    protected virtual void DetachControllersFrom(Control? view)
    {
        if (DesignMode) return;
        if (view is null) return;

        view.RemoveDataContextChangedHandler(OnControlDataContextChanged, DataContextFinder);

        Controllers?.Detach();
        Controllers = null;
    }

    /// <summary>
    /// Ensures the injector to inject controls in a view to a controller.
    /// </summary>
    protected void EnsureControlInjector() => ControlInjector = new WindowsFormsControlInjector(ControlFinder);

    /// <summary>
    /// Ensures the finder to find a type of a controller.
    /// </summary>
    protected void EnsureControllerTypeFinder() => ControllerTypeFinder = new WindowsFormsControllerTypeFinder(ControlKeyFinder, DataContextFinder);

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.
    /// Sets <c>null</c> to the <see cref="View"/> property in order to detach controllers from the view.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources. </param>
    protected override void Dispose(bool disposing)
    {
        if (disposed) return;

        if (disposing) View = null;

        base.Dispose(disposing);

        disposed = true;
    }
    private bool disposed;

    private WindowsFormsControllerCollection EnsureControllers(bool force = false)
    {
        if (Controllers is null || force) Controllers = new WindowsFormsControllerCollection(DataContextFinder, DataContextInjector, ControlInjector, Extensions);
        return Controllers;
    }

    private void OnControlDataContextChanged(object? sender, DataContextChangedEventArgs e)
    {
        view?.RemoveDataContextChangedHandler(OnControlDataContextChanged, DataContextFinder);

        AttachControllersTo(view);

        Controllers.ForEach(controller => EventHandlersOf(controller).GetBy(null).From(view).With(e).Raise("DataContextChanged"));
    }

    internal static bool HandleUnhandledException(Exception exc)
    {
        var e = new UnhandledExceptionEventArgs(exc);
        OnUnhandledException(e);
        return e.Handled;
    }

    private static void OnUnhandledException(UnhandledExceptionEventArgs e) => UnhandledException?.Invoke(null, e);
}
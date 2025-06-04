# Release note

## v4.2.0

### Change

- Change Charites version to 3.2.0.

## v4.1.0

### Changes

- Change Charites version to 3.1.0.

## v4.0.0

### Changes

- Change the target framework version to .NET 8.0.
- Change Charites version to 3.0.0.
- Change Charites.Bindings version to 3.0.0.
- Change to bind after unbinding without throwing an exception when ItemsSource has already bound another items.
- Change to return without throwing an exception when ItemsSource has not bound any items yet.
- Change to bind after unbinding when BoundPropertyBinding has already bound another.
- Change to bind after unbinding when ObservablePropertyBinding has already bound another.
- Change to be able to user the DataContext property and the DataContextChanged event of the Control.

## v3.3.0

## Changes

- Change Charites version to 2.2.0.
- Change Charites.Bindings version to 2.2.0.

## v3.2.0

### Add

- Add the IWindowsFormsControlFinder interface that extends IElementFinder&lt;Component&gt; interface.
- Add the DefaultControlFinder and ControlFinder property to the WindowsFormsController class.

### Changes

 - Change Charites version to 2.1.0.
 - Change the WindowsFormsEventHandlerExtension so that event handlers that have parameters attributed by the following attribute can be injected.
   - FromDIAttribute
   - FromElementAttribute
   - FromDataContextAttribute

## v3.1.0

### Add

- Add the ListItemToIndexConverter class that converts an item in a list to its index.

### Changes

- Change Charites.Bindings version to 2.1.0.
- Change the type of the parameter of the Bind method in the ItemsSource class to IEnumerable from INotifyCollectionChanged.
- Change to be able to handle events of components and inject components to a controller.

## v3.0.0

### Changes

- Change the target framework version to .NET 6.0.
- Change Charites version to 2.0.0.
- Change Charites.Bindings version to 2.0.0.
- Enable Nullable reference types.
- Change to prefer to resolve parameters with the specified resolver when they are resolved by dependecies.

## v2.1.1

### Changes

- Change Charites version to 1.3.2.
- Modify how to retrieve an event name from a method that represents an event handler using naming convention. If its name ends with "Async", it is ignored.

## v2.1.0

### Changes

- Change the framework version to .NET Core 3.1 and .NET 5.0.
- Change Charites version to 1.3.1.
- Change Charites.Bindings version to 1.2.1.

## v2.0.0

### Changes

- Change the framework version to .NET Core 3.0.

## v1.3.0

### Add

- Add the function to inject dependencies of parameters based on the FromDIAttribute attribute.

### Changes

- Change Charites version to 1.3.0.

## v1.2.0

### Add

- Add the IBindableProperty interface and the BoundPropertyBinding class.
- Add the Bind method with the BoundProperty to the ObservablePropertyBindings class.
- Add the Priority property to the ContentViewAttribute class.

### Changes

- Change Charites version to 1.2.0.
- Change Charites.Bindings version to 1.2.0.

### Bug fix

- Fixed to remove event handlers from controllers and sets null to controls and a data context of them when the WindowsFormsController is disposed.

## v1.1.0

### Add

- Add the UnhandledException event to the WindowsFormsController.

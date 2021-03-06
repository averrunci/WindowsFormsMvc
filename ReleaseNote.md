# Release note

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

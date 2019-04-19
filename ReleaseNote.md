# Release note

## V1.2.0

### Add

- Add the IBindableProperty interface and the BoundPropertyBinding class.
- Add the Bind method with the BoundProperty to the ObservablePropertyBindings class.
- Add the Priority property to the ContentViewAttribute class.

### Changes

- Change Charites version to 1.2.0.
- Change Charites.Bindings version to 1.2.0.

### Bug fix

- Fixed to remove event handlers from controllers and sets null to controls and a data context of them when the WindowsFormsController is disposed.

## V1.1.0

### Add

- Add the UnhandledException event to the WindowsFormsController.

// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;
using Carna;

namespace Charites.Windows.Mvc;

[Context("Unhandled exception")]
class WindowsFormsControllerSpec_UnhandledException : FixtureSteppable, IDisposable
{
    WindowsFormsController WindowsFormsController { get; } = new();

    object Controller { get; set; } = default!;
    TestControls.TestControl Control { get; set; } = default!;

    Exception? UnhandledException { get; set; }
    bool HandledException { get; set; }

    public WindowsFormsControllerSpec_UnhandledException()
    {
        WindowsFormsController.UnhandledException += OnWindowsFormsControllerUnhandledException;
    }

    public void Dispose()
    {
        WindowsFormsController.UnhandledException -= OnWindowsFormsControllerUnhandledException;
    }

    private void OnWindowsFormsControllerUnhandledException(object? sender, UnhandledExceptionEventArgs e)
    {
        UnhandledException = e.Exception;
        e.Handled = HandledException;
    }

    [Example("Handles an unhandled exception as it is handled")]
    void Ex01()
    {
        HandledException = true;

        Given("a controller that has an event handler that throws an exception", () => Controller = new TestWindowsFormsControllers.ExceptionTestWindowsFormsController());
        Given("a control", () => Control = new TestControls.TestControl());

        When("the controller is added", () => WindowsFormsController.GetControllers().Add(Controller));
        When("the controller is attached to the control", () => WindowsFormsController.GetControllers().AttachTo(Control));

        When("the handle of the control is created", () => Control.RaiseHandleCreated());

        When("the Click event of the control is raised", () => Control.RaiseClick());
        Then("the unhandled exception should be handled", () => UnhandledException != null);
    }

    [Example("Handles an unhandled exception as it is not handled")]
    void Ex02()
    {
        HandledException = false;

        Given("a controller that has an event handler that throws an exception", () => Controller = new TestWindowsFormsControllers.ExceptionTestWindowsFormsController());
        Given("a control", () => Control = new TestControls.TestControl());

        When("the controller is added", () => WindowsFormsController.GetControllers().Add(Controller));
        When("the controller is attached to the control", () => WindowsFormsController.GetControllers().AttachTo(Control));

        When("the handle of the control is created", () => Control.RaiseHandleCreated());

        When("the Click event of the control is raised", () => Control.RaiseClick());
        Then<TargetInvocationException>("the exception should be thrown");
        Then("the unhandled exception should be handled", () => UnhandledException != null);
    }
}
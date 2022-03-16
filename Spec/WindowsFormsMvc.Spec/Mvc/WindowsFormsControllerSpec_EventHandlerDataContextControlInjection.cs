// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;
using Carna;

namespace Charites.Windows.Mvc;

[Context("Event handler, data context, and control injection")]
class WindowsFormsControllerSpec_EventHandlerDataContextControlInjection : FixtureSteppable
{
    WindowsFormsController WindowsFormsController { get; } = new();

    object DataContext { get; set; } = default!;
    TestControls.TestControl Control { get; set; } = default!;
    TestControls.TestControl ChildControl { get; set; } = default!;

    static bool EventHandled { get; set; }
    private static bool ComponentEventHandled { get; set; }
    static Action NoArgumentAssertionHandler { get; } = () => EventHandled = true;
    static Action NoArgumentComponentEventAssertionHandler { get; } = () => ComponentEventHandled = true;
    static Action<EventArgs> OneArgumentAssertionHandler { get; } = _ => EventHandled = true;
    static Action<EventArgs> OneArgumentComponentEventAssertionHandler { get; } = _ => ComponentEventHandled = true;
    static EventHandler AssertionEventHandler { get; } = (_, _) => EventHandled = true;
    static EventHandler ComponentEventAssertionEventHandler { get; } = (_, _) => ComponentEventHandled = true;

    [Example("Adds event handlers")]
    [Sample(Source = typeof(WindowsFormsControllerSampleDataSource))]
    void Ex01(TestWindowsFormsControllers.ITestWindowsFormsController controller)
    {
        Given("a data context", () => DataContext = new object());
        Given("a child control", () => ChildControl = new TestControls.TestControl { Name = "childControl" });
        Given("a control that has the child control", () =>
        {
            Control = new TestControls.TestControl { Name = "control", DataContext = DataContext };
            Control.Controls.Add(ChildControl);
        });

        When("the controller is added", () => WindowsFormsController.GetControllers().Add(controller));
        When("the controller is attached to the control", () => WindowsFormsController.GetControllers().AttachTo(Control));

        Then("the data context of the controller should be set", () => controller.DataContext == DataContext);
        Then("the control of the controller should be null", () => controller.Control == null);
        Then("the child control of the controller should be null", () => controller.ChildControl == null);
        Then("the component of the controller should be null", () => controller.TestComponent == null);

        When("the handle of the control is created", () => Control.RaiseHandleCreated());

        Then("the data context of the controller should be set", () => controller.DataContext == DataContext);
        Then("the control of the controller should be set", () => controller.Control == Control);
        Then("the child control of the controller should be set", () => controller.ChildControl == ChildControl);
        Then("the component of the controller should be set", () => controller.TestComponent == Control.GetTestComponent());

        EventHandled = false;
        ComponentEventHandled = false;
        When("the Click event of the child control is raised", () => ChildControl.RaiseClick());
        Then("the Click event should be handled", () => EventHandled && !ComponentEventHandled);

        EventHandled = false;
        ComponentEventHandled = false;
        When("the Component event of the control is raised", () => Control.RaiseTestComponentUpdated());
        Then("the Component event should be handled", () => !EventHandled && ComponentEventHandled);
    }

    [Example("Sets controls")]
    [Sample(Source = typeof(WindowsFormsControllerSampleDataSource))]
    void Ex02(TestWindowsFormsControllers.ITestWindowsFormsController controller)
    {
        Given("a child control", () => ChildControl = new TestControls.TestControl { Name = "childControl" });
        Given("a control", () => Control = new TestControls.TestControl { Name = "control" });
        When("the child control is set to the controller", () => WindowsFormsController.SetControl(ChildControl, controller, true));
        When("the control is set to the controller", () => WindowsFormsController.SetControl(Control, controller, true));
        Then("the control should be set to the controller", () => controller.Control == Control);
        Then("the child control should be set to the controller", () => controller.ChildControl == ChildControl);
        Then("the component should be set to the controller", () => controller.TestComponent == Control.GetTestComponent());
    }

    [Example("Sets a data context")]
    [Sample(Source = typeof(WindowsFormsControllerSampleDataSource))]
    void Ex03(TestWindowsFormsControllers.ITestWindowsFormsController controller)
    {
        Given("a data context", () => DataContext = new object());
        When("the data context is set to the controller", () => WindowsFormsController.SetDataContext(DataContext, controller));
        Then("the data context should be set to the controller", () => controller.DataContext == DataContext);
    }

    class WindowsFormsControllerSampleDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new
            {
                Description = "When the contents are attributed to fields and the event handler has no argument.",
                Controller = new TestWindowsFormsControllers.AttributedToField.NoArgumentHandlerController(NoArgumentAssertionHandler, NoArgumentComponentEventAssertionHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to fields and the event handler has one argument.",
                Controller = new TestWindowsFormsControllers.AttributedToField.OneArgumentHandlerController(OneArgumentAssertionHandler, OneArgumentComponentEventAssertionHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to fields and the event handler has two arguments.",
                Controller = new TestWindowsFormsControllers.AttributedToField.EventHandlerController(AssertionEventHandler, ComponentEventAssertionEventHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to properties and the event handler has no argument.",
                Controller = new TestWindowsFormsControllers.AttributedToProperty.NoArgumentHandlerController(NoArgumentAssertionHandler, NoArgumentComponentEventAssertionHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to properties and the event handler has one argument.",
                Controller = new TestWindowsFormsControllers.AttributedToProperty.OneArgumentHandlerController(OneArgumentAssertionHandler, OneArgumentComponentEventAssertionHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to properties and the event handler has two arguments.",
                Controller = new TestWindowsFormsControllers.AttributedToProperty.EventHandlerController(AssertionEventHandler, ComponentEventAssertionEventHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to methods and the event handler has no argument.",
                Controller = new TestWindowsFormsControllers.AttributedToMethod.NoArgumentHandlerController(NoArgumentAssertionHandler, NoArgumentComponentEventAssertionHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to methods and the event handler has one argument.",
                Controller = new TestWindowsFormsControllers.AttributedToMethod.OneArgumentHandlerController(OneArgumentAssertionHandler, OneArgumentComponentEventAssertionHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to methods and the event handler has two arguments.",
                Controller = new TestWindowsFormsControllers.AttributedToMethod.EventHandlerController(AssertionEventHandler, ComponentEventAssertionEventHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to methods using a naming convention and the event handler has no argument.",
                Controller = new TestWindowsFormsControllers.AttributedToMethodUsingNamingConvention.NoArgumentHandlerController(NoArgumentAssertionHandler, NoArgumentComponentEventAssertionHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to methods using a naming convention and the event handler has one argument.",
                Controller = new TestWindowsFormsControllers.AttributedToMethodUsingNamingConvention.OneArgumentHandlerController(OneArgumentAssertionHandler, OneArgumentComponentEventAssertionHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to methods using a naming convention and the event handler has two arguments.",
                Controller = new TestWindowsFormsControllers.AttributedToMethodUsingNamingConvention.EventHandlerController(AssertionEventHandler, ComponentEventAssertionEventHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to async methods using a naming convention and the event handler has no argument.",
                Controller = new TestWindowsFormsControllers.AttributedToAsyncMethodUsingNamingConvention.NoArgumentHandlerController(NoArgumentAssertionHandler, NoArgumentComponentEventAssertionHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to async methods using a naming convention and the event handler has one argument.",
                Controller = new TestWindowsFormsControllers.AttributedToAsyncMethodUsingNamingConvention.OneArgumentHandlerController(OneArgumentAssertionHandler, OneArgumentComponentEventAssertionHandler)
            };
            yield return new
            {
                Description = "When the contents are attributed to async methods using a naming convention and the event handler has two arguments.",
                Controller = new TestWindowsFormsControllers.AttributedToAsyncMethodUsingNamingConvention.EventHandlerController(AssertionEventHandler, ComponentEventAssertionEventHandler)
            };
        }
    }
}
// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;
using Carna;
using NSubstitute;

namespace Charites.Windows.Mvc;

[Context("Attaching and detaching a controller")]
class WindowsFormsControllerSpec_AttachingAndDetachingController : FixtureSteppable
{
    WindowsFormsController WindowsFormsController { get; } = new();

    object DataContext { get; set; } = default!;
    TestControls.TestControl View { get; set; } = default!;

    [Example("Attaches a controller when the view that has the DataContextSource is set")]
    [Sample(Source = typeof(AttachingControllersSampleDataSource))]
    void Ex01(object dataContext, Control view, Type[] expectedControllerTypes)
    {
        Given("a view that contains the data context", () => view.DataContext = dataContext);
        When("the view is set to the WindowsFormsController", () => WindowsFormsController.View = view);
        Then("the controller should be attached to the view", () =>
            WindowsFormsController.GetControllers().Select(controller => controller.GetType()).SequenceEqual(expectedControllerTypes) &&
            WindowsFormsController.GetControllers().Cast<TestWindowsFormsControllers.TestWindowsFormsControllerBase>().All(controller => controller.DataContext == dataContext)
        );
    }

    class AttachingControllersSampleDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            foreach (var data in GetData())
            {
                yield return CreateData(data, "and the view has the DataContextSource", new TestControls.AttachingTestControl());
            }
            foreach (var data in GetData())
            {
                yield return CreateData(data, "and the view does not have the DataContextSource", new TestControls.AttachingTestControlWithoutDataContextSource());
            }
        }

        private object CreateData(object data, string description, Control view)
        {
            var dataType = data.GetType();
            return new
            {
                Description = $"{dataType.GetProperty("Description")?.GetValue(data)} and {description}",
                DataContext = dataType.GetProperty("DataContext")?.GetValue(data),
                View = view,
                ExpectedControllerTypes = dataType.GetProperty("ExpectedControllerTypes")?.GetValue(data)
            };
        }

        private IEnumerable GetData()
        {
            yield return new
            {
                Description = "When the key is the name of the data context type",
                DataContext = new TestDataContexts.AttachingTestDataContext(),
                ExpectedControllerTypes = new[] { typeof(TestWindowsFormsControllers.TestDataContextController) }
            };
            yield return new
            {
                Description = "When the key is the name of the data context base type",
                DataContext = new TestDataContexts.DerivedBaseAttachingTestDataContext(),
                ExpectedControllerTypes = new[] { typeof(TestWindowsFormsControllers.BaseTestDataContextController) }
            };
            yield return new
            {
                Description = "When the key is the full name of the data context type",
                DataContext = new TestDataContexts.AttachingTestDataContextFullName(),
                ExpectedControllerTypes = new[] { typeof(TestWindowsFormsControllers.TestDataContextFullNameController) }
            };
            yield return new
            {
                Description = "When the key is the full name of the data context base type",
                DataContext = new TestDataContexts.DerivedBaseAttachingTestDataContextFullName(),
                ExpectedControllerTypes = new[] { typeof(TestWindowsFormsControllers.BaseTestDataContextFullNameController) }
            };
            yield return new
            {
                Description = "When the key is the name of the data context type that is generic",
                DataContext = new TestDataContexts.GenericAttachingTestDataContext<string>(),
                ExpectedControllerTypes = new[] { typeof(TestWindowsFormsControllers.GenericTestDataContextController) }
            };
            yield return new
            {
                Description = "When the key is the full name of the data context type that is generic",
                DataContext = new TestDataContexts.GenericAttachingTestDataContextFullName<string>(),
                ExpectedControllerTypes = new[] { typeof(TestWindowsFormsControllers.GenericTestDataContextFullNameController), typeof(TestWindowsFormsControllers.GenericTestDataContextFullNameWithoutParametersController) }
            };
            yield return new
            {
                Description = "When the key is the name of interface implemented by the data context",
                DataContext = new TestDataContexts.InterfaceImplementedAttachingTestDataContext(),
                ExpectedControllerTypes = new[] { typeof(TestWindowsFormsControllers.InterfaceImplementedTestDataContextController) }
            };
            yield return new
            {
                Description = "When the key is the full name of interface implemented by the data context",
                DataContext = new TestDataContexts.InterfaceImplementedAttachingTestDataContextFullName(),
                ExpectedControllerTypes = new[] { typeof(TestWindowsFormsControllers.InterfaceImplementedTestDataContextFullNameController) }
            };
        }
    }

    [Example("Attaches a controller when the view is set before the data context of the view is set")]
    [Sample(Source = typeof(AttachingControllersSampleDataSource))]
    void Ex02(object dataContext, Control view, Type[] expectedControllerTypes)
    {
        When("the view is set to the WindowsFormsController", () => WindowsFormsController.View = view);
        When("a data context of the view is set", () => view.DataContext = dataContext);
        Then("the controller should be attached to the view", () =>
            WindowsFormsController.GetControllers().Select(controller => controller.GetType()).SequenceEqual(expectedControllerTypes) &&
            WindowsFormsController.GetControllers().Cast<TestWindowsFormsControllers.TestWindowsFormsControllerBase>().All(controller => controller.DataContext == dataContext)
        );
    }

    [Example("Attaches a controller when the Type is specified")]
    [Sample(Source = typeof(AttachingControllersTypeSpecifiedSampleDataSource))]
    void Ex03(Control view, IEnumerable<Type> expectedControllerTypes)
    {
        Given("a data context", () => DataContext = new object());
        Given("a view that contains the data context", () => view.DataContext = DataContext);
        When("the view is set to the WindowsFormsController", () => WindowsFormsController.View = view);
        Then("the controller should be attached to the view", () =>
            WindowsFormsController.GetControllers().Select(controller => controller.GetType()).SequenceEqual(expectedControllerTypes) &&
            WindowsFormsController.GetControllers().Cast<TestWindowsFormsControllers.TestWindowsFormsControllerBase>().All(controller => controller.DataContext == DataContext)
        );
    }

    class AttachingControllersTypeSpecifiedSampleDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new
            {
                Description = "When the view has the DataContextSource",
                View = new TestControls.SingleControllerView(),
                ExpectedControllerTypes = new[] { typeof(TestWindowsFormsControllers.TestWindowsFormsControllerForSingleControllerView) }
            };
            yield return new
            {
                Description = "When the view does not have the DataContextSource",
                View = new TestControls.SingleControllerViewWithoutDataContextSource(),
                ExpectedControllerTypes = new[] { typeof(TestWindowsFormsControllers.TestWindowsFormsControllerForSingleControllerViewWithoutDataContextSource) }
            };
        }
    }

    [Example("Removes event handlers and sets null to controls and a data context when the Dispose event of the root control is raised")]
    void Ex04()
    {
        var loadEventHandler = Substitute.For<Action>();
        var clickEventHandler = Substitute.For<Action>();

        Given("a view that contains the data context", () => View = new TestControls.TestControl { Name = "Control", DataContext = new TestDataContexts.TestDisposableDataContext() });
        When("the view is set to the WindowsFormsController", () =>
        {
            WindowsFormsController.View = View;
            WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>().LoadAssertionHandler = loadEventHandler;
            WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>().ClickAssertionHandler = clickEventHandler;
        });
        Then("the dat context of the controller should be set", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>().DataContext == View.DataContext);

        When("the handle of the view is created", () => View.RaiseHandleCreated());
        Then("the control of the controller should be set", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>().Control == View);
        When("the Load event is raised", () => View.RaiseLoad());
        Then("the Load event should be handled", () => loadEventHandler.Received(1).Invoke());
        When("the Click event is raised", () => View.RaiseClick());
        Then("the Click event should be handled", () => clickEventHandler.Received(1).Invoke());

        loadEventHandler.ClearReceivedCalls();
        clickEventHandler.ClearReceivedCalls();
        When("the view is disposed", () => View.Dispose());
        Then("the data context of the controller should be null", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>().DataContext == null);
        Then("the control of the controller should be null", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>().Control == null);
        Then("the controller should be disposed", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>().IsDisposed);
        When("the Load event is raised", () => View.RaiseLoad());
        Then("the Load event should not be handled", () => loadEventHandler.DidNotReceive().Invoke());
        When("the Click event is raised", () => View.RaiseClick());
        Then("the Click event should not be handled", () => clickEventHandler.DidNotReceive().Invoke());
    }

    [Example("Removes event handlers and sets null to controls and a data context when the view is released from the WindowsFormsController")]
    void Ex05()
    {
        var loadEventHandler = Substitute.For<Action>();
        var clickEventHandler = Substitute.For<Action>();
        TestWindowsFormsControllers.TestDisposableWindowsFormsController controller = default!;

        Given("a view that contains the data context", () => View = new TestControls.TestControl { Name = "Control", DataContext = new TestDataContexts.TestDisposableDataContext() });
        When("the view is set to the WindowsFormsController", () =>
        {
            WindowsFormsController.View = View;
            controller = WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>();
            controller.LoadAssertionHandler = loadEventHandler;
            controller.ClickAssertionHandler = clickEventHandler;
        });
        Then("the dat context of the controller should be set", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>().DataContext == View.DataContext);

        When("the handle of the view is created", () => View.RaiseHandleCreated());
        Then("the control of the controller should be set", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>().Control == View);
        When("the Load event is raised", () => View.RaiseLoad());
        Then("the Load event should be handled", () => loadEventHandler.Received(1).Invoke());
        When("the Click event is raised", () => View.RaiseClick());
        Then("the Click event should be handled", () => clickEventHandler.Received(1).Invoke());

        loadEventHandler.ClearReceivedCalls();
        clickEventHandler.ClearReceivedCalls();
        When("the view is released from the WindowsFormsController", () => WindowsFormsController.View = null);
        Then("the controller should be detached", () => !WindowsFormsController.GetControllers().Any());
        Then("the data context of the controller should be null", () => controller.DataContext == null);
        Then("the control of the controller should be null", () => controller.Control == null);
        Then("the controller should be disposed", () => controller.IsDisposed);
        When("the Load event is raised", () => View.RaiseLoad());
        Then("the Load event should not be handled", () => loadEventHandler.DidNotReceive().Invoke());
        When("the Click event is raised", () => View.RaiseClick());
        Then("the Click event should not be handled", () => clickEventHandler.DidNotReceive().Invoke());
    }

    [Example("Attaches multi controllers")]
    [Sample(Source = typeof(AttachingMultiControllersSampleDataSource))]
    void Ex06(Control view)
    {
        var loadEventHandler = Substitute.For<Action>();
        var clickEventHandler = Substitute.For<Action>();

        Given("a view that contains the data context", () =>
        {
            view.Name = "Control";
            view.DataContext = new TestDataContexts.MultiTestDataContext();
        });
        When("the view is set to the WindowsFormsController", () =>
        {
            WindowsFormsController.View = view;
            WindowsFormsController.GetController<TestWindowsFormsControllers.MultiTestWindowsFormsControllerA>().LoadAssertionHandler = loadEventHandler;
            WindowsFormsController.GetController<TestWindowsFormsControllers.MultiTestWindowsFormsControllerA>().ClickAssertionHandler = clickEventHandler;

            WindowsFormsController.GetController<TestWindowsFormsControllers.MultiTestWindowsFormsControllerB>().LoadAssertionHandler = loadEventHandler;
            WindowsFormsController.GetController<TestWindowsFormsControllers.MultiTestWindowsFormsControllerB>().ClickAssertionHandler = clickEventHandler;

            WindowsFormsController.GetController<TestWindowsFormsControllers.MultiTestWindowsFormsControllerC>().LoadAssertionHandler = loadEventHandler;
            WindowsFormsController.GetController<TestWindowsFormsControllers.MultiTestWindowsFormsControllerC>().ClickAssertionHandler = clickEventHandler;
        });
        Then("the data context of the controller should be set", () => WindowsFormsController.GetControllers().Cast<TestWindowsFormsControllers.TestWindowsFormsControllerBase>().All(controller => controller.DataContext == view.DataContext));

        When("the handle of the view is created", () => (view as TestControls.ITestControl)?.RaiseHandleCreated());
        Then("the control of the controller should be set", () => WindowsFormsController.GetControllers().Cast<TestWindowsFormsControllers.TestWindowsFormsControllerBase>().All(controller => controller.Control == view));
        When("the Load event is raised", () => (view as TestControls.ITestControl)?.RaiseLoad());
        Then("the Load event should be handled", () => loadEventHandler.Received(3).Invoke());
        When("the Click event is raised", () => (view as TestControls.ITestControl)?.RaiseClick());
        Then("the Click event should be handled", () => clickEventHandler.Received(3).Invoke());

        loadEventHandler.ClearReceivedCalls();
        clickEventHandler.ClearReceivedCalls();
        When("the view is disposed", view.Dispose);
        Then("the data context of the controller should be null", () => WindowsFormsController.GetControllers().Cast<TestWindowsFormsControllers.TestWindowsFormsControllerBase>().All(controller => controller.DataContext == null));
        Then("the control of the controller should be null", () => WindowsFormsController.GetControllers().Cast<TestWindowsFormsControllers.TestWindowsFormsControllerBase>().All(controller => controller.Control == null));
        When("the Load event is raised", () => (view as TestControls.ITestControl)?.RaiseLoad());
        Then("the Load event should not be handled", () => loadEventHandler.DidNotReceive().Invoke());
        When("the Click event is raised", () => (view as TestControls.ITestControl)?.RaiseClick());
        Then("the Click event should not be handled", () => clickEventHandler.DidNotReceive().Invoke());
    }

    class AttachingMultiControllersSampleDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new
            {
                Description = "When the view has the DataContextSource",
                View = new TestControls.TestControl()
            };
            yield return new
            {
                Description = "When the view does not have the DataContextSource",
                View = new TestControls.TestControlWithoutDataContextSource()
            };
        }
    }

    [Example("Retrieves event handlers and executes them when a view is not attached")]
    void Ex07()
    {
        var loadEventHandler = Substitute.For<Action>();
        TestWindowsFormsControllers.TestWindowsFormsController controller = default!;

        Given("a controller", () => controller = new TestWindowsFormsControllers.TestWindowsFormsController { LoadAssertionHandler = loadEventHandler });
        When("the Load event is raised using the EventHandlerBase", () =>
            WindowsFormsController.EventHandlersOf(controller)
                .GetBy("Control")
                .Raise("Load")
        );
        Then("the Load event should be handled", () => loadEventHandler.Received(1).Invoke());
    }

    [Example("Retrieves event handlers and executes them asynchronously when a  view is not attached")]
    void Ex08()
    {
        var loadEventHandler = Substitute.For<Action>();
        TestWindowsFormsControllers.TestWindowsFormsControllerAsync controller = default!;

        Given("a controller", () => controller = new TestWindowsFormsControllers.TestWindowsFormsControllerAsync { LoadAssertionHandler = loadEventHandler });
        When("the Load event is raised asynchronously using the EventHandlerBase", async () =>
            await WindowsFormsController.EventHandlersOf(controller)
                .GetBy("Control")
                .RaiseAsync("Load")
        );
        Then("the Load event should be handled", () => loadEventHandler.Received(1).Invoke());
    }

    [Example("Removes event handlers and sets null to controls and a data context when the WindowsFormsController is disposed")]
    void Ex09()
    {
        var loadEventHandler = Substitute.For<Action>();
        var clickEventHandler = Substitute.For<Action>();
        TestWindowsFormsControllers.TestDisposableWindowsFormsController controller = default!;

        Given("a view that contains the data context", () => View = new TestControls.TestControl { Name = "Control", DataContext = new TestDataContexts.TestDisposableDataContext() });
        When("the view is set to the WindowsFormsController", () =>
        {
            WindowsFormsController.View = View;
            controller = WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>();
            controller.LoadAssertionHandler = loadEventHandler;
            controller.ClickAssertionHandler = clickEventHandler;
        });
        Then("the dat context of the controller should be set", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>().DataContext == View.DataContext);

        When("the handle of the view is created", () => View.RaiseHandleCreated());
        Then("the control of the controller should be set", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDisposableWindowsFormsController>().Control == View);
        When("the Load event is raised", () => View.RaiseLoad());
        Then("the Load event should be handled", () => loadEventHandler.Received(1).Invoke());
        When("the Click event is raised", () => View.RaiseClick());
        Then("the Click event should be handled", () => clickEventHandler.Received(1).Invoke());

        loadEventHandler.ClearReceivedCalls();
        clickEventHandler.ClearReceivedCalls();
        When("the WindowsFormsController is disposed", () => WindowsFormsController.Dispose());
        Then("the controller should be detached", () => !WindowsFormsController.GetControllers().Any());
        Then("the data context of the controller should be null", () => controller.DataContext == null);
        Then("the control of the controller should be null", () => controller.Control == null);
        Then("the controller should be disposed", () => controller.IsDisposed);
        When("the Load event is raised", () => View.RaiseLoad());
        Then("the Load event should not be handled", () => loadEventHandler.DidNotReceive().Invoke());
        When("the Click event is raised", () => View.RaiseClick());
        Then("the Click event should not be handled", () => clickEventHandler.DidNotReceive().Invoke());
    }
}
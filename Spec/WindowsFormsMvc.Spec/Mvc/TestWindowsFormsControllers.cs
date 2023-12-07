// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc;

internal static class TestWindowsFormsControllers
{
    public interface ITestWindowsFormsController
    {
        object? DataContext { get; }
        Control? Control { get; }
        Control? ChildControl { get; }
        TestControls.TestComponent? TestComponent { get; }
    }

    public class TestWindowsFormsControllerBase
    {
        [DataContext]
        public object? DataContext { get; set; }

        [Element]
        public Control? Control { get; set; }

        [EventHandler(ElementName = nameof(Control), Event = nameof(UserControl.Load))]
        protected void Control_Load() => LoadAssertionHandler?.Invoke();

        [EventHandler(ElementName = nameof(Control), Event = nameof(System.Windows.Forms.Control.Click))]
        protected void Control_Click() => ClickAssertionHandler?.Invoke();

        public Action? LoadAssertionHandler { get; set; }
        public Action? ClickAssertionHandler { get; set; }
    }

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+TestDataContext")]
    public class TestWindowsFormsController : TestWindowsFormsControllerBase;

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+MultiTestDataContext")]
    public class MultiTestWindowsFormsControllerA : TestWindowsFormsControllerBase;

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+MultiTestDataContext")]
    public class MultiTestWindowsFormsControllerB : TestWindowsFormsControllerBase;

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+MultiTestDataContext")]
    public class MultiTestWindowsFormsControllerC : TestWindowsFormsControllerBase;

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+TestDisposableDataContext")]
    public class TestDisposableWindowsFormsController : TestWindowsFormsControllerBase, IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }

    [View(ViewType = typeof(TestControls.SingleControllerView))]
    public class TestWindowsFormsControllerForSingleControllerView : TestWindowsFormsControllerBase;

    [View(ViewType = typeof(TestControls.MultiControllerView))]
    public class MultiTestWindowsFormsControllerAForMultiControllerView : TestWindowsFormsControllerBase;

    [View(ViewType = typeof(TestControls.MultiControllerView))]
    public class MultiTestWindowsFormsControllerBForMultiControllerView : TestWindowsFormsControllerBase;

    [View(ViewType = typeof(TestControls.MultiControllerView))]
    public class MultiTestWindowsFormsControllerCForMultiControllerView : TestWindowsFormsControllerBase;

    public class TestWindowsFormsControllerAsync
    {
        [DataContext]
        public object? DataContext { get; set; }

        [Element]
        public Control? Control { get; set; }

        [EventHandler(ElementName = nameof(Control), Event = nameof(UserControl.Load))]
        private async Task Control_Load()
        {
            var task = Task.Run(() => LoadAssertionHandler?.Invoke());
            task.Wait();
            await task;
        }

        [EventHandler(ElementName = nameof(Control), Event = nameof(System.Windows.Forms.Control.Click))]
        private async Task Control_Click() => await Task.Run(() => ClickAssertionHandler?.Invoke());

        public Action? LoadAssertionHandler { get; set; }
        public Action? ClickAssertionHandler { get; set; }
    }

    public class ExceptionTestWindowsFormsController
    {
        [EventHandler(Event = nameof(Control.Click))]
        private void Control_Click()
        {
            throw new Exception();
        }
    }

    [View(Key = "AttachingTestDataContext")]
    public class TestDataContextController : TestWindowsFormsControllerBase;

    [View(Key = "BaseAttachingTestDataContext")]
    public class BaseTestDataContextController : TestWindowsFormsControllerBase;

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+AttachingTestDataContextFullName")]
    public class TestDataContextFullNameController : TestWindowsFormsControllerBase;

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+BaseAttachingTestDataContextFullName")]
    public class BaseTestDataContextFullNameController : TestWindowsFormsControllerBase;

    [View(Key = "GenericAttachingTestDataContext`1")]
    public class GenericTestDataContextController : TestWindowsFormsControllerBase;

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+GenericAttachingTestDataContextFullName`1[System.String]")]
    public class GenericTestDataContextFullNameController : TestWindowsFormsControllerBase;

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+GenericAttachingTestDataContextFullName`1")]
    public class GenericTestDataContextFullNameWithoutParametersController : TestWindowsFormsControllerBase;

    [View(Key = "IAttachingTestDataContext")]
    public class InterfaceImplementedTestDataContextController : TestWindowsFormsControllerBase;

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+IAttachingTestDataContextFullName")]
    public class InterfaceImplementedTestDataContextFullNameController : TestWindowsFormsControllerBase;

    public class AttributedToField
    {
        public class Controller : ITestWindowsFormsController
        {
            [DataContext]
            protected object? dataContext;

            [Element]
            protected Control? control;

            [Element]
            protected Control? childControl;

            [Element]
            protected TestControls.TestComponent? testComponent;

            public object? DataContext => dataContext;
            public Control? Control => control;
            public Control? ChildControl => childControl;
            public TestControls.TestComponent? TestComponent => testComponent;
        }

        public class NoArgumentHandlerController(Action assertionHandler, Action componentEventAssertionHandler) : Controller
        {
            [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
            private Action handler = assertionHandler;

            [EventHandler(ElementName = "testComponent", Event = nameof(TestControls.TestComponent.Updated))]
            private Action componentEventHandler = componentEventAssertionHandler;
        }

        public class OneArgumentHandlerController(Action<EventArgs> assertionHandler, Action<EventArgs> componentEventAssertionHandler) : Controller
        {
            [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
            private Action<EventArgs> handler = assertionHandler;

            [EventHandler(ElementName = "testComponent", Event = nameof(TestControls.TestComponent.Updated))]
            private Action<EventArgs> componentEventHandler = componentEventAssertionHandler;
        }

        public class EventHandlerController(EventHandler assertionHandler, EventHandler componentEventAssertionHandler) : Controller
        {
            [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
            private EventHandler handler = assertionHandler;

            [EventHandler(ElementName = "testComponent", Event = nameof(TestControls.TestComponent.Updated))]
            private EventHandler componentEventHandler = componentEventAssertionHandler;
        }
    }

    public class AttributedToProperty
    {
        public class Controller : ITestWindowsFormsController
        {
            [DataContext]
            public object? DataContext { get; protected set; }

            [Element(Name = "control")]
            public Control? Control { get; protected set; }

            [Element(Name = "childControl")]
            public Control? ChildControl { get; protected set; }

            [Element(Name = "testComponent")]
            public TestControls.TestComponent? TestComponent { get; protected set; }
        }

        public class NoArgumentHandlerController(Action assertionHandler, Action componentEventAssertionHandler) : Controller
        {
            [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
            private Action Handler { get; set; } = assertionHandler;

            [EventHandler(ElementName = "testComponent", Event = nameof(TestControls.TestComponent.Updated))]
            private Action ComponentEventHandler { get; set; } = componentEventAssertionHandler;
        }

        public class OneArgumentHandlerController(Action<EventArgs> assertionHandler, Action<EventArgs> componentEventAssertionHandler) : Controller
        {
            [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
            private Action<EventArgs> Handler { get; set; } = assertionHandler;

            [EventHandler(ElementName = "testComponent", Event = nameof(TestControls.TestComponent.Updated))]
            private Action<EventArgs> ComponentEventHandler { get; set; } = componentEventAssertionHandler;
        }

        public class EventHandlerController(EventHandler assertionHandler, EventHandler componentEventAssertionHandler) : Controller
        {
            [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
            private EventHandler Handler { get; set; } = assertionHandler;

            [EventHandler(ElementName = "testComponent", Event = nameof(TestControls.TestComponent.Updated))]
            private EventHandler ComponentEventHandler { get; set; } = componentEventAssertionHandler;
        }
    }

    public class AttributedToMethod
    {
        public class Controller : ITestWindowsFormsController
        {
            [DataContext]
            protected void SetDataContext(object? dataContext) => DataContext = dataContext;
            public object? DataContext { get; private set; }

            [Element(Name = "control")]
            protected void SetControl(Control? control) => Control = control;
            public Control? Control { get; private set; }

            [Element(Name = "childControl")]
            protected void SetChildControl(Control? childControl) => ChildControl = childControl;
            public Control? ChildControl { get; private set; }

            [Element(Name = "testComponent")]
            protected void SetTestComponent(TestControls.TestComponent? testComponent) => TestComponent = testComponent;
            public TestControls.TestComponent? TestComponent { get; private set; }
        }

        public class NoArgumentHandlerController(Action assertionHandler, Action componentEventAssertionHandler) : Controller
        {
            [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
            public void ChildControl_Click() => assertionHandler();

            [EventHandler(ElementName = "testComponent", Event = nameof(TestControls.TestComponent.Updated))]
            public void TestComponent_Updated() => componentEventAssertionHandler();
        }

        public class OneArgumentHandlerController(Action<EventArgs> assertionHandler, Action<EventArgs> componentEventAssertionHandler) : Controller
        {
            [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
            public void ChildControl_Click(EventArgs e) => assertionHandler(e);

            [EventHandler(ElementName = "testComponent", Event = nameof(TestControls.TestComponent.Updated))]
            public void TestComponent_Updated(EventArgs e) => componentEventAssertionHandler(e);
        }

        public class EventHandlerController(EventHandler assertionHandler, EventHandler componentEventAssertionHandler) : Controller
        {
            [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
            public void ChildControl_Click(object? sender, EventArgs e) => assertionHandler(sender, e);

            [EventHandler(ElementName = "testComponent", Event = nameof(TestControls.TestComponent.Updated))]
            public void TestComponent_Updated(object? sender, EventArgs e) => componentEventAssertionHandler(sender, e);
        }
    }

    public class AttributedToMethodUsingNamingConvention
    {
        public class Controller : ITestWindowsFormsController
        {
            protected void SetDataContext(object? dataContext) => DataContext = dataContext;
            public object? DataContext { get; private set; }

            [Element(Name = "control")]
            protected void SetControl(Control? control) => Control = control;
            public Control? Control { get; private set; }

            [Element(Name = "childControl")]
            protected void SetChildControl(Control? childControl) => ChildControl = childControl;
            public Control? ChildControl { get; private set; }

            [Element(Name = "testComponent")]
            protected void SetTestComponent(TestControls.TestComponent? testComponent) => TestComponent = testComponent;
            public TestControls.TestComponent? TestComponent { get; private set; }
        }

        public class NoArgumentHandlerController(Action assertionHandler, Action componentEventAssertionHandler) : Controller
        {
            public void childControl_Click() => assertionHandler();

            public void testComponent_Updated() => componentEventAssertionHandler();
        }

        public class OneArgumentHandlerController(Action<EventArgs> assertionHandler, Action<EventArgs> componentEventAssertionHandler) : Controller
        {
            public void childControl_Click(EventArgs e) => assertionHandler(e);

            public void testComponent_Updated(EventArgs e) => componentEventAssertionHandler(e);
        }

        public class EventHandlerController(EventHandler assertionHandler, EventHandler componentEventAssertionHandler) : Controller
        {
            public void childControl_Click(object? sender, EventArgs e) => assertionHandler(sender, e);

            public void testComponent_Updated(object? sender, EventArgs e) => componentEventAssertionHandler(sender, e);
        }
    }

    public class AttributedToAsyncMethodUsingNamingConvention
    {
        public class Controller : ITestWindowsFormsController
        {
            protected void SetDataContext(object? dataContext) => DataContext = dataContext;
            public object? DataContext { get; private set; }

            [Element(Name = "control")]
            protected void SetControl(Control? control) => Control = control;
            public Control? Control { get; private set; }

            [Element(Name = "childControl")]
            protected void SetChildControl(Control? childControl) => ChildControl = childControl;
            public Control? ChildControl { get; private set; }

            [Element(Name = "testComponent")]
            protected void SetTestComponent(TestControls.TestComponent? testComponent) => TestComponent = testComponent;
            public TestControls.TestComponent? TestComponent { get; private set; }
        }

        public class NoArgumentHandlerController(Action assertionHandler, Action componentEventAssertionHandler) : Controller
        {
            public Task childControl_ClickAsync()
            {
                assertionHandler();
                return Task.CompletedTask;
            }

            public Task testComponent_UpdatedAsync()
            {
                componentEventAssertionHandler();
                return Task.CompletedTask;
            }
        }

        public class OneArgumentHandlerController(Action<EventArgs> assertionHandler, Action<EventArgs> componentEventAssertionHandler) : Controller
        {
            public Task childControl_ClickAsync(EventArgs e)
            {
                assertionHandler(e);
                return Task.CompletedTask;
            }

            public Task testComponent_UpdatedAsync(EventArgs e)
            {
                componentEventAssertionHandler(e);
                return Task.CompletedTask;
            }
        }

        public class EventHandlerController(EventHandler assertionHandler, EventHandler componentEventAssertionHandler) : Controller
        {
            public Task childControl_ClickAsync(object? sender, EventArgs e)
            {
                assertionHandler(sender, e);
                return Task.CompletedTask;
            }

            public Task testComponent_UpdatedAsync(object? sender, EventArgs e)
            {
                componentEventAssertionHandler(sender, e);
                return Task.CompletedTask;
            }
        }
    }
}
// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Charites.Windows.Mvc
{
    internal static class TestWindowsFormsControllers
    {
        public interface ITestWindowsFormsController
        {
            object DataContext { get; }
            Control Control { get; }
            Control ChildControl { get; }
        }

        public class TestWindowsFormsControllerBase
        {
            [DataContext]
            public object DataContext { get; set; }

            [Element]
            public Control Control { get; set; }

            [EventHandler(ElementName = nameof(Control), Event = nameof(UserControl.Load))]
            protected void Control_Load() => LoadAssertionHandler?.Invoke();

            [EventHandler(ElementName = nameof(Control), Event = nameof(System.Windows.Forms.Control.Click))]
            protected void Control_Click() => ClickAssertionHandler?.Invoke();

            public Action LoadAssertionHandler { get; set; }
            public Action ClickAssertionHandler { get; set; }
        }

        [View(Key = "Charites.Windows.Mvc.TestDataContexts+TestDataContext")]
        public class TestWindowsFormsController : TestWindowsFormsControllerBase { }

        [View(Key = "Charites.Windows.Mvc.TestDataContexts+MultiTestDataContext")]
        public class MultiTestWindowsFormsControllerA : TestWindowsFormsControllerBase { }

        [View(Key = "Charites.Windows.Mvc.TestDataContexts+MultiTestDataContext")]
        public class MultiTestWindowsFormsControllerB : TestWindowsFormsControllerBase { }

        [View(Key = "Charites.Windows.Mvc.TestDataContexts+MultiTestDataContext")]
        public class MultiTestWindowsFormsControllerC : TestWindowsFormsControllerBase { }

        [View(ViewType = typeof(TestControls.SingleControllerView))]
        public class TestWindowsFormsControllerForSingleControllerView : TestWindowsFormsControllerBase { }

        [View(ViewType = typeof(TestControls.MultiControllerView))]
        public class MultiTestWindowsFormsControllerAForMultiControllerView : TestWindowsFormsControllerBase { }

        [View(ViewType = typeof(TestControls.MultiControllerView))]
        public class MultiTestWindowsFormsControllerBForMultiControllerView : TestWindowsFormsControllerBase { }

        [View(ViewType = typeof(TestControls.MultiControllerView))]
        public class MultiTestWindowsFormsControllerCForMultiControllerView : TestWindowsFormsControllerBase { }

        public class TestWindowsFormsControllerAsync
        {
            [DataContext]
            public object DataContext { get; set; }

            [Element]
            public Control Control { get; set; }

            [EventHandler(ElementName = nameof(Control), Event = nameof(UserControl.Load))]
            private async Task Control_Load()
            {
                var task = Task.Run(() => LoadAssertionHandler?.Invoke());
                task.Wait();
                await task;
            }

            [EventHandler(ElementName = nameof(Control), Event = nameof(System.Windows.Forms.Control.Click))]
            private async Task Control_Click() => await Task.Run(() => ClickAssertionHandler?.Invoke());

            public Action LoadAssertionHandler { get; set; }
            public Action ClickAssertionHandler { get; set; }
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
        public class TestDataContextController : TestWindowsFormsControllerBase { }

        [View(Key = "BaseAttachingTestDataContext")]
        public class BaseTestDataContextController : TestWindowsFormsControllerBase { }

        [View(Key = "Charites.Windows.Mvc.TestDataContexts+AttachingTestDataContextFullName")]
        public class TestDataContextFullNameController : TestWindowsFormsControllerBase { }

        [View(Key = "Charites.Windows.Mvc.TestDataContexts+BaseAttachingTestDataContextFullName")]
        public class BaseTestDataContextFullNameController : TestWindowsFormsControllerBase { }

        [View(Key = "GenericAttachingTestDataContext`1")]
        public class GenericTestDataContextController : TestWindowsFormsControllerBase { }

        [View(Key = "Charites.Windows.Mvc.TestDataContexts+GenericAttachingTestDataContextFullName`1[System.String]")]
        public class GenericTestDataContextFullNameController : TestWindowsFormsControllerBase { }

        [View(Key = "Charites.Windows.Mvc.TestDataContexts+GenericAttachingTestDataContextFullName`1")]
        public class GenericTestDataContextFullNameWithoutParametersController : TestWindowsFormsControllerBase { }

        [View(Key = "IAttachingTestDataContext")]
        public class InterfaceImplementedTestDataContextController : TestWindowsFormsControllerBase { }

        [View(Key = "Charites.Windows.Mvc.TestDataContexts+IAttachingTestDataContextFullName")]
        public class InterfaceImplementedTestDataContextFullNameController : TestWindowsFormsControllerBase { }

        public class AttributedToField
        {
            public class Controller : ITestWindowsFormsController
            {
                [DataContext]
                protected object dataContext;

                [Element]
                protected Control control;

                [Element]
                protected Control childControl;

                public object DataContext => dataContext;
                public Control Control => control;
                public Control ChildControl => childControl;
            }

            public class NoArgumentHandlerController : Controller
            {
                [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
                private Action handler;

                public NoArgumentHandlerController(Action assertionHandler)
                {
                    handler = assertionHandler;
                }
            }

            public class OneArgumentHandlerController : Controller
            {
                [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
                Action<EventArgs> handler;

                public OneArgumentHandlerController(Action<EventArgs> assertionHandler)
                {
                    handler = assertionHandler;
                }
            }

            public class EventHandlerController : Controller
            {
                [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
                EventHandler handler;

                public EventHandlerController(EventHandler assertionHandler)
                {
                    handler = assertionHandler;
                }
            }
        }

        public class AttributedToProperty
        {
            public class Controller : ITestWindowsFormsController
            {
                [DataContext]
                public object DataContext { get; protected set; }

                [Element(Name = "control")]
                public Control Control { get; protected set; }

                [Element(Name = "childControl")]
                public Control ChildControl { get; protected set; }
            }

            public class NoArgumentHandlerController : Controller
            {
                [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
                private Action Handler { get; set; }

                public NoArgumentHandlerController(Action assertionHandler)
                {
                    Handler = assertionHandler;
                }
            }

            public class OneArgumentHandlerController : Controller
            {
                [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
                private Action<EventArgs> Handler { get; set; }

                public OneArgumentHandlerController(Action<EventArgs> assertionHandler)
                {
                    Handler = assertionHandler;
                }
            }

            public class EventHandlerController : Controller
            {
                [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
                private EventHandler Handler { get; set; }

                public EventHandlerController(EventHandler assertionHandler)
                {
                    Handler = assertionHandler;
                }
            }
        }

        public class AttributedToMethod
        {
            public class Controller : ITestWindowsFormsController
            {
                [DataContext]
                protected void SetDataContext(object dataContext) => DataContext = dataContext;
                public object DataContext { get; private set; }

                [Element(Name = "control")]
                protected void SetControl(Control control) => Control = control;
                public Control Control { get; private set; }

                [Element(Name = "childControl")]
                protected void SetChildControl(Control childControl) => ChildControl = childControl;
                public Control ChildControl { get; private set; }
            }

            public class NoArgumentHandlerController : Controller
            {
                [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
                public void ChildControl_Click() => handler();
                private readonly Action handler;

                public NoArgumentHandlerController(Action assertionHandler)
                {
                    handler = assertionHandler;
                }
            }

            public class OneArgumentHandlerController : Controller
            {
                [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
                public void ChildControl_Click(EventArgs e) => handler(e);
                private readonly Action<EventArgs> handler;

                public OneArgumentHandlerController(Action<EventArgs> assertionHandler)
                {
                    handler = assertionHandler;
                }
            }

            public class EventHandlerController : Controller
            {
                [EventHandler(ElementName = "childControl", Event = nameof(System.Windows.Forms.Control.Click))]
                public void ChildControl_Click(object sender, EventArgs e) => handler(sender, e);
                private readonly EventHandler handler;

                public EventHandlerController(EventHandler assertionHandler)
                {
                    handler = assertionHandler;
                }
            }
        }
    }
}

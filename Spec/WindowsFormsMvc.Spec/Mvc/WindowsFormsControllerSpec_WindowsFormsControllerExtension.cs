// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Windows.Forms;
using Carna;
using NSubstitute;

namespace Charites.Windows.Mvc
{
    [Context("WindowsFormsControllerExtension")]
    class WindowsFormsControllerSpec_WindowsFormsControllerExtension : FixtureSteppable, IDisposable
    {
        class TestExtension : IWindowsFormsControllerExtension
        {
            public static object TestExtensionContainer { get; } = new object();
            public void Attach(object controller, Control element) { }
            public void Detach(object controller, Control element) { }
            public object Retrieve(object controller) => TestExtensionContainer;
        }

        IWindowsFormsControllerExtension Extension { get; } = Substitute.For<IWindowsFormsControllerExtension>();

        WindowsFormsController WindowsFormsController { get; } = new WindowsFormsController();
        TestDataContexts.TestDataContext DataContext { get; } = new TestDataContexts.TestDataContext();
        TestControls.TestControl View { get; set; }
        TestWindowsFormsControllers.TestWindowsFormsController Controller { get; set; }

        public void Dispose()
        {
            if (Extension == null) return;

            WindowsFormsController.RemoveExtension(Extension);
        }

        [Example("Attaches an extension when the handle of the view is created and detaches it when the view is disposed")]
        void Ex01()
        {
            Given("a view that contains a data context", () => View = new TestControls.TestControl { DataContext = DataContext });
            When("the view is set to the WindowsFormsController", () =>
            {
                WindowsFormsController.View = View;
                Controller = WindowsFormsController.GetController<TestWindowsFormsControllers.TestWindowsFormsController>();
            });
            When("the extension is added", () => WindowsFormsController.AddExtension(Extension));
            When("the handle of the view is created", () => View.RaiseHandleCreated());
            Then("the extension should be attached", () => Extension.Received().Attach(Controller, View));

            When("the view is disposed", () => View.Dispose());
            Then("the extension should be detached", () => Extension.Received().Detach(Controller, View));
        }

        [Example("Retrieves a container of an extension")]
        void Ex02()
        {
            Given("a controller", () => Controller = new TestWindowsFormsControllers.TestWindowsFormsController());
            When("an extension is added", () => WindowsFormsController.AddExtension(new TestExtension()));
            Then("the container of the extension should be retrieved", () => WindowsFormsController.Retrieve<TestExtension, object>(Controller) == TestExtension.TestExtensionContainer);
        }
    }
}

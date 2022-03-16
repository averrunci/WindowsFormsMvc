// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using Carna;
using NSubstitute;

namespace Charites.Windows.Mvc;

[Context("WindowsFormsControllerExtension")]
class WindowsFormsControllerSpec_WindowsFormsControllerExtension : FixtureSteppable, IDisposable
{
    class TestExtension : IWindowsFormsControllerExtension
    {
        public static object TestExtensionContainer { get; } = new();
        public void Attach(object controller, Component element) { }
        public void Detach(object controller, Component element) { }
        public object Retrieve(object controller) => TestExtensionContainer;
    }

    IWindowsFormsControllerExtension Extension { get; } = Substitute.For<IWindowsFormsControllerExtension>();

    WindowsFormsController WindowsFormsController { get; } = new();
    TestDataContexts.TestDataContext DataContext { get; } = new();
    TestControls.TestControl View { get; set; } = default!;
    TestWindowsFormsControllers.TestWindowsFormsController Controller { get; set; } = default!;

    public void Dispose()
    {
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
        Then("the extension should be attached", () => Extension.Received(1).Attach(Controller, View));

        When("the view is disposed", () => View.Dispose());
        Then("the extension should be detached", () => Extension.Received(1).Detach(Controller, View));
    }

    [Example("Retrieves a container of an extension")]
    void Ex02()
    {
        Given("a controller", () => Controller = new TestWindowsFormsControllers.TestWindowsFormsController());
        When("an extension is added", () => WindowsFormsController.AddExtension(new TestExtension()));
        Then("the container of the extension should be retrieved", () => WindowsFormsController.Retrieve<TestExtension, object>(Controller) == TestExtension.TestExtensionContainer);
    }
}
// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;
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
    TestWindowsFormsControllers.TestWindowsFormsController Controller { get; set; } = default!;

    public void Dispose()
    {
        WindowsFormsController.RemoveExtension(Extension);
    }

    [Example("Attaches an extension when the handle of the view is created and detaches it when the view is disposed")]
    [Sample(Source = typeof(WindowsFormsControllerExtensionSampleDataSource))]
    void Ex01(Control view)
    {
        Given("a view that contains a data context", () => view.DataContext = DataContext);
        When("the view is set to the WindowsFormsController", () =>
        {
            WindowsFormsController.View = view;
            Controller = WindowsFormsController.GetController<TestWindowsFormsControllers.TestWindowsFormsController>();
        });
        When("the extension is added", () => WindowsFormsController.AddExtension(Extension));
        When("the handle of the view is created", () => (view as TestControls.ITestControl)?.RaiseHandleCreated());
        Then("the extension should be attached", () => Extension.Received(1).Attach(Controller, view));

        When("the view is disposed", view.Dispose);
        Then("the extension should be detached", () => Extension.Received(1).Detach(Controller, view));
    }

    class WindowsFormsControllerExtensionSampleDataSource : ISampleDataSource
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

    [Example("Retrieves a container of an extension")]
    void Ex02()
    {
        Given("a controller", () => Controller = new TestWindowsFormsControllers.TestWindowsFormsController());
        When("an extension is added", () => WindowsFormsController.AddExtension(new TestExtension()));
        Then("the container of the extension should be retrieved", () => WindowsFormsController.Retrieve<TestExtension, object>(Controller) == TestExtension.TestExtensionContainer);
    }
}
// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Mvc
{
    [Context("Data context changed")]
    class WindowsFormsControllerSpec_DataContextChanged : FixtureSteppable
    {
        WindowsFormsController WindowsFormsController { get; } = new WindowsFormsController();

        object DataContext { get; } = new TestDataContexts.AttachingTestDataContext();
        object AnotherDataContext { get; } = new object();

        TestControls.AttachingTestControl View { get; set; }

        [Example("Changes the data context")]
        void Ex01()
        {
            Given("a view that contains the data context", () => View = new TestControls.AttachingTestControl { DataContext = DataContext });
            When("the view is set to the WindowsFormsController", () => WindowsFormsController.View = View);
            Then("the data context of the controller should be set", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDataContextController>().DataContext == DataContext);
            When("another data context is set for the view", () => View.DataContext = AnotherDataContext);
            Then("the data context of the controller should be changed", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDataContextController>().DataContext == AnotherDataContext);
        }
    }
}

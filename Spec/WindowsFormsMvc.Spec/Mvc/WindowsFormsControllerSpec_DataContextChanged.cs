// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;
using Carna;

namespace Charites.Windows.Mvc;

[Context("Data context changed")]
class WindowsFormsControllerSpec_DataContextChanged : FixtureSteppable
{
    WindowsFormsController WindowsFormsController { get; } = new();

    object DataContext { get; } = new TestDataContexts.AttachingTestDataContext();
    object AnotherDataContext { get; } = new();

    [Example("Changes the data context when the view has the DataContextSource")]
    [Sample(Source = typeof(WindowsFormsControllerDataContextChangedSampleDataSource))]
    void Ex01(Control view)
    {
        Given("a view that contains the data context", () => view.DataContext = DataContext );
        When("the view is set to the WindowsFormsController", () => WindowsFormsController.View = view);
        Then("the data context of the controller should be set", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDataContextController>().DataContext == DataContext);
        When("another data context is set for the view", () => view.DataContext = AnotherDataContext);
        Then("the data context of the controller should be changed", () => WindowsFormsController.GetController<TestWindowsFormsControllers.TestDataContextController>().DataContext == AnotherDataContext);
    }

    class WindowsFormsControllerDataContextChangedSampleDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new
            {
                Description = "When the view has the DataContextSource",
                View = new TestControls.AttachingTestControl()
            };
            yield return new
            {
                Description = "When the view does not have the DataContextSource",
                View = new TestControls.AttachingTestControlWithoutDataContextSource()
            };
        }
    }
}
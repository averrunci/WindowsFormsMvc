// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;
using Charites.Windows.Mvc;

namespace Charites.Windows.Forms;

[Specification("ContentControl Spec")]
class ContentControlSpec : FixtureSteppable
{
    ContentControl ContentControl { get; } = new();

    TestDataContexts.TestContent Content { get; } = new();
    TestDataContexts.DerivedTestContent DerivedContent { get; } = new();
    TestDataContexts.EmptyContent EmptyContent { get; } = new();

    TestControls.TestControl ContentView { get; set; } = default!;

    [Example("When the view type is not specified")]
    void Ex01()
    {
        When("the content is set to the content control", () => ContentControl.Content = Content);
        Then("the content view should be set to the content control", () =>
            ContentControl.Controls.Count == 1 &&
            ContentControl.Controls[0].GetType() == typeof(TestControls.TestContentView) &&
            ContentControl.Controls[0].Dock == DockStyle.Fill
        );
        Then("the data context of the content view should be set", () =>
            ContentControl.Controls.OfType<TestControls.TestControl>().First().DataContext == Content
        );

        ContentView = (TestControls.TestControl)ContentControl.Controls[0];
        When("another content is set to the content control", () => ContentControl.Content = DerivedContent);
        Then("the new content view should be set to the content control", () =>
            ContentControl.Controls.Count == 1 &&
            ContentControl.Controls[0].GetType() == typeof(TestControls.TestContentViewSpecifiedByBaseType) &&
            ContentControl.Controls[0].Dock == DockStyle.Fill
        );
        Then("the data context of the new content view should be set", () =>
            ContentControl.Controls.OfType<TestControls.TestControl>().First().DataContext == DerivedContent
        );
        Then("the old content view should be disposed", () => ContentView.IsDisposed);

        ContentView = (TestControls.TestControl)ContentControl.Controls[0];
        When("the content that is null is set to the content control", () => ContentControl.Content = null);
        Then("the controls of the content control should be cleared", () => ContentControl.Controls.Count == 0);
        Then("the old content view should be disposed", () => ContentView.IsDisposed);

        When("the content with which any controls are not associated is set to the content control", () => ContentControl.Content = EmptyContent);
        Then("the label whose text is a string expression of the content should be set to the content control", () =>
            ContentControl.Controls.Count == 1 &&
            ContentControl.Controls[0].GetType() == typeof(Label) &&
            ContentControl.Controls.OfType<Label>().First().Text == EmptyContent.ToString() &&
            ContentControl.Controls[0].Dock == DockStyle.Fill
        );
    }

    [Example("When the view type is specified")]
    void Ex02()
    {
        When("the view type is set to the content control", () => ContentControl.ViewType = typeof(TestControls.TestControl));
        Then("the controls of the content control should not be set", () => ContentControl.Controls.Count == 0);

        When("the content is set to the content control", () => ContentControl.Content = Content);
        Then("the content view whose type is the specified view type should be set to the content control", () =>
            ContentControl.Controls.Count == 1 &&
            ContentControl.Controls[0].GetType() == typeof(TestControls.TestControl) &&
            ContentControl.Controls[0].Dock == DockStyle.Fill
        );
        Then("the data context of the content view should be set", () =>
            ContentControl.Controls.OfType<TestControls.TestControl>().First().DataContext == Content
        );
    }
}
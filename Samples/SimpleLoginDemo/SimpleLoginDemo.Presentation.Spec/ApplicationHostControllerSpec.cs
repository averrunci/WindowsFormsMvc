// Copyright (C) 2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Windows.Forms;
using Carna;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
{
    [Specification("ApplicationHostController Spec")]
    class ApplicationHostControllerSpec : FixtureSteppable
    {
        ApplicationHostController Controller { get; }
        WindowsFormsController WindowsFormsController { get; } = new();

        ApplicationHost Host { get; } = new();
        IContentNavigator Navigator { get; } = Substitute.For<IContentNavigator>();

        object NextContent { get; } = new();

        public ApplicationHostControllerSpec()
        {
            Controller = new ApplicationHostController(Navigator);

            WindowsFormsController.SetDataContext(Host, Controller);
        }

        [Example("Sets a new content when the current content is navigated")]
        void Ex01()
        {
            When("the content is navigated to the next content", () =>
                Navigator.Navigated += Raise.EventWith(Navigator, new ContentNavigatedEventArgs(ContentNavigationMode.New, NextContent, Host))
            );
            Then("the content to navigate should be set to the content of the ApplicationHost", () => Host.Content.Value == NextContent);
        }

        [Example("Loads the ApplicationHost view")]
        void Ex02()
        {
            When("the ApplicationHost view is loaded", () =>
                WindowsFormsController.EventHandlersOf(Controller)
                    .GetBy("ApplicationHostView")
                    .Raise(nameof(UserControl.Load))
            );
            Then("the content should be navigated to the LoginContent", () =>
            {
                Navigator.Received(1).NavigateTo(Arg.Any<LoginContent>());
            });
        }
    }
}

// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home;

[Specification("HomeController Spec")]
class HomeControllerSpec : FixtureSteppable
{
    HomeController Controller { get; } = new();
    WindowsFormsController WindowsFormsController { get; } = new();

    HomeContent HomeContent { get; } = new("User");
    IContentNavigator Navigator { get; } = Substitute.For<IContentNavigator>();

    public HomeControllerSpec()
    {
        WindowsFormsController.SetDataContext(HomeContent, Controller);
    }

    [Example("Logs out")]
    void Ex01()
    {
        When("the log out button is clicked", () =>
            WindowsFormsController.EventHandlersOf(Controller)
                .GetBy("logoutButton")
                .Resolve<IContentNavigator>(() => Navigator)
                .Raise(nameof(Control.Click))
        );
        Then("the content should be navigated to the LoginContent", () =>
        {
            Navigator.Received(1).NavigateTo(Arg.Any<LoginContent>());
        });
    }
}
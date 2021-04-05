// Copyright (C) 2018-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Windows.Forms;
using Carna;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home
{
    [Specification("HomeController Spec")]
    class HomeControllerSpec : FixtureSteppable
    {
        HomeController Controller { get; set; }
        WindowsFormsController WindowsFormsController { get; } = new WindowsFormsController();

        HomeContent HomeContent { get; } = new HomeContent("User");
        IContentNavigator Navigator { get; } = Substitute.For<IContentNavigator>();

        public HomeControllerSpec()
        {
            Controller = new HomeController(Navigator);

            WindowsFormsController.SetDataContext(HomeContent, Controller);
        }

        [Example("Logs out")]
        void Ex01()
        {
            When("the log out button is clicked", () =>
                WindowsFormsController.EventHandlersOf(Controller)
                    .GetBy("logoutButton")
                    .Raise(nameof(Control.Click))
            );
            Then("the content should be navigated to the LoginContent", () =>
            {
                Navigator.Received(1).NavigateTo(Arg.Any<LoginContent>());
            });
        }

        [Example("When the IContentNavigator is not specified")]
        void Ex02()
        {
            When("a controller to which the IContentNavigator is no specified is created", () => new HomeController(null));
            Then<ArgumentNullException>($"{typeof(ArgumentNullException)} should be thrown");
        }
    }
}

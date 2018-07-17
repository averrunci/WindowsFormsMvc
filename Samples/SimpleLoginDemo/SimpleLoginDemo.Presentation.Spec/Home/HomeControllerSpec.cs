// Copyright (C) 2018 Fievus
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
        HomeController Controller { get; } = new HomeController();
        WindowsFormsController WindowsFormsController { get; } = new WindowsFormsController();

        HomeContent HomeContent { get; } = new HomeContent("User");
        EventHandler<ContentRequestedEventArgs> ContentRequestedEventHandler { get; } = Substitute.For<EventHandler<ContentRequestedEventArgs>>();

        public HomeControllerSpec()
        {
            HomeContent.ContentRequested += ContentRequestedEventHandler;

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
            Then("the ContentRequested event should be raised", () =>
                ContentRequestedEventHandler.Received(1).Invoke(HomeContent, Arg.Is<ContentRequestedEventArgs>(e => e.Content is LoginContent))
            );
        }
    }
}

// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Carna;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home
{
    [Specification("HomeContent Spec")]
    class HomeContentSpec : FixtureSteppable
    {
        HomeContent HomeContent { get; } = new HomeContent("User");
        EventHandler<ContentRequestedEventArgs> ContentRequestedEventHandler { get; } = Substitute.For<EventHandler<ContentRequestedEventArgs>>();

        public HomeContentSpec()
        {
            HomeContent.ContentRequested += ContentRequestedEventHandler;
        }

        [Example("Logs out")]
        void Ex01()
        {
            When("to log out", () => HomeContent.Logout());
            Then("the ContentRequested event should be raised", () =>
                ContentRequestedEventHandler.Received(1).Invoke(HomeContent, Arg.Is<ContentRequestedEventArgs>(e => e.Content is LoginContent))
            );
        }
    }
}

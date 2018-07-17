// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Carna;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login
{
    [Specification("LoginContent Spec")]
    class LoginContentSpec : FixtureSteppable
    {
        LoginContent LoginContent { get; } = new LoginContent();

        EventHandler<ContentRequestedEventArgs> ContentRequestedEventHandler { get; } = Substitute.For<EventHandler<ContentRequestedEventArgs>>();

        public LoginContentSpec()
        {
            LoginContent.ContentRequested += ContentRequestedEventHandler;
        }

        [Example("Validates the user id and password")]
        [Sample(null, null, false, Description = "When the user id is null and the password is null")]
        [Sample("", "", false, Description = "When the user id is empty and the password is empty")]
        [Sample(null, "password", false, Description = "When the user id is null and the password is not null or empty")]
        [Sample("", "password", false, Description = "When the user id is empty and the password is not null or empty")]
        [Sample("user", null, false, Description = "When the user id is not null or empty and the password is null")]
        [Sample("user", "", false, Description = "When the user id is not null or empty and the password is empty")]
        [Sample("user", "password", true, Description = "when the user id is not null or empty and the password is not null or empty")]
        void Ex01(string userId, string password, bool expected)
        {
            When("the user id is set", () => LoginContent.UserId.Value = userId);
            When("the password is set", () => LoginContent.Password.Value = password);
            Then($"the validation result should be {expected}", () => LoginContent.IsValid == expected);
        }

        [Example("Indicates whether to be able to log in")]
        void Ex02()
        {
            Expect("you should not be able to log in", () => !LoginContent.CanLogin.Value);

            When("the user id is set", () => LoginContent.UserId.Value = "user");
            Then("you should not be able to log in", () => !LoginContent.CanLogin.Value);

            When("the password is set", () => LoginContent.Password.Value = "password");
            Then("you should be able to log in", () => LoginContent.CanLogin.Value);

            When("the user id is unset", () => LoginContent.UserId.Value = string.Empty);
            Then("you should not be able to log in", () => !LoginContent.CanLogin.Value);

            When("the user id is set again", () => LoginContent.UserId.Value = "user");
            Then("you should be able to log in", () => LoginContent.CanLogin.Value);

            When("the password is unset", () => LoginContent.Password.Value = string.Empty);
            Then("you should not be able to log in", () => !LoginContent.CanLogin.Value);

            When("the password is set again", () => LoginContent.Password.Value = "password");
            Then("you should be able to log in", () => LoginContent.CanLogin.Value);
        }

        [Example("Logs in when the content is valid")]
        void Ex03()
        {
            When("the valid user id is set", () => LoginContent.UserId.Value = "user");
            When("the valid password is set", () => LoginContent.Password.Value = "password");
            When("to log in", () => LoginContent.Login());
            Then("the ContentChanging event should be raised", () =>
                ContentRequestedEventHandler.Received(1).Invoke(
                    LoginContent,
                    Arg.Is<ContentRequestedEventArgs>(e => (e.Content as HomeContent).UserId == LoginContent.UserId.Value)
                )
            );
        }

        [Example("Logs in when the content is invalid")]
        void Ex04()
        {
            When("the invalid user id is set", () => LoginContent.UserId.Value = null);
            When("the invalid password is set", () => LoginContent.Password.Value = null);
            When("to log in", () => LoginContent.Login());
            Then("the ContentChanging event should not be raised", () =>
                ContentRequestedEventHandler.DidNotReceive().Invoke(Arg.Any<object>(), Arg.Any<ContentRequestedEventArgs>())
            );
        }
    }
}

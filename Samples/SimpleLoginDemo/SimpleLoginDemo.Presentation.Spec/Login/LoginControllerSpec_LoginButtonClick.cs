// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Carna;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Properties;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login
{
    [Context("Clicks the login button")]
    class LoginControllerSpec_LoginButtonClick : FixtureSteppable
    {
        LoginController Controller { get; set; }
        WindowsFormsController WindowsFormsController { get; } = new WindowsFormsController();

        LoginContent LoginContent { get; } = new LoginContent { Message = { Value = "message" } };
        IUserAuthentication UserAuthentication { get; } = Substitute.For<IUserAuthentication>();
        EventHandler<ContentRequestedEventArgs> ContentRequestedEventHandler { get; } = Substitute.For<EventHandler<ContentRequestedEventArgs>>();

        public LoginControllerSpec_LoginButtonClick()
        {
            Controller = new LoginController(UserAuthentication);
            LoginContent.ContentRequested += ContentRequestedEventHandler;

            WindowsFormsController.SetDataContext(LoginContent, Controller);
        }

        [Example("When the user is authenticated")]
        void Ex01()
        {
            When("the valid user id and password are set", () =>
            {
                LoginContent.UserId.Value = "user";
                LoginContent.Password.Value = "password";

                UserAuthentication.Authenticate(LoginContent.UserId.Value, LoginContent.Password.Value)
                    .Returns(Task.FromResult(UserAuthenticationResult.Succeeded()));
            });
            When("the login button is clicked", async () =>
                await WindowsFormsController.EventHandlersOf(Controller)
                    .GetBy("loginButton")
                    .RaiseAsync(nameof(Control.Click))
            );
            Then("the ContentRequested event should be raised", () =>
                ContentRequestedEventHandler.Received(1).Invoke(
                    LoginContent,
                    Arg.Is<ContentRequestedEventArgs>(e => (e.Content as HomeContent).UserId == LoginContent.UserId.Value)
                )
            );
        }

        [Example("When the IUserAuthentication is not specified")]
        void Ex02()
        {
            Given("a controller to which the IUserAuthentication is not specified", () =>
            {
                Controller = new LoginController(null);
                WindowsFormsController.SetDataContext(LoginContent, Controller);
            });
            When("the login button is clicked", async () =>
                await WindowsFormsController.EventHandlersOf(Controller)
                    .GetBy("loginButton")
                    .RaiseAsync(nameof(Control.Click))
            );
            Then("the ContentRequested event should not be raised", () =>
                ContentRequestedEventHandler.DidNotReceive().Invoke(Arg.Any<object>(), Arg.Any<ContentRequestedEventArgs>())
            );
            Then("the LoginNotAvailable message should be set", () => LoginContent.Message.Value == Resources.LoginNotAvailable);
        }

        [Example("When the login content is not valid")]
        void Ex03()
        {
            When("the invalid user id and password are set", () =>
            {
                LoginContent.UserId.Value = null;
                LoginContent.Password.Value = null;
            });
            When("the login button is clicked", async () =>
                await WindowsFormsController.EventHandlersOf(Controller)
                    .GetBy("loginButton")
                    .RaiseAsync(nameof(Control.Click))
            );
            Then("the ContentRequested event should not be raised", () =>
                ContentRequestedEventHandler.DidNotReceive().Invoke(Arg.Any<object>(), Arg.Any<ContentRequestedEventArgs>())
            );
        }

        [Example("When the user is not authenticated")]
        void Ex04()
        {
            When("the no authenticated user id and password are set", () =>
            {
                LoginContent.UserId.Value = "user";
                LoginContent.Password.Value = "password";

                UserAuthentication.Authenticate(LoginContent.UserId.Value, LoginContent.Password.Value)
                    .Returns(UserAuthenticationResult.Failed());
            });
            When("the login button is clicked", async () =>
                await WindowsFormsController.EventHandlersOf(Controller)
                    .GetBy("loginButton")
                    .RaiseAsync(nameof(Control.Click))
            );
            Then("the ContentRequested event should not be raised", () =>
                ContentRequestedEventHandler.DidNotReceive().Invoke(Arg.Any<object>(), Arg.Any<ContentRequestedEventArgs>())
            );
            Then("the LoginFailureMessage message should be set", () => LoginContent.Message.Value == Resources.LoginFailureMessage);
        }
    }
}

// Copyright (C) 2018-2021 Fievus
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
        IContentNavigator Navigator { get; } = Substitute.For<IContentNavigator>();
        IUserAuthentication UserAuthentication { get; } = Substitute.For<IUserAuthentication>();

        public LoginControllerSpec_LoginButtonClick()
        {
            Controller = new LoginController(Navigator, UserAuthentication);

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
            Then("the content should be navigated to the HomeContent", () =>
            {
                Navigator.Received(1).NavigateTo(Arg.Is<HomeContent>(content => content.UserId == LoginContent.UserId.Value));
            });
        }

        [Example("When the IContentNavigator is not specified")]
        void Ex02()
        {
            When("a controller to which the IContentNavigator is not specified is created", () =>
            {
                Controller = new LoginController(null, UserAuthentication);
            });
            Then<ArgumentNullException>($"{typeof(ArgumentNullException)} should be thrown");
        }

        [Example("When the IUserAuthentication is not specified")]
        void Ex03()
        {
            Given("a controller to which the IUserAuthentication is not specified", () =>
            {
                Controller = new LoginController(Navigator, null);
                WindowsFormsController.SetDataContext(LoginContent, Controller);
            });
            When("the login button is clicked", async () =>
                await WindowsFormsController.EventHandlersOf(Controller)
                    .GetBy("loginButton")
                    .RaiseAsync(nameof(Control.Click))
            );
            Then("the content should not be navigated to any contents.", () =>
            {
                Navigator.DidNotReceive().NavigateTo(Arg.Any<object>());
            });
            Then("the LoginNotAvailable message should be set", () => LoginContent.Message.Value == Resources.LoginNotAvailable);
        }

        [Example("When the login content is not valid")]
        void Ex04()
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
            Then("the content should not be navigated to any contents.", () =>
            {
                Navigator.DidNotReceive().NavigateTo(Arg.Any<object>());
            });
        }

        [Example("When the user is not authenticated")]
        void Ex05()
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
            Then("the content should not be navigated to any contents.", () =>
            {
                Navigator.DidNotReceive().NavigateTo(Arg.Any<object>());
            });
            Then("the LoginFailureMessage message should be set", () => LoginContent.Message.Value == Resources.LoginFailureMessage);
        }
    }
}

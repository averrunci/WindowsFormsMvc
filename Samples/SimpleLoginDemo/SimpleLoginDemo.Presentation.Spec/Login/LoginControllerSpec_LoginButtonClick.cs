// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Properties;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;

[Context("Clicks the login button")]
class LoginControllerSpec_LoginButtonClick : FixtureSteppable
{
    LoginController Controller { get; } = new();
    WindowsFormsController WindowsFormsController { get; } = new();

    LoginContent LoginContent { get; } = new() { Message = { Value = "message" } };
    IContentNavigator Navigator { get; } = Substitute.For<IContentNavigator>();
    IUserAuthentication UserAuthentication { get; } = Substitute.For<IUserAuthentication>();

    public LoginControllerSpec_LoginButtonClick()
    {
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
                .Resolve<IContentNavigator>(() => Navigator)
                .Resolve<IUserAuthentication>(() => UserAuthentication)
                .RaiseAsync(nameof(Control.Click))
        );
        Then("the content should be navigated to the HomeContent", () =>
        {
            Navigator.Received(1).NavigateTo(Arg.Is<HomeContent>(content => content.UserId == LoginContent.UserId.Value));
        });
    }

    [Example("When the login content is not valid")]
    void Ex02()
    {
        When("the invalid user id and password are set", () =>
        {
            LoginContent.UserId.Value = string.Empty;
            LoginContent.Password.Value = string.Empty;
        });
        When("the login button is clicked", async () =>
            await WindowsFormsController.EventHandlersOf(Controller)
                .GetBy("loginButton")
                .Resolve<IContentNavigator>(() => Navigator)
                .Resolve<IUserAuthentication>(() => UserAuthentication)
                .RaiseAsync(nameof(Control.Click))
        );
        Then("the authentication should not be executed", () =>
            UserAuthentication.DidNotReceive().Authenticate(Arg.Any<string>(), Arg.Any<string>()));
        Then("the content should not be navigated to any contents.", () =>
        {
            Navigator.DidNotReceive().NavigateTo(Arg.Any<object>());
        });
    }

    [Example("When the user is not authenticated")]
    void Ex03()
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
                .Resolve<IContentNavigator>(() => Navigator)
                .Resolve<IUserAuthentication>(() => UserAuthentication)
                .RaiseAsync(nameof(Control.Click))
        );
        Then("the content should not be navigated to any contents.", () =>
        {
            Navigator.DidNotReceive().NavigateTo(Arg.Any<object>());
        });
        Then("the LoginFailureMessage message should be set", () => LoginContent.Message.Value == Resources.LoginFailureMessage);
    }
}
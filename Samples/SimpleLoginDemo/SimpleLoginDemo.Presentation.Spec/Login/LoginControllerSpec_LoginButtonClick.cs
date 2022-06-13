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
    ILoginCommand LoginCommand { get; } = Substitute.For<ILoginCommand>();

    [Example("When the user is authenticated")]
    void Ex01()
    {
        When("the valid user id and password are set", () =>
        {
            LoginContent.UserId.Value = "user";
            LoginContent.Password.Value = "password";

            LoginCommand.Authenticate(LoginContent)
                .Returns(Task.FromResult(LoginAuthenticationResult.Succeeded()));
        });
        When("the login button is clicked", async () =>
            await WindowsFormsController.EventHandlersOf(Controller)
                .GetBy("loginButton")
                .ResolveFromDataContext(LoginContent)
                .ResolveFromDI<IContentNavigator>(() => Navigator)
                .ResolveFromDI<ILoginCommand>(() => LoginCommand)
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
                .ResolveFromDataContext(LoginContent)
                .ResolveFromDI<IContentNavigator>(() => Navigator)
                .ResolveFromDI<ILoginCommand>(() => LoginCommand)
                .RaiseAsync(nameof(Control.Click))
        );
        Then("the authentication should not be executed", () =>
            LoginCommand.DidNotReceive().Authenticate(Arg.Any<LoginContent>()));
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

            LoginCommand.Authenticate(LoginContent)
                .Returns(LoginAuthenticationResult.Failed());
        });
        When("the login button is clicked", async () =>
            await WindowsFormsController.EventHandlersOf(Controller)
                .GetBy("loginButton")
                .ResolveFromDataContext(LoginContent)
                .ResolveFromDI<IContentNavigator>(() => Navigator)
                .ResolveFromDI<ILoginCommand>(() => LoginCommand)
                .RaiseAsync(nameof(Control.Click))
        );
        Then("the content should not be navigated to any contents.", () =>
        {
            Navigator.DidNotReceive().NavigateTo(Arg.Any<object>());
        });
        Then("the LoginFailureMessage message should be set", () => LoginContent.Message.Value == Resources.LoginFailureMessage);
    }
}
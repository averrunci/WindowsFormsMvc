// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Properties;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;

[View(Key = nameof(LoginContent))]
public class LoginController
{
    [EventHandler(Event = nameof(UserControl.Load))]
    private void OnLoad([FromElement(Name = "LoginView")] LoginView loginView, [FromElement] Panel loginPanel)
    {
        OnSizeChanged(loginView, loginPanel);
    }

    [EventHandler(Event = nameof(Control.SizeChanged))]
    private void OnSizeChanged([FromElement(Name = "LoginView")] LoginView loginView, [FromElement] Panel loginPanel)
    {
        loginPanel.Location = new Point((loginView.Width - loginPanel.Width) / 2, (loginView.Height - loginPanel.Height) / 2);
    }

    [EventHandler(ElementName = "loginButton", Event = nameof(Control.Click))]
    private async Task OnLoginButtonClickAsync([FromDataContext] LoginContent loginContent, [FromDI] IContentNavigator navigator, [FromDI] ILoginCommand command)
    {
        loginContent.Message.Value = string.Empty;

        if (!loginContent.IsValid) return;

        var result = await command.AuthenticateAsync(loginContent);
        if (result.Success)
        {
            navigator.NavigateTo(new HomeContent(loginContent.UserId.Value));
        }
        else
        {
            loginContent.Message.Value = Resources.LoginFailureMessage;
        }
    }
}
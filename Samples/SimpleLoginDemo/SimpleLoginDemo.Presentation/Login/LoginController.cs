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
    [DataContext]
    private readonly LoginContent? loginContent = null;

    [Element(Name = "LoginView")]
    private readonly LoginView? loginView = null;

    [Element]
    private readonly Panel? loginPanel = null;

    [EventHandler(Event = nameof(UserControl.Load))]
    void OnLoad()
    {
        OnSizeChanged();
    }

    [EventHandler(Event = nameof(Control.SizeChanged))]
    void OnSizeChanged()
    {
        if (loginView is null || loginPanel is null) return;

        loginPanel.Location = new Point((loginView.Width - loginPanel.Width) / 2, (loginView.Height - loginPanel.Height) / 2);
    }

    [EventHandler(ElementName = "loginButton", Event = nameof(Control.Click))]
    private async Task OnLoginButtonClickAsync([FromDI] IContentNavigator navigator, [FromDI] IUserAuthentication userAuthentication)
    {
        if (loginContent is null) return;

        loginContent.Message.Value = string.Empty;

        if (!loginContent.IsValid) return;

        var currentLoginContent = loginContent;
        var result = await userAuthentication.Authenticate(currentLoginContent.UserId.Value, currentLoginContent.Password.Value);
        if (result.Success)
        {
            navigator.NavigateTo(new HomeContent(loginContent.UserId.Value));
        }
        else
        {
            currentLoginContent.Message.Value = Resources.LoginFailureMessage;
        }
    }
}
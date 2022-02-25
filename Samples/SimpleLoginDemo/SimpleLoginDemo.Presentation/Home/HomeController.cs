// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home;

[View(Key = nameof(HomeContent))]
public class HomeController
{
    [EventHandler(ElementName = "logoutButton", Event = nameof(Control.Click))]
    private void OnLogoutButtonClick([FromDI] IContentNavigator navigator)
    {
        navigator.NavigateTo(new LoginContent());
    }
}
// Copyright (C) 2018-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Windows.Forms;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home
{
    [View(Key = nameof(HomeContent))]
    public class HomeController
    {
        private readonly IContentNavigator navigator;

        public HomeController(IContentNavigator navigator)
        {
            this.navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
        }

        [EventHandler(ElementName = "logoutButton", Event = nameof(Control.Click))]
        private void OnLogoutButtonClick()
        {
            navigator.NavigateTo(new LoginContent());
        }
    }
}

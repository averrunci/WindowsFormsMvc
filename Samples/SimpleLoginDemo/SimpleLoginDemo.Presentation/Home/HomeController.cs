// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Windows.Forms;
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home
{
    [View(Key = nameof(HomeContent))]
    public class HomeController
    {
        [DataContext]
        private readonly HomeContent homeContent = null;

        [EventHandler(ElementName = "logoutButton", Event = nameof(Control.Click))]
        private void OnLogoutButtonClick()
        {
            homeContent.Logout();
        }
    }
}

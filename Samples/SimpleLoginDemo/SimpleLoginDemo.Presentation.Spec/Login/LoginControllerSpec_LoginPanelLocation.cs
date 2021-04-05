// Copyright (C) 2018-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Windows.Forms;
using Carna;
using Charites.Windows.Mvc;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login
{
    [Context("Login panel location")]
    class LoginControllerSpec_LoginPanelLocation : FixtureSteppable
    {
        LoginController Controller { get; } = new LoginController(Substitute.For<IContentNavigator>(), null);
        WindowsFormsController WindowsFormsController { get; } = new WindowsFormsController();

        LoginView LoginView { get; } = new LoginView { Name = "LoginView", Width = 800, Height = 600 };
        Panel LoginPanel { get; } = new Panel { Name = "loginPanel", Width = 200, Height = 150 };

        public LoginControllerSpec_LoginPanelLocation()
        {
            WindowsFormsController.SetControl(LoginView, Controller, true);
            WindowsFormsController.SetControl(LoginPanel, Controller, true);
        }

        void AssertLoginPanelLocation()
        {
            Then("the login panel is located on the center of the login view", () =>
                LoginPanel.Location.X == (LoginView.Width - LoginPanel.Width) / 2 &&
                LoginPanel.Location.Y == (LoginView.Height - LoginPanel.Height) / 2
            );
        }

        [Example("When the login view is loaded")]
        void Ex01()
        {
            When("the Load event is raised", () =>
                WindowsFormsController.EventHandlersOf(Controller)
                    .GetBy(null)
                    .Raise(nameof(UserControl.Load))
            );
            AssertLoginPanelLocation();
        }

        [Example("When the size of the login view is changed")]
        void Ex02()
        {
            When("the SizeChanged event is raised", () =>
                WindowsFormsController.EventHandlersOf(Controller)
                    .GetBy(null)
                    .Raise(nameof(Control.SizeChanged))
            );
            AssertLoginPanelLocation();
        }
    }
}

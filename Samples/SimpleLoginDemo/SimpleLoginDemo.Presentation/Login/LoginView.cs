// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Windows.Forms;
using Charites.Windows.Forms;
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login
{
    [ContentView(typeof(LoginContent))]
    public partial class LoginView : UserControl
    {
        private readonly ObservablePropertyBindings observablePropertyBindings = new ObservablePropertyBindings();

        public LoginView()
        {
            InitializeComponent();
        }

        private void BindContent(LoginContent loginContent)
        {
            if (loginContent == null) return;

            observablePropertyBindings.BindTwoWay(loginContent.UserId, userIdTextBox, nameof(userIdTextBox.Text));
            observablePropertyBindings.BindTwoWay(loginContent.Password, passwordTextBox, nameof(passwordTextBox.Text));
            observablePropertyBindings.Bind(loginContent.CanLogin, loginButton, nameof(loginButton.Enabled));
            observablePropertyBindings.Bind(loginContent.Message, messageLabel, nameof(messageLabel.Text));
        }

        private void UnbindContent(LoginContent loginContent)
        {
            if (loginContent == null) return;

            observablePropertyBindings.Unbind();
        }

        private void dataContextSource_DataContextChanged(object sender, DataContextChangedEventArgs e)
        {
            UnbindContent(e.OldValue as LoginContent);
            BindContent(e.NewValue as LoginContent);
        }
    }
}

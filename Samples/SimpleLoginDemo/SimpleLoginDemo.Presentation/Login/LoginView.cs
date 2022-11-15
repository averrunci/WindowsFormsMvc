// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Forms;
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;

[ContentView(typeof(LoginContent))]
public partial class LoginView : UserControl
{
    private readonly ObservablePropertyBindings observablePropertyBindings = new();

    public LoginView()
    {
        InitializeComponent();
    }

    private void BindContent(LoginContent loginContent)
    {
        observablePropertyBindings.BindTwoWay(loginContent.UserId, userIdTextBox, nameof(userIdTextBox.Text));
        observablePropertyBindings.BindTwoWay(loginContent.Password, passwordTextBox, nameof(passwordTextBox.Text));
        observablePropertyBindings.Bind(loginContent.CanLogin, loginButton, nameof(loginButton.Enabled));
        observablePropertyBindings.Bind(loginContent.Message, messageLabel, nameof(messageLabel.Text));
    }

    private void UnbindContent(LoginContent loginContent)
    {
        observablePropertyBindings.Unbind();
    }

    private void dataContextSource_DataContextChanged(object? sender, DataContextChangedEventArgs e)
    {
        (e.OldValue as LoginContent).IfPresent(UnbindContent);
        (e.NewValue as LoginContent).IfPresent(BindContent);
    }
}
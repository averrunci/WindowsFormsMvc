// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Forms;
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation;

[ContentView(typeof(ApplicationHost))]
public partial class ApplicationHostView : UserControl
{
    private readonly ObservablePropertyBindings observablePropertyBindings = new();

    public ApplicationHostView()
    {
        InitializeComponent();
    }

    private void BindContent(ApplicationHost host)
    {
        observablePropertyBindings.Bind(host.Content, contentControl, nameof(contentControl.Content));
    }

    private void UnbindContent()
    {
        observablePropertyBindings.Unbind();
    }

    private void ApplicationHostView_DataContextChanged(object? sender, EventArgs e)
    {
        UnbindContent();
        (DataContext as ApplicationHost).IfPresent(BindContent);
    }
}
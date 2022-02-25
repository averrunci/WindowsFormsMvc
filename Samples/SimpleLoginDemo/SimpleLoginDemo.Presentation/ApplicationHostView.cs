// Copyright (C) 2022 Fievus
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

    private void UnbindContent(ApplicationHost host)
    {
        observablePropertyBindings.Unbind();
    }

    private void dataContextSource_DataContextChanged(object? sender, DataContextChangedEventArgs e)
    {
        if (e.OldValue is ApplicationHost oldContent) UnbindContent(oldContent);
        if (e.NewValue is ApplicationHost newContent) BindContent(newContent);
    }
}
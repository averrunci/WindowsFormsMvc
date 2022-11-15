// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home;

[ContentView(typeof(HomeContent))]
public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }

    private void BindContent(HomeContent homeContent)
    {
        messageLabel.Text = homeContent.Message;
    }

    private void UnbindContent(HomeContent homeContent)
    {
        messageLabel.Text = string.Empty;
    }

    private void dataContextSource_DataContextChanged(object? sender, DataContextChangedEventArgs e)
    {
        (e.OldValue as HomeContent).IfPresent(UnbindContent);
        (e.NewValue as HomeContent).IfPresent(BindContent);
    }
}
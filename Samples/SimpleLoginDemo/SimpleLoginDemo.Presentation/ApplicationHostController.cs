// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation;

[View(Key = nameof(ApplicationHost))]
public class ApplicationHostController : IDisposable
{
    private readonly IContentNavigator navigator;

    [DataContext]
    private readonly ApplicationHost? host = null;

    public ApplicationHostController(IContentNavigator navigator)
    {
        this.navigator = navigator;
        SubscribeContentNavigatorEvent();
    }

    public void Dispose()
    {
        UnsubscribeContentNavigatorEvent();
    }

    private void SubscribeContentNavigatorEvent()
    {
        navigator.Navigated += OnContentNavigated;
    }

    private void UnsubscribeContentNavigatorEvent()
    {
        navigator.Navigated -= OnContentNavigated;
    }

    private void OnContentNavigated(object? sender, ContentNavigatedEventArgs e)
    {
        if (host is null) return;

        host.Content.Value = e.Content;
    }

    private void ApplicationHostView_Load()
    {
        navigator.NavigateTo(new LoginContent());
    }
}
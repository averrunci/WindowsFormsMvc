// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation;

[View(Key = nameof(ApplicationHost))]
public class ApplicationHostController : ControllerBase<ApplicationHost>, IDisposable
{
    private readonly IContentNavigator navigator;

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

    private void Navigate(ApplicationHost host, object content)
    {
        host.Content.Value = content;
    }

    private void OnContentNavigated(object? sender, ContentNavigatedEventArgs e)
    {
        DataContext.IfPresent(e.Content, Navigate);
    }

    private void ApplicationHostView_Load()
    {
        navigator.NavigateTo(new LoginContent());
    }
}
// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Properties;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home
{
    public class HomeContent : ILoginDemoContent
    {
        public event EventHandler<ContentRequestedEventArgs> ContentRequested;

        public string UserId { get; }

        public string Message => string.Format(Resources.UserMessageFormat, UserId);

        public HomeContent(string userId) => UserId = userId;

        public void Logout()
        {
            OnContentRequested(new ContentRequestedEventArgs(new LoginContent()));
        }

        protected virtual void OnContentRequested(ContentRequestedEventArgs e) => ContentRequested?.Invoke(this, e);
    }
}

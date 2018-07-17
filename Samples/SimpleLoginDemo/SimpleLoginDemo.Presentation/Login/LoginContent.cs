// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Charites.Windows.Mvc.Bindings;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login
{
    public class LoginContent : ILoginDemoContent
    {
        public event EventHandler<ContentRequestedEventArgs> ContentRequested;

        [DisplayName]
        [Required]
        public ObservableProperty<string> UserId { get; } = string.Empty.ToObservableProperty();

        [Required]
        public ObservableProperty<string> Password { get; } = string.Empty.ToObservableProperty();

        public ObservableProperty<string> Message { get; } = string.Empty.ToObservableProperty();

        public ObservableProperty<bool> CanLogin { get; } = new ObservableProperty<bool>();

        public bool IsValid
        {
            get
            {
                UserId.EnsureValidation();
                Password.EnsureValidation();

                var errorMessages = new List<string>();
                if (UserId.HasErrors) errorMessages.Add(UserId.Error);
                if (Password.HasErrors) errorMessages.Add(Password.Error);
                Message.Value = string.Join(Environment.NewLine, errorMessages);

                return !errorMessages.Any();
            }
        }

        public LoginContent()
        {
            UserId.EnableValidation(() => UserId);
            Password.EnableValidation(() => Password);

            UserId.EnsureValidation();
            Password.EnsureValidation();

            CanLogin.Bind(context => !UserId.HasErrors && !Password.HasErrors, UserId, Password);
        }

        public void Login()
        {
            if (!IsValid) return;

            OnContentRequested(new ContentRequestedEventArgs(new HomeContent(UserId.Value)));
        }

        protected virtual void OnContentRequested(ContentRequestedEventArgs e) => ContentRequested?.Invoke(this, e);
    }
}

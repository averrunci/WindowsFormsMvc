// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;

public class LoginContent
{
    [DisplayName]
    [Required]
    public ObservableProperty<string> UserId { get; } = string.Empty.ToObservableProperty();

    [Required]
    public ObservableProperty<string> Password { get; } = string.Empty.ToObservableProperty();

    public ObservableProperty<string> Message { get; } = string.Empty.ToObservableProperty();

    public BoundProperty<bool> CanLogin { get; }

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

        CanLogin = BoundProperty<bool>.By(_ => !UserId.HasErrors && !Password.HasErrors, UserId, Password);
    }
}
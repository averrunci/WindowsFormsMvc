﻿// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Properties;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home;

public class HomeContent(string userId)
{
    public string UserId { get; } = userId;

    public string Message => string.Format(Resources.UserMessageFormat, UserId);
}
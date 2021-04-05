// Copyright (C) 2018-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Properties;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home
{
    public class HomeContent
    {
        public string UserId { get; }

        public string Message => string.Format(Resources.UserMessageFormat, UserId);

        public HomeContent(string userId) => UserId = userId;
    }
}

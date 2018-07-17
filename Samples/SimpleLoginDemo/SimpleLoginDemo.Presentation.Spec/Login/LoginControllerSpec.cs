// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login
{
    [Specification("LoginController Spec")]
    class LoginControllerSpec
    {
        [Context]
        LoginControllerSpec_LoginPanelLocation LoginPanelLocation { get; }

        [Context]
        LoginControllerSpec_LoginButtonClick LoginButtonClick { get; }
    }
}

// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;

[Specification("LoginController Spec")]
class LoginControllerSpec
{
    [Context]
    LoginControllerSpec_LoginPanelLocation LoginPanelLocation => default!;

    [Context]
    LoginControllerSpec_LoginButtonClick LoginButtonClick => default!;
}
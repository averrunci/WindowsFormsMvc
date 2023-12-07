// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;

[Specification(
    "LoginController Spec",
    typeof(LoginControllerSpec_LoginPanelLocation),
    typeof(LoginControllerSpec_LoginButtonClick)
)]
class LoginControllerSpec;
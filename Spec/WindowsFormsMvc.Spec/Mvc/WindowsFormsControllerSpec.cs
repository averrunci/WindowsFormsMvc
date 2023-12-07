// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Mvc;

[Specification(
    "WindowsFormsController Spec",
    typeof(WindowsFormsControllerSpec_EventHandlerDataContextControlInjection),
    typeof(WindowsFormsControllerSpec_AttachingAndDetachingController),
    typeof(WindowsFormsControllerSpec_DataContextChanged),
    typeof(WindowsFormsControllerSpec_WindowsFormsControllerExtension),
    typeof(WindowsFormsControllerSpec_UnhandledException)
)]
class WindowsFormsControllerSpec;
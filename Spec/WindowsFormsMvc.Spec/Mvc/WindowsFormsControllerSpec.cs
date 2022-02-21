// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Mvc
{
    [Specification("WindowsFormsController Spec")]
    class WindowsFormsControllerSpec
    {
        [Context]
        WindowsFormsControllerSpec_EventHandlerDataContextControlInjection EventHandlerDataContextControlInjection => default!;

        [Context]
        WindowsFormsControllerSpec_AttachingAndDetachingController AttachingAndDetachingController => default!;

        [Context]
        WindowsFormsControllerSpec_DataContextChanged DataContextChanged => default!;

        [Context]
        WindowsFormsControllerSpec_WindowsFormsControllerExtension WindowsFormsControllerExtension => default!;

        [Context]
        WindowsFormsControllerSpec_UnhandledException UnhandledException => default!;
    }
}

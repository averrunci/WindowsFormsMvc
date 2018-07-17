// Copyright (C) 2018 Fievus
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
        WindowsFormsControllerSpec_EventHandlerDataContextControlInjection EventHandlerDataContextControlInjection { get; }

        [Context]
        WindowsFormsControllerSpec_AttachingAndDetachingController AttachingAndDetachingController { get; }

        [Context]
        WindowsFormsControllerSpec_DataContextChanged DataContextChanged { get; }

        [Context]
        WindowsFormsControllerSpec_WindowsFormsControllerExtension WindowsFormsControllerExtension { get; }
    }
}

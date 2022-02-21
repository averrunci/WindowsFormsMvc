// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc;

internal static class WindowsFormsControllerExtensions
{
    public static T GetController<T>(this WindowsFormsController @this) => @this.GetControllers().OfType<T>().First();
}
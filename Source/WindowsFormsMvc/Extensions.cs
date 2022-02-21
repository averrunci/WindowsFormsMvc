// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;

namespace Charites.Windows;

internal static class Extensions
{
    public static void ForEach<T>(this IEnumerable<T>? @this, Action<T> action)
    {
        if (@this is null) return;

        foreach (var item in @this)
        {
            action(item);
        }
    }

    public static Control? FindControl(this Control? @this, string controlName)
    {
        if (@this is null) return null;
        if (string.IsNullOrEmpty(controlName)) return @this;
        if (@this.Name == controlName) return @this;

        return @this.Controls.OfType<Control>()
            .Select(child => FindControl(child, controlName))
            .FirstOrDefault(control => control is not null);
    }

    public static void AddDataContextChangedHandler(this Control @this, DataContextChangedEventHandler handler, IWindowsFormsDataContextFinder dataContextFinder)
    {
        var dataContextSource = dataContextFinder.FindSource(@this);
        if (dataContextSource is null) return;

        dataContextSource.DataContextChanged += handler;
    }

    public static void RemoveDataContextChangedHandler(this Control @this, DataContextChangedEventHandler handler, IWindowsFormsDataContextFinder dataContextFinder)
    {
        var dataContextSource = dataContextFinder.FindSource(@this);
        if (dataContextSource is null) return;

        dataContextSource.DataContextChanged -= handler;
    }

    public static void SetDataContext(this Control @this, object? dataContext, IWindowsFormsDataContextFinder dataContextFinder)
    {
        var dataContextSource = dataContextFinder.FindSource(@this);
        if (dataContextSource is null) return;

        dataContextSource.Value = dataContext;
    }
}
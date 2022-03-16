// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using System.Reflection;
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

    public static Component? FindComponent(this Component? @this, string name)
    {
        if (@this is null) return null;
        if (string.IsNullOrEmpty(name)) return @this;
        
        return (@this as Control).FindControl(name) ??
            @this.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(field => typeof(Component).IsAssignableFrom(field.FieldType))
                .Where(field => field.Name == name)
                .Select(field => field.GetValue(@this) as Component)
                .FirstOrDefault(component => component is not null);
    }

    private static Control? FindControl(this Control? @this, string controlName)
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
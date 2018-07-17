// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Charites.Windows.Mvc;

namespace Charites.Windows
{
    internal static class Extensions
    {
        public static T RequireNonNull<T>(this T @this) => RequireNonNull(@this, null);

        public static T RequireNonNull<T>(this T @this, string name)
        {
            if (@this == null) throw new ArgumentNullException(name);

            return @this;
        }

        public static void IfPresent<T>(this T @this, Action<T> action)
        {
            if (@this == null || action == null) return;

            action(@this);
        }

        public static TResult IfPresent<TSource, TResult>(this TSource @this, Func<TSource, TResult> selector)
            => @this == null || selector == null ? default(TResult) : selector(@this);

        public static void IfAbsent<T>(this T @this, Action action)
        {
            if (@this != null) return;

            action();
        }

        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            if (@this == null || action == null) return;

            foreach (var item in @this)
            {
                action(item);
            }
        }

        public static Control FindControl(this Control @this, string controlName)
        {
            if (@this == null) return null;
            if (string.IsNullOrEmpty(controlName)) return @this;
            if (@this.Name == controlName) return @this;

            foreach (Control child in @this.Controls)
            {
                var control = FindControl(child, controlName);
                if (control != null) return control;
            }

            return null;
        }

        public static void AddDataContextChangedHandler(this Control @this, DataContextChangedEventHandler handler, IWindowsFormsDataContextFinder dataContextFinder)
            => dataContextFinder?.FindSource(@this).IfPresent(dataContextSource => dataContextSource.DataContextChanged += handler);

        public static void RemoveDataContextChangedHandler(this Control @this, DataContextChangedEventHandler handler, IWindowsFormsDataContextFinder dataContextFinder)
            => dataContextFinder?.FindSource(@this).IfPresent(dataContextSource => dataContextSource.DataContextChanged -= handler);

        public static void SetDataContext(this Control @this, object dataContext, IWindowsFormsDataContextFinder dataContextFinder)
            => dataContextFinder?.FindSource(@this).IfPresent(dataContextSource => dataContextSource.Value = dataContext);
    }
}

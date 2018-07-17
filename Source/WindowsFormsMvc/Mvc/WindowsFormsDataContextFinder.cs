﻿// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Charites.Windows.Mvc
{
    internal sealed class WindowsFormsDataContextFinder : IWindowsFormsDataContextFinder
    {
        public object Find(Control view) => FindSource(view)?.Value;

        public DataContextSource FindSource(Control view)
            => view?.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(field => typeof(DataContextSource).IsAssignableFrom(field.FieldType))
                .Select(field => field.GetValue(view) as DataContextSource)
                .FirstOrDefault();
    }
}

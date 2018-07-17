// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

using System.Drawing;
using System.Windows.Forms;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms
{
    internal class TestItem
    {
        public string Value
        {
            get => ValueProperty.Value;
            set => ValueProperty.Value = value;
        }

        public bool Checked
        {
            get => CheckedProperty.Value;
            set => CheckedProperty.Value = value;
        }

        public bool? NullableChecked
        {
            get => NullableCheckedProperty.Value;
            set => NullableCheckedProperty.Value = value;
        }

        public CheckState CheckState
        {
            get => CheckStateProperty.Value;
            set => CheckStateProperty.Value = value;
        }

        public Color BackColor
        {
            get => BackColorProperty.Value;
            set => BackColorProperty.Value = value;
        }

        public Color ForeColor
        {
            get => ForeColorProperty.Value;
            set => ForeColorProperty.Value = value;
        }

        public Font Font
        {
            get => FontProperty.Value;
            set => FontProperty.Value = value;
        }

        public ObservableProperty<string> ValueProperty { get; } = new ObservableProperty<string>();
        public ObservableProperty<bool> CheckedProperty { get; } = new ObservableProperty<bool>();
        public ObservableProperty<bool?> NullableCheckedProperty { get; } = new ObservableProperty<bool?>();
        public ObservableProperty<CheckState> CheckStateProperty { get; } = new ObservableProperty<CheckState>();

        public ObservableProperty<Color> BackColorProperty { get; } = new ObservableProperty<Color>();
        public ObservableProperty<Color> ForeColorProperty { get; } = new ObservableProperty<Color>();
        public ObservableProperty<Font> FontProperty { get; } = new ObservableProperty<Font>();

        public TestItem()
        {
        }

        public TestItem(string value)
        {
            Value = value;
        }

        public TestItem(string value, bool @checked)
        {
            Value = value;
            Checked = @checked;
        }

        public TestItem(string value, bool? @checked)
        {
            Value = value;
            NullableChecked = @checked;
        }

        public TestItem(string value, CheckState checkState)
        {
            Value = value;
            CheckState = checkState;
        }
    }
}

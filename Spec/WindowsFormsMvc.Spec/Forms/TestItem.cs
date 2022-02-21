// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Forms;

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

    public ObservableProperty<string> ValueProperty { get; } = new(string.Empty);
    public ObservableProperty<bool> CheckedProperty { get; } = new(false);
    public ObservableProperty<bool?> NullableCheckedProperty { get; } = new(null);
    public ObservableProperty<CheckState> CheckStateProperty { get; } = new(CheckState.Unchecked);

    public ObservableProperty<Color> BackColorProperty { get; } = new(Color.Transparent);
    public ObservableProperty<Color> ForeColorProperty { get; } = new(Color.Transparent);
    public ObservableProperty<Font> FontProperty { get; } = new(SystemFonts.DefaultFont);

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
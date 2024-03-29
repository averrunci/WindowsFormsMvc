﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Context("The value is specified")]
class ComboBoxItemsSourceSpec_ValueSpecified : ItemsSourceSpecContext<ComboBox, TestItem>
{
    ComboBox ComboBox { get; } = new();

    protected override ItemsSource<ComboBox, TestItem> CreateItemsSource() => new ComboBoxItemsSource<TestItem>(ComboBox, item => item.ValueProperty);
    protected override bool AssertControlItems(IList<TestItem> items) => ComboBox.Items.OfType<string>().SequenceEqual(items.Select(item => item.Value));
}
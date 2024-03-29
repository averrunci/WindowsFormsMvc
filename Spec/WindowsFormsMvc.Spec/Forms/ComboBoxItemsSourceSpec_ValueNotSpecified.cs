﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Context("The value is not specified")]
class ComboBoxItemsSourceSpec_ValueNotSpecified : ItemsSourceSpecContext<ComboBox, object>
{
    ComboBox ComboBox { get; } = new();

    protected override ItemsSource<ComboBox, object> CreateItemsSource() => new ComboBoxItemsSource(ComboBox);
    protected override bool AssertControlItems(IList<TestItem> items) => ComboBox.Items.OfType<TestItem>().SequenceEqual(items);
}
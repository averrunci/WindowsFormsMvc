// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Context("The value is specified")]
class ListBoxItemsSourceSpec_ValueSpecified : ItemsSourceSpecContext<ListBox, TestItem>
{
    ListBox ListBox { get; } = new();

    protected override ItemsSource<ListBox, TestItem> CreateItemsSource() => new ListBoxItemsSource<TestItem>(ListBox, item => item.ValueProperty);
    protected override bool AssertControlItems(IList<TestItem> items) => ListBox.Items.OfType<string>().SequenceEqual(items.Select(item => item.Value));
}
// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Context("The value is not specified")]
class ListBoxItemsSourceSpec_ValueNotSpecified : ItemsSourceSpecContext<ListBox, object>
{
    ListBox ListBox { get; } = new();

    protected override ItemsSource<ListBox, object> CreateItemsSource() => new ListBoxItemsSource(ListBox);
    protected override bool AssertControlItems(IList<TestItem> items) => ListBox.Items.OfType<TestItem>().SequenceEqual(items);
}
// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Carna;

namespace Charites.Windows.Forms
{
    [Context("The value is not specified")]
    class ListBoxItemsSourceSpec_ValueNotSpecified : ItemsSourceSpecContext<ListBox, object>
    {
        ListBox ListBox { get; } = new ListBox();

        protected override ItemsSource<ListBox, object> CreateItemsSource() => new ListBoxItemsSource(ListBox);
        protected override bool AssertControlItems(IList<TestItem> items) => ListBox.Items.OfType<TestItem>().SequenceEqual(items);
    }
}

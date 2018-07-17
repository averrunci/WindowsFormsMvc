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
    class CheckedListBoxItemsSourceSpec_ValueNotSpecified : ItemsSourceSpecContext<CheckedListBox, object>
    {
        CheckedListBox CheckedListBox { get; } = new CheckedListBox();

        protected override ItemsSource<CheckedListBox, object> CreateItemsSource() =>  new CheckedListBoxItemsSource(CheckedListBox);
        protected override bool AssertControlItems(IList<TestItem> items)
            => CheckedListBox.Items.OfType<TestItem>().SequenceEqual(items) &&
                Enumerable.Range(0, CheckedListBox.Items.Count).All(index => !CheckedListBox.GetItemChecked(index));
    }
}

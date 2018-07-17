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
    [Context("The value member is specified")]
    class CheckedListBoxItemsSourceSpec_ValueMemberSpecified : ItemsSourceSpecContext<CheckedListBox, object>
    {
        CheckedListBox CheckedListBox { get; } = new CheckedListBox();

        protected override ItemsSource<CheckedListBox, object> CreateItemsSource() => new CheckedListBoxItemsSource(CheckedListBox)
        {
            ValueMember = nameof(TestItem.Value)
        };
        protected override bool AssertControlItems(IList<TestItem> items)
            => CheckedListBox.Items.OfType<string>().SequenceEqual(items.Select(item => item.Value)) &&
                Enumerable.Range(0, CheckedListBox.Items.Count).All(index => !CheckedListBox.GetItemChecked(index));
    }
}

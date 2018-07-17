// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using Carna;

namespace Charites.Windows.Forms
{
    [Context("The boolean checked member is specified")]
    class CheckedListBoxItemsSourceSpec_BooleanCheckedMemberSpecified : ItemsSourceSpecContext<CheckedListBox, object>
    {
        CheckedListBox CheckedListBox { get; } = new CheckedListBox();

        protected override ItemsSource<CheckedListBox, object> CreateItemsSource() => new CheckedListBoxItemsSource(CheckedListBox)
        {
            ValueMember = nameof(TestItem.Value),
            CheckedMember = nameof(TestItem.Checked)
        };
        protected override bool AssertControlItems(IList<TestItem> items)
            => CheckedListBox.Items.OfType<string>().SequenceEqual(items.Select(item => item.Value)) &&
                Enumerable.Range(0, CheckedListBox.Items.Count).All(index => CheckedListBox.GetItemChecked(index) == items[index].Checked);

        protected override IEnumerable<TestItem> CreateInitialItems() => Enumerable.Range(0, 5).Select(index => new TestItem($"Item {index + 1}", index % 2 == 0));

        [Example("Updates the change of the check state of the checked list box to the items source")]
        void Ex11()
        {
            Given("items", () => Items = new ObservableCollection<TestItem>(InitialItems = CreateInitialItems().ToArray()));
            Given("a ItemsSource", () => ItemsSource = CreateItemsSource());

            When("to bind items to the list box", () => ItemsSource.Bind(Items));
            When("the check state of the checked list box is changed", () =>
            {
                CheckedListBox.SetItemChecked(0, false);
                CheckedListBox.SetItemChecked(1, true);
            });
            Then("the checked value of items source should be changed", () =>
                !Items[0].Checked &&
                Items[1].Checked
            );
        }
    }
}

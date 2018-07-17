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
    [Context("The value and the nullable of the boolean checked value selector are specified")]
    class CheckedListBoxItemsSourceSpec_ValueAndNullableBooleanCheckedSelectorSpecified : ItemsSourceSpecContext<CheckedListBox, TestItem>
    {
        CheckedListBox CheckedListBox { get; } = new CheckedListBox();

        protected override ItemsSource<CheckedListBox, TestItem> CreateItemsSource()
            => new CheckedListBoxItemsSource<TestItem>(CheckedListBox, item => item.ValueProperty, item => item.NullableCheckedProperty);
        protected override bool AssertControlItems(IList<TestItem> items)
            => CheckedListBox.Items.OfType<string>().SequenceEqual(items.Select(item => item.Value)) &&
                Enumerable.Range(0, CheckedListBox.Items.Count).All(index =>
                    CheckedListBox.GetItemCheckState(index) == (items[index].NullableChecked.HasValue ? items[index].NullableChecked.Value
                        ? CheckState.Checked : CheckState.Unchecked : CheckState.Indeterminate)
                );

        protected override IEnumerable<TestItem> CreateInitialItems()
            => Enumerable.Range(0, 5).Select(index => new TestItem($"Item {index + 1}", index % 3 == 0 ? true : index % 3 == 1 ? false : new bool?()));

        [Example("Updates the change of the check state of the checked list box to the items source")]
        void Ex11()
        {
            Given("items", () => Items = new ObservableCollection<TestItem>(InitialItems = CreateInitialItems().ToArray()));
            Given("a ItemsSource", () => ItemsSource = CreateItemsSource());

            When("to bind items to the list box", () => ItemsSource.Bind(Items));
            When("the check state of the checked list box is changed", () =>
            {
                CheckedListBox.SetItemCheckState(0, CheckState.Unchecked);
                CheckedListBox.SetItemCheckState(1, CheckState.Indeterminate);
                CheckedListBox.SetItemCheckState(2, CheckState.Checked);
            });
            Then("the checked value of items source should be changed", () =>
                !Items[0].NullableChecked.Value &&
                !Items[1].NullableChecked.HasValue &&
                Items[2].NullableChecked.Value
            );
            When("the check state of the item is changed", () =>
            {
                Items[2].NullableChecked = false;
                Items[3].NullableChecked = new bool?();
                Items[4].NullableChecked = true;
            });
            Then("the checked value of the checked list box should be changed", () =>
                CheckedListBox.GetItemCheckState(2) == CheckState.Unchecked &&
                CheckedListBox.GetItemCheckState(3) == CheckState.Indeterminate &&
                CheckedListBox.GetItemCheckState(4) == CheckState.Checked
            );
        }
    }
}

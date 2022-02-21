// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.ObjectModel;
using Carna;

namespace Charites.Windows.Forms;

[Context("The value and the boolean checked value selector are specified")]
class CheckedListBoxItemsSourceSpec_ValueAndBooleanCheckedSelectorSpecified : ItemsSourceSpecContext<CheckedListBox, TestItem>
{
    CheckedListBox CheckedListBox { get; } = new();

    protected override ItemsSource<CheckedListBox, TestItem> CreateItemsSource()
        => new CheckedListBoxItemsSource<TestItem>(CheckedListBox, item => item.ValueProperty, item => item.CheckedProperty);
    protected override bool AssertControlItems(IList<TestItem> items)
        => CheckedListBox.Items.OfType<string>().SequenceEqual(items.Select(item => item.Value)) &&
            Enumerable.Range(0, CheckedListBox.Items.Count).All(index => CheckedListBox.GetItemChecked(index) == items[index].Checked);

    protected override IEnumerable<TestItem> CreateInitialItems() => Enumerable.Range(0, 5).Select(index => new TestItem($"Item {index + 1}", index % 2 == 0));

    [Example("Updates the other when either the check state of the item is changed")]
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
        When("the check state of the item is changed", () =>
        {
            Items[2].Checked = false;
            Items[3].Checked = true;
        });
        Then("the checked value of the checked list box should be changed", () =>
            !CheckedListBox.GetItemChecked(2) &&
            CheckedListBox.GetItemChecked(3)
        );
    }
}
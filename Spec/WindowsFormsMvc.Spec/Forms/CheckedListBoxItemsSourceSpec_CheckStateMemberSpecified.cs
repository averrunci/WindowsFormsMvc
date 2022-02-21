﻿// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.ObjectModel;
using Carna;

namespace Charites.Windows.Forms;

[Context("The check state member is specified")]
class CheckedListBoxItemsSourceSpec_CheckStateMemberSpecified : ItemsSourceSpecContext<CheckedListBox, object>
{
    CheckedListBox CheckedListBox { get; } = new();

    protected override ItemsSource<CheckedListBox, object> CreateItemsSource() => new CheckedListBoxItemsSource(CheckedListBox)
    {
        ValueMember = nameof(TestItem.Value),
        CheckedMember = nameof(TestItem.CheckState)
    };
    protected override bool AssertControlItems(IList<TestItem> items)
        => CheckedListBox.Items.OfType<string>().SequenceEqual(items.Select(item => item.Value)) &&
            Enumerable.Range(0, CheckedListBox.Items.Count).All(index => CheckedListBox.GetItemCheckState(index) == items[index].CheckState);

    protected override IEnumerable<TestItem> CreateInitialItems()
        => Enumerable.Range(0, 5).Select(index => new TestItem($"Item {index + 1}", index % 3 == 0 ? CheckState.Checked : index % 3 == 1 ? CheckState.Unchecked : CheckState.Indeterminate));

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
        Then("the check state of items source should be changed", () =>
            Items[0].CheckState == CheckState.Unchecked &&
            Items[1].CheckState == CheckState.Indeterminate &&
            Items[2].CheckState == CheckState.Checked
        );
    }
}
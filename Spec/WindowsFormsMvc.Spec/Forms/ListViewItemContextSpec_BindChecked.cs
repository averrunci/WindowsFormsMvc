// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Context("Binds the checked value")]
class ListViewItemContextSpec_BindChecked : FixtureSteppable
{
    ListViewItemContext<TestItem> ListViewItemContext { get; }

    TestItem Item { get; }
    ListViewItem ListViewItem { get; } = new();

    const bool InitialChecked = true;
    const bool ChangedChecked = false;

    public ListViewItemContextSpec_BindChecked()
    {
        Item = new TestItem { Checked = InitialChecked, Value = InitialChecked.ToString() };
        ListViewItemContext = new ListViewItemContext<TestItem>(Item, ListViewItem);
    }

    [Example("When the selector is specified")]
    void Ex01()
    {
        When("to bind the observable property with the selector", () => ListViewItemContext.BindChecked(item => item.CheckedProperty));
        Then("the checked value of the item of the list view should be set to the value of the bound item", () => ListViewItem.Checked == InitialChecked);

        When("to changed the checked value of the bound item", () => Item.Checked = ChangedChecked);
        Then("the checked value of the item of the list view should be changed", () => ListViewItem.Checked == ChangedChecked);

        When("the context is disposed", () => ListViewItemContext.Dispose());
        When("to changed the checked value of the bound item", () => Item.Checked = InitialChecked);
        Then("the checked value of the item of the list view should not be changed", () => ListViewItem.Checked == ChangedChecked);
    }

    [Example("When the selector and the converter are specified")]
    void Ex02()
    {
        When("to bind the observable property with the selector and the converter", () => ListViewItemContext.BindChecked(item => item.ValueProperty, bool.Parse));
        Then("the converted checked value of the item of the list view should be set to the value of the bound item", () => ListViewItem.Checked == InitialChecked);

        When("to changed the checked value of the bound item", () => Item.Value = ChangedChecked.ToString());
        Then("the converted checked value of the item of the list view should be changed", () => ListViewItem.Checked == ChangedChecked);

        When("the context is disposed", () => ListViewItemContext.Dispose());
        When("to changed the checked value of the bound item", () => Item.Value = InitialChecked.ToString());
        Then("the converted checked value of the item of the list view should not be changed", () => ListViewItem.Checked == ChangedChecked);
    }
}
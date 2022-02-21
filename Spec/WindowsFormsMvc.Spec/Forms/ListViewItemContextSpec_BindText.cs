// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Context("Binds the text value")]
class ListViewItemContextSpec_BindText : FixtureSteppable
{
    ListViewItemContext<TestItem> ListViewItemContext { get; }

    TestItem Item { get; }
    ListViewItem ListViewItem { get; } = new();

    const int SubItemIndex = 1;

    const string InitialText = "Initial Text";
    const string ChangedText = "Changed Text";

    const bool InitialChecked = true;
    const bool ChangedChecked = false;

    public ListViewItemContextSpec_BindText()
    {
        Item = new TestItem { Value = InitialText, Checked = InitialChecked };
        ListViewItemContext = new ListViewItemContext<TestItem>(Item, ListViewItem);
    }

    [Example("When the selector is specified")]
    void Ex01()
    {
        When("to bind the observable property with the selector", () => ListViewItemContext.BindText(SubItemIndex, item => item.ValueProperty));
        Then("the text value of the sub item of the item of the list view should be set to the value of the bound item", () => ListViewItem.SubItems[SubItemIndex].Text == InitialText);

        When("to changed the text value of the bound item", () => Item.Value = ChangedText);
        Then("the text value of the sub item of the item of the list view should be changed", () => ListViewItem.SubItems[SubItemIndex].Text == ChangedText);

        When("the context is disposed", () => ListViewItemContext.Dispose());
        When("to changed the text value of the bound item", () => Item.Value = InitialText);
        Then("the text value of the sub item of the item of the list view should not be changed", () => ListViewItem.SubItems[SubItemIndex].Text == ChangedText);
    }

    [Example("When the selector and the converter are specified")]
    void Ex02()
    {
        When("to bind the observable property with the selector and the converter", () => ListViewItemContext.BindText(SubItemIndex, item => item.CheckedProperty, value => value.ToString()));
        Then("the converted text value of the sub item of the item of the list view should be set to the value of the bound item", () => ListViewItem.SubItems[SubItemIndex].Text == InitialChecked.ToString());

        When("to changed the text value of the bound item", () => Item.Checked = ChangedChecked);
        Then("the converted text value of the sub item of the item of the list view should be changed", () => ListViewItem.SubItems[SubItemIndex].Text == ChangedChecked.ToString());

        When("the context is disposed", () => ListViewItemContext.Dispose());
        When("to changed the text value of the bound item", () => Item.Checked = InitialChecked);
        Then("the converted text value of the sub item of the item of the list view should not be changed", () => ListViewItem.SubItems[SubItemIndex].Text == ChangedChecked.ToString());
    }
}
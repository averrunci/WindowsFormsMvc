// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Context("Binds the back color")]
class ListViewItemContextSpec_BindBackColor : FixtureSteppable
{
    ListViewItemContext<TestItem> ListViewItemContext { get; }

    TestItem Item { get; }
    ListViewItem ListViewItem { get; } = new();

    const int SubItemIndex = 1;

    static Color InitialBackColor { get; } = Color.AliceBlue;
    static Color ChangedBackColor { get; } = Color.Aqua;

    public ListViewItemContextSpec_BindBackColor()
    {
        Item = new TestItem { BackColor = InitialBackColor, Value = InitialBackColor.Name };
        ListViewItemContext = new ListViewItemContext<TestItem>(Item, ListViewItem);
    }

    [Example("When the selector is specified")]
    void Ex01()
    {
        When("to bind the observable property with the selector", () => ListViewItemContext.BindBackColor(SubItemIndex, item => item.BackColorProperty));
        Then("the back color value of the sub item of the item of the list view should be set to the value of the bound item", () => ListViewItem.SubItems[SubItemIndex].BackColor == InitialBackColor);

        When("to changed the back color value of the bound item", () => Item.BackColor = ChangedBackColor);
        Then("the back color value of the sub item of the item of the list view should be changed", () => ListViewItem.SubItems[SubItemIndex].BackColor == ChangedBackColor);

        When("the context is disposed", () => ListViewItemContext.Dispose());
        When("to changed the back color value of the bound item", () => Item.BackColor = InitialBackColor);
        Then("the back color value of the sub item of the item of the list view should not be changed", () => ListViewItem.SubItems[SubItemIndex].BackColor == ChangedBackColor);
    }

    [Example("When the selector and the converter are specified")]
    void Ex02()
    {
        When("to bind the observable property with the selector and the converter", () => ListViewItemContext.BindBackColor(SubItemIndex, item => item.ValueProperty, Color.FromName));
        Then("the converted back color value of the sub item of the item of the list view should be set to the value of the bound item", () => ListViewItem.SubItems[SubItemIndex].BackColor == InitialBackColor);

        When("to changed the back color value of the bound item", () => Item.Value = ChangedBackColor.Name);
        Then("the converted back color value of the sub item of the item of the list view should be changed", () => ListViewItem.SubItems[SubItemIndex].BackColor == ChangedBackColor);

        When("the context is disposed", () => ListViewItemContext.Dispose());
        When("to changed the back color value of the bound item", () => Item.Value = InitialBackColor.Name);
        Then("the converted back color value of the sub item of the item of the list view should not be changed", () => ListViewItem.SubItems[SubItemIndex].BackColor == ChangedBackColor);
    }
}
// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Context("Binds the font")]
class ListViewItemContextSpec_BindFont : FixtureSteppable
{
    ListViewItemContext<TestItem> ListViewItemContext { get; }

    TestItem Item { get; }
    ListViewItem ListViewItem { get; } = new();

    const int SubItemIndex = 1;

    static Font InitialFont { get; } = new(FontFamily.GenericSerif, 12);
    static Font ChangedFont { get; } = new(FontFamily.GenericSansSerif, 12);

    public ListViewItemContextSpec_BindFont()
    {
        Item = new TestItem { Font = InitialFont, Value = InitialFont.FontFamily.Name };
        ListViewItemContext = new ListViewItemContext<TestItem>(Item, ListViewItem);
    }

    [Example("When the selector is specified")]
    void Ex01()
    {
        When("to bind the observable property with the selector", () => ListViewItemContext.BindFont(SubItemIndex, item => item.FontProperty));
        Then("the font value of the sub item of the item of the list view should be set to the value of the bound item", () => Equals(ListViewItem.SubItems[SubItemIndex].Font, InitialFont));

        When("to changed the font value of the bound item", () => Item.Font = ChangedFont);
        Then("the font value of the sub item of the item of the list view should be changed", () => Equals(ListViewItem.SubItems[SubItemIndex].Font, ChangedFont));

        When("the context is disposed", () => ListViewItemContext.Dispose());
        When("to changed the font value of the bound item", () => Item.Font = InitialFont);
        Then("the font value of the sub item of the item of the list view should not be changed", () => Equals(ListViewItem.SubItems[SubItemIndex].Font, ChangedFont));
    }

    [Example("When the selector and the converter are specified")]
    void Ex02()
    {
        When("to bind the observable property with the selector and the converter", () => ListViewItemContext.BindFont(SubItemIndex, item => item.ValueProperty, value => new Font(value == FontFamily.GenericSansSerif.Name ? FontFamily.GenericSansSerif : FontFamily.GenericSerif, 12)));
        Then("the converted font value of the sub item of the item of the list view should be set to the value of the bound item", () => Equals(ListViewItem.SubItems[SubItemIndex].Font, InitialFont));

        When("to changed the font value of the bound item", () => Item.Value = ChangedFont.FontFamily.Name);
        Then("the converted font value of the sub item of the item of the list view should be changed", () => Equals(ListViewItem.SubItems[SubItemIndex].Font, ChangedFont));

        When("the context is disposed", () => ListViewItemContext.Dispose());
        When("to changed the font value of the bound item", () => Item.Value = InitialFont.FontFamily.Name);
        Then("the converted font value of the sub item of the item of the list view should not be changed", () => Equals(ListViewItem.SubItems[SubItemIndex].Font, ChangedFont));
    }
}
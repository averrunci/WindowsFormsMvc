// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

using System.Drawing;
using System.Windows.Forms;
using Carna;

namespace Charites.Windows.Forms
{
    [Context("Binds the fore color")]
    class ListViewItemContextSpec_BindForeColor : FixtureSteppable
    {
        ListViewItemContext<TestItem> ListViewItemContext { get; }

        TestItem Item { get; }
        ListViewItem ListViewItem { get; } = new ListViewItem();

        const int SubItemIndex = 1;

        static Color InitialForeColor { get; } = Color.DodgerBlue;
        static Color ChangedForeColor { get; } = Color.DarkBlue;

        public ListViewItemContextSpec_BindForeColor()
        {
            Item = new TestItem { ForeColor = InitialForeColor, Value = InitialForeColor.Name };
            ListViewItemContext = new ListViewItemContext<TestItem>(Item, ListViewItem);
        }

        [Example("When the selector is specified")]
        void Ex01()
        {
            When("to bind the observable property with the selector", () => ListViewItemContext.BindForeColor(SubItemIndex, item => item.ForeColorProperty));
            Then("the fore color value of the sub item of the item of the list view should be set to the value of the bound item", () => ListViewItem.SubItems[SubItemIndex].ForeColor == InitialForeColor);

            When("to changed the fore color value of the bound item", () => Item.ForeColor = ChangedForeColor);
            Then("the fore color value of the sub item of the item of the list view should be changed", () => ListViewItem.SubItems[SubItemIndex].ForeColor == ChangedForeColor);

            When("the context is disposed", () => ListViewItemContext.Dispose());
            When("to changed the fore color value of the bound item", () => Item.ForeColor = InitialForeColor);
            Then("the fore color value of the sub item of the item of the list view should not be changed", () => ListViewItem.SubItems[SubItemIndex].ForeColor == ChangedForeColor);
        }

        [Example("When the selector and the converter are specified")]
        void Ex02()
        {
            When("to bind the observable property with the selector and the converter", () => ListViewItemContext.BindForeColor(SubItemIndex, item => item.ValueProperty, Color.FromName));
            Then("the converted fore color value of the sub item of the item of the list view should be set to the value of the bound item", () => ListViewItem.SubItems[SubItemIndex].ForeColor == InitialForeColor);

            When("to changed the fore color value of the bound item", () => Item.Value = ChangedForeColor.Name);
            Then("the converted fore color value of the sub item of the item of the list view should be changed", () => ListViewItem.SubItems[SubItemIndex].ForeColor == ChangedForeColor);

            When("the context is disposed", () => ListViewItemContext.Dispose());
            When("to changed the fore color value of the bound item", () => Item.Value = InitialForeColor.Name);
            Then("the converted fore color value of the sub item of the item of the list view should not be changed", () => ListViewItem.SubItems[SubItemIndex].ForeColor == ChangedForeColor);
        }
    }
}

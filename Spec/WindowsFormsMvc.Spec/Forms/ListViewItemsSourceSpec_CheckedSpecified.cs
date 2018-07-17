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
    [Context("The checked selector is specified")]
    class ListViewItemsSourceSpec_CheckedSpecified : ItemsSourceSpecContext<ListView, TestItem>
    {
        ListView ListView { get; } = new ListView { CheckBoxes = true };

        protected override ItemsSource<ListView, TestItem> CreateItemsSource() => new ListViewItemsSource<TestItem>(ListView)
        {
            CheckedSelector = item => item.CheckedProperty
        };
        protected override bool AssertControlItems(IList<TestItem> items)
            => ListView.Items.Count == items.Count &&
                Enumerable.Range(0, ListView.Items.Count).All(index => ListView.Items[index].Checked == items[index].Checked);

        protected override IEnumerable<TestItem> CreateInitialItems() => Enumerable.Range(0, 5).Select(index => new TestItem($"Item {index + 1}", index % 2 == 0));

        public ListViewItemsSourceSpec_CheckedSpecified()
        {
            ListView.CreateControl();
        }

        [Example("Updates the change of the checked value of the item of the list view to the items source")]
        void Ex11()
        {
            Given("items", () => Items = new ObservableCollection<TestItem>(InitialItems = CreateInitialItems().ToArray()));
            Given("a ItemsSource", () => ItemsSource = CreateItemsSource());

            When("to bind items to the list view", () => ItemsSource.Bind(Items));
            When("the checked value of the item of the list view is changed", () =>
            {
                ListView.Items[0].Checked = false;
                ListView.Items[1].Checked = true;
            });
            Then("the checked value of items source should be changed", () =>
                !Items[0].Checked &&
                Items[1].Checked
            );
        }
    }
}

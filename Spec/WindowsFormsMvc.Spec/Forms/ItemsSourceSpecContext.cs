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
    abstract class ItemsSourceSpecContext<TControl, TItem> : FixtureSteppable where TControl : Control
    {
        protected ItemsSource<TControl, TItem> ItemsSource { get; set; }

        protected TestItem[] InitialItems { get; set; }
        protected ObservableCollection<TestItem> Items { get; set; }

        protected abstract ItemsSource<TControl, TItem> CreateItemsSource();
        protected abstract bool AssertControlItems(IList<TestItem> items);

        protected virtual IEnumerable<TestItem> CreateInitialItems() => Enumerable.Range(1, 5).Select(no => new TestItem($"Item {no}"));

        [Example("Binds items")]
        protected void Ex01()
        {
            Given("items", () => Items = new ObservableCollection<TestItem>(InitialItems = CreateInitialItems().ToArray()));
            Given("a ItemsSource", () => ItemsSource = CreateItemsSource());

            When("to bind items to the list box", () => ItemsSource.Bind(Items));
            Then("the items should be set to the list box", () => AssertControlItems(Items));

            When("a new item is added to the collection", () => Items.Add(new TestItem("Added Item")));
            Then("the item should be added to the list box", () => AssertControlItems(Items));

            When("a new item is inserted into the collection", () => Items.Insert(1, new TestItem("Inserted Item")));
            Then("the item should be inserted into the list box", () => AssertControlItems(Items));

            When("an item is removed from the collection", () => Items.RemoveAt(2));
            Then("the item should be removed from the list box", () => AssertControlItems(Items));

            When("an item is moved into the collection", () => Items.Move(3, 2));
            Then("the item should be moved into the list box", () => AssertControlItems(Items));

            When("an item is replaced in the collection", () => Items[4] = new TestItem("Replaced Item"));
            Then("the item should be replaced in the list box", () => AssertControlItems(Items));

            When("all items in the collection are cleared", () => Items.Clear());
            Then("all items in the list box should be cleared", () => AssertControlItems(new List<TestItem>()));
        }

        [Example("Unbinds items")]
        protected void Ex02()
        {
            Given("items", () => Items = new ObservableCollection<TestItem>(InitialItems = CreateInitialItems().ToArray()));
            Given("a ItemsSource", () => ItemsSource = CreateItemsSource());

            When("to bind items to the list box", () => ItemsSource.Bind(Items));
            Then("the items should be set to the list box", () => AssertControlItems(Items));

            When("to unbind items from the list box", () => ItemsSource.Unbind());

            When("a new item is added to the collection", () => Items.Add(new TestItem("Added Item")));
            Then("the item should not be added to the list box", () => AssertControlItems(InitialItems));

            When("a new item is inserted into the collection", () => Items.Insert(1, new TestItem("Inserted Item")));
            Then("the item should not be inserted into the list box", () => AssertControlItems(InitialItems));

            When("an item is removed from the collection", () => Items.RemoveAt(2));
            Then("the item should not be removed from the list box", () => AssertControlItems(InitialItems));

            When("an item is moved into the collection", () => Items.Move(3, 2));
            Then("the item should not be moved into the list box", () => AssertControlItems(InitialItems));

            When("an item is replaced in the collection", () => Items[4] = new TestItem("Replaced Item"));
            Then("the item should not be replaced in the list box", () => AssertControlItems(InitialItems));

            When("all items in the collection are cleared", () => Items.Clear());
            Then("all items in the list box should not be cleared", () => AssertControlItems(InitialItems));
        }
    }
}

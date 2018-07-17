// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using Carna;
using NSubstitute;

namespace Charites.Windows.Forms
{
    [Context("Item bound")]
    class ListViewItemsSourceSpec_ItemBound : ItemsSourceSpecContext<ListView, TestItem>
    {
        ListView ListView { get; } = new ListView();

        ListViewItemBoundEventHandler<TestItem> ItemBoundHandler { get; } = Substitute.For<ListViewItemBoundEventHandler<TestItem>>();
        TestItem Item { get; set; }

        protected override ItemsSource<ListView, TestItem> CreateItemsSource() => new ListViewItemsSource<TestItem>(ListView);
        protected override bool AssertControlItems(IList<TestItem> items) => ListView.Items.Count == items.Count;

        [Example("ItemBound event")]
        void Ex11()
        {
            Given("items", () => Items = new ObservableCollection<TestItem>(InitialItems = CreateInitialItems().ToArray()));
            Given("a ItemsSource", () =>
            {
                ItemsSource = CreateItemsSource();
                ((ListViewItemsSource<TestItem>)ItemsSource).ItemBound += ItemBoundHandler;
            });

            When("to bind items to the list box", () => ItemsSource.Bind(Items));
            Then("the ItemBound event should be raised", () => ItemBoundHandler.Received(InitialItems.Length).Invoke(ItemsSource, Arg.Is<ListViewItemBoundEventArgs<TestItem>>(e =>
                ListView.Items.Contains(e.ListViewItemContext.ListViewItem) && InitialItems.Contains(e.ListViewItemContext.Item))
            ));

            ItemBoundHandler.ClearReceivedCalls();
            When("a new item is added to the collection", () => Items.Add(Item = new TestItem("Added Item")));
            Then("the ItemBound event should be raised", () => ItemBoundHandler.Received(1).Invoke(ItemsSource, Arg.Is<ListViewItemBoundEventArgs<TestItem>>(e =>
                ListView.Items.Contains(e.ListViewItemContext.ListViewItem) && e.ListViewItemContext.Item == Item))
            );

            ItemBoundHandler.ClearReceivedCalls();
            When("a new item is inserted into the collection", () => Items.Insert(1, Item = new TestItem("Inserted Item")));
            Then("the ItemBound event should be raised", () => ItemBoundHandler.Received(1).Invoke(ItemsSource, Arg.Is<ListViewItemBoundEventArgs<TestItem>>(e =>
                ListView.Items.Contains(e.ListViewItemContext.ListViewItem) && e.ListViewItemContext.Item == Item))
            );

            ItemBoundHandler.ClearReceivedCalls();
            When("an item is removed from the collection", () => Items.RemoveAt(2));
            Then("the ItemBound event should not be raised", () => ItemBoundHandler.Received(0).Invoke(null, null));

            ItemBoundHandler.ClearReceivedCalls();
            When("an item is moved into the collection", () => Items.Move(3, 2));
            Then("the ItemBound event should not be raised", () => ItemBoundHandler.Received(0).Invoke(null, null));

            ItemBoundHandler.ClearReceivedCalls();
            When("an item is replaced in the collection", () => Items[4] = Item = new TestItem("Replaced Item"));
            Then("the ItemBound event should be raised", () => ItemBoundHandler.Received(1).Invoke(ItemsSource, Arg.Is<ListViewItemBoundEventArgs<TestItem>>(e =>
                ListView.Items.Contains(e.ListViewItemContext.ListViewItem) && e.ListViewItemContext.Item == Item))
            );

            ItemBoundHandler.ClearReceivedCalls();
            When("all items in the collection are cleared", () => Items.Clear());
            Then("the ItemBound event should not be raised", () => ItemBoundHandler.Received(0).Invoke(null, null));
        }
    }
}

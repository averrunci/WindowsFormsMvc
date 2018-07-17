// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Carna;

namespace Charites.Windows.Forms
{
    [Context("The header member is specified")]
    class TabControlItemsSourceSpec_HeaderMemberSpecified : ItemsSourceSpecContext<TabControl, object>
    {
        TabControl TabControl { get; } = new TabControl();

        public TabControlItemsSourceSpec_HeaderMemberSpecified()
        {
            TabControl.CreateControl();
        }

        protected override ItemsSource<TabControl, object> CreateItemsSource() => new TabControlItemsSource(TabControl)
        {
            HeaderMember = nameof(TestItem.Value)
        };
        protected override bool AssertControlItems(IList<TestItem> items)
            => TabControl.TabPages.OfType<TabPage>().Select(tabPage => tabPage.Text).SequenceEqual(items.Select(item => item.Value)) &&
                TabControl.TabPages.OfType<TabPage>().Select(tabPage => tabPage.Controls.Count).All(count => count == 1) &&
                TabControl.TabPages.OfType<TabPage>().Select(tabPage => (tabPage.Controls[0] as ContentControl)?.Content).SequenceEqual(items);
    }
}

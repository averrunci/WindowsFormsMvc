// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Windows.Forms;
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home
{
    [ContentView(typeof(HomeContent))]
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void BindContent(HomeContent homeContent)
        {
            if (homeContent == null) return;

            messageLabel.Text = homeContent.Message;
        }

        private void UnbindContent(HomeContent homeContent)
        {
            if (homeContent == null) return;

            messageLabel.Text = string.Empty;
        }

        private void dataContextSource_DataContextChanged(object sender, Mvc.DataContextChangedEventArgs e)
        {
            UnbindContent(e.OldValue as HomeContent);
            BindContent(e.NewValue as HomeContent);
        }
    }
}

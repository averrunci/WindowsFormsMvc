// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Windows.Forms;
using Charites.Windows.Forms;
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
{
    [ContentView(typeof(ApplicationHost))]
    public partial class ApplicationHostView : UserControl
    {
        private readonly ObservablePropertyBindings observablePropertyBindings = new ObservablePropertyBindings();

        public ApplicationHostView()
        {
            InitializeComponent();
        }

        private void BindContent(ApplicationHost host)
        {
            if (host == null) return;

            observablePropertyBindings.Bind(host.Content, contentControl, nameof(contentControl.Content));
        }

        private void UnbindContent(ApplicationHost host)
        {
            if (host == null) return;

            observablePropertyBindings.Unbind();
        }

        private void dataContextSource_DataContextChanged(object sender, DataContextChangedEventArgs e)
        {
            UnbindContent(e.OldValue as ApplicationHost);
            BindContent(e.NewValue as ApplicationHost);
        }
    }
}

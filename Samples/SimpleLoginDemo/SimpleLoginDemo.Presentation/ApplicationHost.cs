// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
{
    public class ApplicationHost
    {
        public ObservableProperty<ILoginDemoContent> Content { get; } = new ObservableProperty<ILoginDemoContent>();

        public ApplicationHost(ILoginDemoContent initialContent)
        {
            Content.PropertyValueChanged += (s, e) =>
            {
                if (e.OldValue != null) e.OldValue.ContentRequested -= OnContentRequested;
                if (e.NewValue != null) e.NewValue.ContentRequested += OnContentRequested;
            };
            Content.Value = initialContent;
        }

        private void OnContentRequested(object sender, ContentRequestedEventArgs e)
        {
            Content.Value = e.Content;
        }
    }
}

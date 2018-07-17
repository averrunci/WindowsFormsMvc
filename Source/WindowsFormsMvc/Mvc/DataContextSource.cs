// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;

namespace Charites.Windows.Mvc
{
    /// <summary>
    /// Represents a source of the data context.
    /// </summary>
    public class DataContextSource : Component
    {
        /// <summary>
        /// Occurs when the data context is changed.
        /// </summary>
        public event DataContextChangedEventHandler DataContextChanged;

        /// <summary>
        /// Gets or sets the data context value.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value
        {
            get => value;
            set
            {
                if (this.value == value) return;

                var e = new DataContextChangedEventArgs(this.value, value);
                this.value = value;
                OnDataContextChanged(e);
            }
        }
        private object value;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextSource"/> class.
        /// </summary>
        public DataContextSource()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextSource"/> class
        /// with the specified container.
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> to add the current <see cref="DataContextSource"/> to.</param>
        public DataContextSource(IContainer container)
        {
            container?.Add(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by this component and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Value = null;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Raises the <see cref="DataContextChanged"/> event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnDataContextChanged(DataContextChangedEventArgs e) => DataContextChanged?.Invoke(this, e);
    }
}

// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Charites.Windows.Mvc
{
    /// <summary>
    /// Represents a collection of controller objects.
    /// </summary>
    public sealed class WindowsFormsControllerCollection : ControllerCollection<Control>
    {
        private readonly IWindowsFormsDataContextFinder dataContextFinder;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsFormsControllerCollection"/> class
        /// with the specified <see cref="IWindowsFormsDataContextFinder" />, <see cref="IDataContextInjector" />,
        /// <see cref="IWindowsFormsControlInjector" />, and the enumerable of the <see cref="IWindowsFormsControllerExtension" />.
        /// </summary>
        /// <param name="dataContextFinder">The finder to find a data context.</param>
        /// <param name="dataContextInjector">The injector to inject a data context.</param>
        /// <param name="elementInjector">The injector to inject elements.</param>
        /// <param name="extensions">The extensions for a controller.</param>
        public WindowsFormsControllerCollection(IWindowsFormsDataContextFinder dataContextFinder, IDataContextInjector dataContextInjector, IWindowsFormsControlInjector elementInjector, IEnumerable<IWindowsFormsControllerExtension> extensions) : base(dataContextFinder, dataContextInjector, elementInjector, extensions)
        {
            this.dataContextFinder = dataContextFinder;
        }

        /// <summary>
        /// Adds the controllers of the specified collection to the end of the <see cref="WindowsFormsControllerCollection"/>.
        /// </summary>
        /// <param name="controllers">
        /// The controllers to add to the end of the <see cref="WindowsFormsControllerCollection"/>.
        /// If the specified collection is <c>null</c>, nothing is added without throwing an exception.
        /// </param>
        public void AddRange(IEnumerable<object> controllers) => controllers.ForEach(Add);

        /// <summary>
        /// Gets the value that indicates whether the control to which controllers are attached is created.
        /// </summary>
        /// <param name="associatedControl">The control to which controllers are attached.</param>
        /// <returns>
        /// <c>true</c> if the control to which controllers are attached is created;
        /// otherwise, <c>false</c> is returned.
        /// </returns>
        protected override bool IsAssociatedElementLoaded(Control associatedControl) => associatedControl.IsHandleCreated;

        /// <summary>
        /// Subscribes events of the control to which controllers are attached.
        /// </summary>
        /// <param name="associatedControl">The control to which controllers are attached.</param>
        protected override void SubscribeAssociatedElementEvents(Control associatedControl)
        {
            associatedControl.HandleCreated += OnControlHandleCreated;
            associatedControl.Disposed += OnControlDisposed;

            associatedControl.AddDataContextChangedHandler(OnControlDataContextChanged, dataContextFinder);
        }

        /// <summary>
        /// Unsubscribes events of the control to which controllers are attached.
        /// </summary>
        /// <param name="associatedControl">The control to which controllers are attached.</param>
        protected override void UnsubscribeAssociatedElementEvents(Control associatedControl)
        {
            associatedControl.HandleCreated -= OnControlHandleCreated;
            associatedControl.Disposed -= OnControlDisposed;

            associatedControl.RemoveDataContextChangedHandler(OnControlDataContextChanged, dataContextFinder);
        }

        private void OnControlHandleCreated(object sender, EventArgs e)
        {
            if (!(sender is Control control)) return;

            SetElement(control);
            AttachExtensions();
        }

        private void OnControlDataContextChanged(object sender, DataContextChangedEventArgs e)
        {
            SetDataContext(e.NewValue);
        }

        private void OnControlDisposed(object sender, EventArgs e)
        {
            Detach();
        }
    }
}

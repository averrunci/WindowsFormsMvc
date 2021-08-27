// Copyright (C) 2018-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Charites.Windows.Mvc
{
    /// <summary>
    /// Represents an item of an event handler.
    /// </summary>
    public sealed class WindowsFormsEventHandlerItem : EventHandlerItem<Control>
    {
        private readonly EventInfo eventInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsFormsEventHandlerItem"/> class
        /// with the specified element name, element, event name, event information,
        /// event handler, and a value that indicates whether to register the handler such that
        /// it is invoked even when the event is marked handled in its event data.
        /// </summary>
        /// <param name="elementName">The name of the element that raises the event.</param>
        /// <param name="element">The element that raises the event.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="eventInfo">The information of the event that is raised</param>
        /// <param name="handler">The handler of the event.</param>
        /// <param name="handledEventsToo">
        /// <c>true</c> to register the handler such that it is invoked even when the
        /// event is marked handled in its event data; <c>false</c> to register the
        /// handler with the default condition that it will not be invoked if the event
        /// is already marked handled.
        /// </param>
        public WindowsFormsEventHandlerItem(string elementName, Control element, string eventName, EventInfo eventInfo, Delegate handler, bool handledEventsToo) : base(elementName, element, eventName, handler, handledEventsToo)
        {
            this.eventInfo = eventInfo;
        }

        /// <summary>
        /// Adds the specified event handler to the specified element.
        /// </summary>
        /// <param name="element">The element to which the specified event handler is added.</param>
        /// <param name="handler">The event handler to add.</param>
        /// <param name="handledEventsToo">
        /// <c>true</c> to register the handler such that it is invoked even when the
        /// event is marked handled in its event data; <c>false</c> to register the
        /// handler with the default condition that it will not be invoked if the event
        /// is already marked handled.
        /// </param>
        protected override void AddEventHandler(Control element, Delegate handler, bool handledEventsToo)
        {
            if (element == null || handler == null || eventInfo == null) return;

            eventInfo.AddMethod?.Invoke(element, new object[] { handler });
        }

        /// <summary>
        /// Removes the specified event handler from the specified element.
        /// </summary>
        /// <param name="element">The element from which the specified event handler is removed.</param>
        /// <param name="handler">The event handler to remove.</param>
        protected override void RemoveEventHandler(Control element, Delegate handler)
        {
            if (element == null || handler == null || eventInfo == null) return;

            eventInfo.RemoveMethod?.Invoke(element, new object[] { handler });
        }

        /// <summary>
        /// Creates the resolver to resolver dependencies of parameters.
        /// </summary>
        /// <param name="dependencyResolver">The resolver to resolver dependencies of parameters.</param>
        /// <returns>The resolver to resolver dependencies of parameters.</returns>
        protected override IParameterDependencyResolver CreateParameterDependencyResolver(IDictionary<Type, Func<object>> dependencyResolver)
        {
            return new WindowsFormsParameterDependencyResolver(dependencyResolver);
        }
    }
}

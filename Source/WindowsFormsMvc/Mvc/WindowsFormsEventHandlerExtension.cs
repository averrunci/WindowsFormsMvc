// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Charites.Windows.Mvc
{
    internal sealed class WindowsFormsEventHandlerExtension : EventHandlerExtension<Control, WindowsFormsEventHandlerItem>, IWindowsFormsControllerExtension
    {
        private static readonly ConditionalWeakTable<Control, IDictionary<object, EventHandlerBase<Control, WindowsFormsEventHandlerItem>>> EventHandlerBaseRepository = new ConditionalWeakTable<Control, IDictionary<object, EventHandlerBase<Control, WindowsFormsEventHandlerItem>>>();

        protected override IDictionary<object, EventHandlerBase<Control, WindowsFormsEventHandlerItem>> EnsureEventHandlerBases(Control element)
        {
            if (element == null) return new Dictionary<object, EventHandlerBase<Control, WindowsFormsEventHandlerItem>>();

            if (EventHandlerBaseRepository.TryGetValue(element, out var foundEventHandlerBases)) return foundEventHandlerBases;

            var eventHandlerBases = new Dictionary<object, EventHandlerBase<Control, WindowsFormsEventHandlerItem>>();
            EventHandlerBaseRepository.Add(element, eventHandlerBases);
            return eventHandlerBases;
        }

        protected override void AddEventHandler(Control element, EventHandlerAttribute eventHandlerAttribute, Func<Type, Delegate> handlerCreator, EventHandlerBase<Control, WindowsFormsEventHandlerItem> eventHandlers)
        {
            var targetElement = element.FindControl(eventHandlerAttribute.ElementName);
            var eventInfo = RetrieveEventInfo(targetElement, eventHandlerAttribute.Event);
            eventHandlers.Add(new WindowsFormsEventHandlerItem(
                eventHandlerAttribute.ElementName, targetElement,
                eventHandlerAttribute.Event, eventInfo,
                handlerCreator(eventInfo?.EventHandlerType), eventHandlerAttribute.HandledEventsToo
            ));
        }

        protected override EventHandlerAction CreateEventHandlerAction(MethodInfo method, object target) => new WindowsFormsEventHandlerAction(method, target);

        private EventInfo RetrieveEventInfo(Control element, string name)
            => element?.GetType().GetEvents().FirstOrDefault(e => e.Name == name);
    }
}

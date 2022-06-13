// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Charites.Windows.Mvc;

internal sealed class WindowsFormsEventHandlerExtension : EventHandlerExtension<Component, WindowsFormsEventHandlerItem>, IWindowsFormsControllerExtension
{
    private static readonly ConditionalWeakTable<Component, IDictionary<object, EventHandlerBase<Component, WindowsFormsEventHandlerItem>>> EventHandlerBaseRepository = new();

    public WindowsFormsEventHandlerExtension()
    {
        Add<WindowsFormsEventHandlerParameterFromDIResolver>();
        Add<WindowsFormsEventHandlerParameterFromElementResolver>();
        Add<WindowsFormsEventHandlerParameterFromDataContextResolver>();
    }

    protected override IDictionary<object, EventHandlerBase<Component, WindowsFormsEventHandlerItem>> EnsureEventHandlerBases(Component? element)
    {
        if (element is null) return new Dictionary<object, EventHandlerBase<Component, WindowsFormsEventHandlerItem>>();

        if (EventHandlerBaseRepository.TryGetValue(element, out var foundEventHandlerBases)) return foundEventHandlerBases;

        var eventHandlerBases = new Dictionary<object, EventHandlerBase<Component, WindowsFormsEventHandlerItem>>();
        EventHandlerBaseRepository.Add(element, eventHandlerBases);
        return eventHandlerBases;
    }

    protected override void AddEventHandler(Component? element, EventHandlerAttribute eventHandlerAttribute, Func<Type?, Delegate?> handlerCreator, EventHandlerBase<Component, WindowsFormsEventHandlerItem> eventHandlers)
    {
        var targetElement = element.FindComponent(eventHandlerAttribute.ElementName);
        var eventInfo = RetrieveEventInfo(targetElement, eventHandlerAttribute.Event);
        eventHandlers.Add(new WindowsFormsEventHandlerItem(
            eventHandlerAttribute.ElementName, targetElement,
            eventHandlerAttribute.Event, eventInfo,
            handlerCreator(eventInfo?.EventHandlerType), eventHandlerAttribute.HandledEventsToo,
            CreateParameterResolver(element)
        ));
    }

    protected override EventHandlerAction CreateEventHandlerAction(MethodInfo method, object? target, Component? element)
        => new WindowsFormsEventHandlerAction(method, target, CreateParameterDependencyResolver(CreateParameterResolver(element)));

    private EventInfo? RetrieveEventInfo(Component? element, string name)
        => element?.GetType().GetEvents().FirstOrDefault(e => e.Name == name);
}
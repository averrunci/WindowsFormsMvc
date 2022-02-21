// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;

namespace Charites.Windows.Mvc;

/// <summary>
/// Provides the function to find a type of a view that is associated with a content.
/// </summary>
/// <remarks>
/// The type of the view is searched using the type of the content, the base type of the content,
/// or interfaces the content implements.
/// </remarks>
public class ContentViewTypeFinder : IContentViewTypeFinder
{
    /// <summary>
    /// Finds a type of a view that is associated with the specified content.
    /// </summary>
    /// <param name="content">The content with which the view is associated.</param>
    /// <returns>The type of the view that is associated with the specified content.</returns>
    protected virtual Type? FindContentViewType(object content) => FindContentViewType(content.GetType());

    /// <summary>
    /// Finds a type of a view that is associated with the specified type of the content.
    /// </summary>
    /// <param name="contentType">The type of the content with which the view is associated.</param>
    /// <returns>The type of the view that is associated with the specified type of the content.</returns>
    protected virtual Type? FindContentViewType(Type? contentType)
        => contentType is null ? null : SelectContentViewType(contentType) ??
            FindContentViewType(contentType.BaseType) ??
            contentType.GetInterfaces().Select(FindContentViewType).FirstOrDefault(type => type is not null);

    /// <summary>
    /// Selects a type of view that is associated with the specified type of the content.
    /// </summary>
    /// <param name="contentType">The type of the content with which the view is associated.</param>
    /// <returns>Tye type of the view that is associated with the specified type of the content.</returns>
    protected virtual Type? SelectContentViewType(Type contentType)
        => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Select(type => new
            {
                Type = type,
                ContentView = type.GetCustomAttribute<ContentViewAttribute>(true)
            })
            .Where(x => x.ContentView?.ContentType == contentType)
            .OrderByDescending(x => x.ContentView!.Priority)
            .FirstOrDefault()?.Type;

    Type? IContentViewTypeFinder.Find(object content)
        => FindContentViewType(content);
}
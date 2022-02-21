// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc;

/// <summary>
/// Provides the function to find a type of a view that is associated with a content.
/// </summary>
public interface IContentViewTypeFinder
{
    /// <summary>
    /// Finds a type of a view that is associated with the specified content.
    /// </summary>
    /// <param name="content">The content with which the view is associated.</param>
    /// <returns>The type of the view that is associated with the specified content.</returns>
    Type? Find(object content);
}
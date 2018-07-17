// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Charites.Windows.Mvc
{
    /// <summary>
    /// Specifies the view that is associated with the content.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ContentViewAttribute : Attribute
    {
        /// <summary>
        /// Gets the type of the content with which the view is associated.
        /// </summary>
        public Type ContentType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentViewAttribute"/> class
        /// with the type of the content with which the view is associated.
        /// </summary>
        /// <param name="contentType">The type of the content with which the view is associated.</param>
        public ContentViewAttribute(Type contentType)
        {
            ContentType = contentType;
        }
    }
}

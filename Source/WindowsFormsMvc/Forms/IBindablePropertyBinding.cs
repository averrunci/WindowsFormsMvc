// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Forms;

/// <summary>
/// Provides the function to bind a bindable property.
/// </summary>
public interface IBindablePropertyBinding
{
    /// <summary>
    /// Binds an observable property.
    /// </summary>
    void Bind();

    /// <summary>
    /// Unbinds a bound property.
    /// </summary>
    void Unbind();
}
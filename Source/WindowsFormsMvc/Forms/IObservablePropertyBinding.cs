// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Forms
{
    /// <summary>
    /// Provides the function to bind an observable property.
    /// </summary>
    public interface IObservablePropertyBinding
    {
        /// <summary>
        /// Binds an observable property.
        /// </summary>
        void Bind();

        /// <summary>
        /// Binds an observable property to update the other when
        /// either its value or the target value is changed.
        /// </summary>
        void BindTwoWay();

        /// <summary>
        /// Binds an observable property to update the observable property
        /// when the target value is changed.
        /// </summary>
        void BindToSource();

        /// <summary>
        /// Unbinds a bound property.
        /// </summary>
        void Unbind();
    }
}

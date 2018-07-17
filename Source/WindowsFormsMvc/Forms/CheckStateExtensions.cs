// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Windows.Forms;

namespace Charites.Windows.Forms
{
    internal static class CheckStateExtensions
    {
        public static bool ToBoolean(this CheckState @this)
            => @this == CheckState.Checked;

        public static bool? ToNullableOfBoolean(this CheckState @this)
            => @this == CheckState.Indeterminate ? new bool?() : @this.ToBoolean();

        public static CheckState ToCheckState(this bool @this)
            => @this ? CheckState.Checked : CheckState.Unchecked;

        public static CheckState ToCheckState(this bool? @this)
            => @this?.ToCheckState() ?? CheckState.Indeterminate;
    }
}

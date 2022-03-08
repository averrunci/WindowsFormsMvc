// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Specification("ListItemToIndexConverter Spec")]
class ListItemToIndexConverter
{
    [Context]
    ListItemToIndexConverter_Convert Convert => default!;

    [Context]
    ListItemToIndexConverter_ConvertBack ConvertBack => default!;
}
// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Specification(
    "ListViewItemContext Spec",
    typeof(ListViewItemContextSpec_BindChecked),
    typeof(ListViewItemContextSpec_BindText),
    typeof(ListViewItemContextSpec_BindBackColor),
    typeof(ListViewItemContextSpec_BindForeColor),
    typeof(ListViewItemContextSpec_BindFont)
)]
class ListViewItemContextSpec;
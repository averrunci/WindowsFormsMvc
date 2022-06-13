// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Specification(
    "CheckedListBoxItemsSource Spec",
    typeof(CheckedListBoxItemsSourceSpec_ValueNotSpecified),
    typeof(CheckedListBoxItemsSourceSpec_ValueMemberSpecified),
    typeof(CheckedListBoxItemsSourceSpec_BooleanCheckedMemberSpecified),
    typeof(CheckedListBoxItemsSourceSpec_NullableBooleanCheckedMemberSpecified),
    typeof(CheckedListBoxItemsSourceSpec_CheckStateMemberSpecified),
    typeof(CheckedListBoxItemsSourceSpec_ValueAndBooleanCheckedSelectorSpecified),
    typeof(CheckedListBoxItemsSourceSpec_ValueAndNullableBooleanCheckedSelectorSpecified),
    typeof(CheckedListBoxItemsSourceSpec_ValueAndCheckStateSelectorSpecified)
)]
class CheckedListBoxItemsSourceSpec
{
}
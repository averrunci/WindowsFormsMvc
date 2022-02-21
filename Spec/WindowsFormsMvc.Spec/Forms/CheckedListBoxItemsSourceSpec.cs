// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Specification("CheckedListBoxItemsSource Spec")]
class CheckedListBoxItemsSourceSpec
{
    [Context]
    CheckedListBoxItemsSourceSpec_ValueNotSpecified ValueNotSpecified => default!;

    [Context]
    CheckedListBoxItemsSourceSpec_ValueMemberSpecified ValueMemberSpecified => default!;

    [Context]
    CheckedListBoxItemsSourceSpec_BooleanCheckedMemberSpecified BooleanCheckedMemberSpecified => default!;

    [Context]
    CheckedListBoxItemsSourceSpec_NullableBooleanCheckedMemberSpecified NullableBooleanCheckedMemberSpecified => default!;

    [Context]
    CheckedListBoxItemsSourceSpec_CheckStateMemberSpecified CheckStateMemberSpecified => default!;

    [Context]
    CheckedListBoxItemsSourceSpec_ValueAndBooleanCheckedSelectorSpecified ValueAndBooleanCheckedSelectorSpecified => default!;

    [Context]
    CheckedListBoxItemsSourceSpec_ValueAndNullableBooleanCheckedSelectorSpecified ValueAndNullableBooleanCheckedSelectorSpecified => default!;

    [Context]
    CheckedListBoxItemsSourceSpec_ValueAndCheckStateSelectorSpecified ValueAndCheckStateSelectorSpecified => default!;
}
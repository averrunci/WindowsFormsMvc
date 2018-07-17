// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms
{
    [Specification("CheckedListBoxItemsSource Spec")]
    class CheckedListBoxItemsSourceSpec
    {
        [Context]
        CheckedListBoxItemsSourceSpec_ValueNotSpecified ValueNotSpecified { get; }

        [Context]
        CheckedListBoxItemsSourceSpec_ValueMemberSpecified ValueMemberSpecified { get; }

        [Context]
        CheckedListBoxItemsSourceSpec_BooleanCheckedMemberSpecified BooleanCheckedMemberSpecified { get; }
        
        [Context]
        CheckedListBoxItemsSourceSpec_NullableBooleanCheckedMemberSpecified NullableBooleanCheckedMemberSpecified { get; }

        [Context]
        CheckedListBoxItemsSourceSpec_CheckStateMemberSpecified CheckStateMemberSpecified { get; }

        [Context]
        CheckedListBoxItemsSourceSpec_ValueAndBooleanCheckedSelectorSpecified ValueAndBooleanCheckedSelectorSpecified { get; }

        [Context]
        CheckedListBoxItemsSourceSpec_ValueAndNullableBooleanCheckedSelectorSpecified ValueAndNullableBooleanCheckedSelectorSpecified { get; }

        [Context]
        CheckedListBoxItemsSourceSpec_ValueAndCheckStateSelectorSpecified ValueAndCheckStateSelectorSpecified { get; }
    }
}

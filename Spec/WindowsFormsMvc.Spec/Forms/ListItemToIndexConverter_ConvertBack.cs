// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Context("Converts an index to an item")]
class ListItemToIndexConverter_ConvertBack : FixtureSteppable
{
    ListItemToIndexConverter<string> Converter { get; }

    string[] Items { get; } = { "Item1", "Item2", "Item3", "Item4", "Item5" };

    public ListItemToIndexConverter_ConvertBack()
    {
        Converter = new ListItemToIndexConverter<string>(Items);
    }

    [Example("When the index that is in the range of the list is specified")]
    void Ex01()
    {
        Expect("converted value should be the item at the specified item", () =>
            Items.Select((item, index) => Converter.ConvertBack(index) == item).All(result => result)
        );
    }

    [Example("When the index that is lower than 0 is specified")]
    void Ex02()
    {
        Expect("converted value should be default", () => Converter.ConvertBack(-1) == default);
    }

    [Example("When the index that is equal to the list count is specified")]
    void Ex03()
    {
        Expect("converted value should be default", () => Converter.ConvertBack(Items.Length) == default);
    }

    [Example("When the index that is greater than the list count is specified")]
    void Ex04()
    {
        Expect("converted value should be default", () => Converter.ConvertBack(Items.Length + 1) == default);
    }
}
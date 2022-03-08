// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Forms;

[Context("Converts an item to an index")]
class ListItemToIndexConverter_Convert : FixtureSteppable
{
    ListItemToIndexConverter<string> Converter { get; }

    string[] Items { get; } = { "Item1", "Item2", "Item3", "Item4", "Item5" };

    public ListItemToIndexConverter_Convert()
    {
        Converter = new ListItemToIndexConverter<string>(Items);
    }

    [Example("When the item in the list is specified")]
    void Ex01()
    {
        Expect("converted value should be the index for the specified item", () =>
            Items.Select((item, index) => Converter.Convert(item) == index).All(result => result)
        );
    }

    [Example("When the item that does not include in the list is specified")]
    void Ex02()
    {
        Expect("converted value should be -1", () => Converter.Convert("Item99") == -1);
    }

    [Example("When the item that is default value is specified")]
    void Ex03()
    {
        Expect("converted value should be -1", () => Converter.Convert(default) == -1);
    }
}
// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Forms;

/// <summary>
/// Provides the function to convert an item in the list to its index.
/// </summary>
/// <typeparam name="T">The type of items in the list.</typeparam>
public class ListItemToIndexConverter<T>
{
    private readonly IEnumerable<T> items;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListItemToIndexConverter{T}"/> class
    /// with the specified list.
    /// </summary>
    /// <param name="items">The items to convert an item in the list to its index.</param>
    public ListItemToIndexConverter(IEnumerable<T> items)
    {
        this.items = items;
    }

    /// <summary>
    /// Converts the specified item to its index.
    /// </summary>
    /// <param name="item">The item to convert to its index.</param>
    /// <returns>
    /// The index of <paramref name="item"/> if found in the list; otherwise <c>-1</c>.
    /// </returns>
    public int Convert(T? item) => item is null ? -1 : new List<T>(items).IndexOf(item);

    /// <summary>
    /// Converts the specified index to the item at it.
    /// </summary>
    /// <param name="index">The index to convert to the item at it.</param>
    /// <returns>
    /// The item at the specified index if it is valid range in the list; otherwise <c>default</c>.
    /// </returns>
    public T? ConvertBack(int index) => index < 0 || index >= items.Count() ? default : items.ElementAt(index);
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingRefrigerator;

internal class shelf
{
    static private int _uniqueIdentifierShelf = 0;
    public shelf( int shelfFloor, double place, List<Item> items=null)
    {
        Id = ++_uniqueIdentifierShelf;
        ShelfFloor = shelfFloor;
        Place = place;
        Items = items;
    }

    public int Id { get; }
    public int ShelfFloor { get; set; }
    public double Place { get; set; }
    public List<Item> Items { get; set; }
}

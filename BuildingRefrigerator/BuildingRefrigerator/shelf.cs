using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingRefrigerator;

public class shelf
{
    static private int _uniqueIdentifierShelf = 0;
    public shelf( int shelfFloor, double place=20, List<Item> items=null)
    {
        Id = ++_uniqueIdentifierShelf;
        ShelfFloor = shelfFloor;
        Place = place;
        Items = items;
    }

    public int Id { get; }
    public int ShelfFloor { get; set; }
    public double Place { get; }
    public List<Item> Items { get; set; }

    public static bool isThisShelfExsist(int idShelf)
    {
        return idShelf > 0 && idShelf <= _uniqueIdentifierShelf;
    }

}

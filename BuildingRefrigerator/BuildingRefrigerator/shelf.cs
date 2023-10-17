using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BuildingRefrigerator;

public class shelf : IComparable<shelf>
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

    public override string ToString()
    { 
        return this.ToStringProperty(); 
    }
    public  double HowMuchSpaceIsLeftOnTheShelf()
    {
        List<Item> items = this.Items;
        double leftSpace = this.Place;
        foreach (Item item in items)
        {
            leftSpace -= item.Place;
        }
        if (leftSpace < 0)
            throw new Exception("There are products on this shelf that have no place");
        return leftSpace;
    }

    public static bool isThisShelfExsist(int idShelf)
    {
        return( idShelf > 0||idShelf==-1) && idShelf <= _uniqueIdentifierShelf;
    }

    public int CompareTo(shelf? other)
    {
        return this.HowMuchSpaceIsLeftOnTheShelf().CompareTo(other?.HowMuchSpaceIsLeftOnTheShelf());
    }
}

namespace BuildingRefrigerator;

public class shelf : IComparable<shelf>
{
    static private int _uniqueIdentifierShelf = 0;
    private List<Item?>? _items;
    public shelf(int shelfFloor, double place = 20, List<Item> items = null)
    {
        Id = ++_uniqueIdentifierShelf;
        ShelfFloor = shelfFloor;
        Place = place;
        Items = items;
    }
    public int Id { get; }
    public int ShelfFloor { get; set; }
    public double Place { get; }
    public List<Item?>? Items
    {
        get => _items; set
        {
            if (value == null)
                _items = new List<Item?>();
            else
                _items = value;
        }
    }
    public override string ToString()
    {
        return "\nshelf:" + this.ToStringProperty();
    }
    public double HowMuchSpaceIsLeftOnTheShelf()
    {
        List<Item?>? items = this.Items;
        double leftSpace = this.Place;
        if (items == null)
            return leftSpace;
        leftSpace -= items.Where(item => item?.Place != null).Sum(item => item.Place);
        if (leftSpace < 0)
            throw new Exception("There are products on this shelf that have no place");
        return leftSpace;
    }
    public int CompareTo(shelf? other)
    {
        return other.HowMuchSpaceIsLeftOnTheShelf().CompareTo(this?.HowMuchSpaceIsLeftOnTheShelf());
    }
}
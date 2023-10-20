namespace BuildingRefrigerator;

public class shelf : IComparable<shelf>
{
    private List<Item> _items;
    public shelf(int shelfFloor, double area = 20, List<Item> items = null)
    {
        ID = Guid.NewGuid();
        ShelfFloor = shelfFloor;
        Area = area;
        Items = items;
    }
    public Guid ID { get;private set; }
    public int ShelfFloor { get; set; }
    public double Area { get; }
    public List<Item> Items
    {
        get => _items; set
        {
            if (value == null)
                _items = new List<Item>();
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
        List<Item> items = this.Items;
        double leftSpace = this.Area;
        if (items == null)
            return leftSpace;
        leftSpace -= items.Where(item => item.Area != null).Sum(item => item.Area);
        if (leftSpace < 0)
            throw new Exception("There are products on this shelf that have no space");
        return leftSpace;
    }
    public Item RemoveItem(Guid id)
    {
        Item returnItem;
        foreach (Item it in this.Items)
            if (it.ID == id)
            {
                returnItem = it;
                this.Items.Remove(it);
                return returnItem;
            }
        return null;
    }
    public void DeletingExpiredItems()
    {
        this.Items.RemoveAll(item => item.ExpiryDate < DateTime.Now);
    }
    public int CompareTo(shelf other)
    {
        return other.HowMuchSpaceIsLeftOnTheShelf().CompareTo(this.HowMuchSpaceIsLeftOnTheShelf());
    }
}
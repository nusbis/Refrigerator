namespace BuildingRefrigerator;

public class Item : IComparable<Item?>
{
    static private int _uniqueIdentifierItem = 0;
    private TypeOfFood _type;
    private Cosher _cisher;
    private DateTime _expiryDate;
    public Item(string name, TypeOfFood type, Cosher cosher, DateTime expiryDate, double place, int idShelf=-1)
    {
        Id = ++_uniqueIdentifierItem;
        Name = name;
        Type = type;
        Cosher = cosher;
        ExpiryDate = expiryDate;
        Place = place;
        IdShelf = idShelf;
    }
    public int Id { get; }
    public string Name { get; set; }
    public int IdShelf { get; set; }
    public TypeOfFood Type
    {
        get => _type;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            _type = value;
        }
    }
    public Cosher Cosher
    {
        get => _cisher;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            _cisher = value;
        }
    }
    public DateTime ExpiryDate
    {
        get => _expiryDate;
        private set
        {
            if (value < DateTime.Now)
                throw new ArgumentException("Date is in the past");
            _expiryDate = value;
        }
    }
    public double Place { get; }
    public int CompareTo(Item? other)
    {
        return this.ExpiryDate.CompareTo(other?.ExpiryDate);
    }
    public override string ToString()
    {
        return "\nItem: " + this.ToStringProperty();
    }
}

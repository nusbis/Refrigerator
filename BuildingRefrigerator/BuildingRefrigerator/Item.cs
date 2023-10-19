namespace BuildingRefrigerator;

public class Item : IComparable<Item?>
{
    private TypeOfItem _type;
    private Kashrut _cisher;
    private DateTime _expiryDate;
    public Item(string name, TypeOfItem type, Kashrut cosher, DateTime expiryDate, double area, Guid idShelf= default)
    {
        ID = Guid.NewGuid();
        Name = name;
        Type = type;
        Cosher = cosher;
        ExpiryDate = expiryDate;
        Area = area;
        IdShelf = idShelf;
    }
    public Guid ID { get; private set; }
    public string Name { get; set; }
    public Guid IdShelf { get; set; }
    public TypeOfItem Type
    {
        get => _type;
        private set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            _type = value;
        }
    }
    public Kashrut Cosher
    {
        get => _cisher;
       private set
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
    public double Area { get; }
    public int CompareTo(Item? other)
    {
        return this.ExpiryDate.CompareTo(other?.ExpiryDate);
    }
    public override string ToString()
    {
        return "\nItem: " + this.ToStringProperty();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingRefrigerator;

public class Item: IComparable<Item?>
{
    static private int _uniqueIdentifierItem = 0;
    private int _idShelf;
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
    public int IdShelf
    {
        get => _idShelf;
        set
        {
            if (shelf.isThisShelfExsist(value))
                _idShelf = value;
            else
                throw new Exception("this shelf isnt exsist!");
        }
    }
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
       
        //sort by id, using CompareTo of the int type
        return this.ExpiryDate.CompareTo(other?.ExpiryDate);

    }

    public override string ToString()
    { 
        return this.ToStringProperty(); 
    }
 
}

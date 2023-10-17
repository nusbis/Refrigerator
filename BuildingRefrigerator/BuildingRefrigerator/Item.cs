using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingRefrigerator;

public class Item: IComparable<Item>
{
    static private int _uniqueIdentifierItem = 0;
    public Item(string name, Type type, Cosher cosher, DateTime expiryDate, double place, int idShelf=-1)
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
        get => IdShelf;
        set
        {
            if (shelf.isThisShelfExsist(value))
                IdShelf = value;
            else
                throw new Exception("this shelf isnt exsist!");
        }
    }
    public Type Type
    {
        get => Type;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            Type = value;
        }
    }
    public Cosher Cosher
    {
        get => Cosher;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            Cosher = value;
        }
    }
    public DateTime ExpiryDate
    {
        get => ExpiryDate;
        private set
        {
            if (value < DateTime.Now)
                throw new ArgumentException("Date is in the past");
            ExpiryDate = value;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BuildingRefrigerator;

internal class Item
{
    static private int _uniqueIdentifierItem = 0;
    public Item(string name,Type type,Cosher cosher,DateTime expiryDate,double place,int idShelf)
    {
        Id = ++_uniqueIdentifierItem;
        Name = name ;
        Type = type ;
        Cosher = cosher ;
        ExpiryDate = expiryDate ;
        Place = place ;
        IdShelf = idShelf ;
    }

    public int Id { get; }
    public string Name { get; set; }
    public Type Type { get; set; }
    public Cosher Cosher { get; set; }
    public DateTime ExpiryDate { get; set; }
    public double Place { get; set; }
    public  int IdShelf { get; set ; }

}

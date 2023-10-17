using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingRefrigerator;

public class refrigerator
{
    static private int _uniqueIdentifierRefrigerator = 0;
    public refrigerator(string model, string color, int quantityOfShelves = 5, List<shelf> shelves = null)
    {
        Id = ++_uniqueIdentifierRefrigerator;
        Model = model;
        Color = color;
        Shelves = shelves;
        QuantityOfShelves = quantityOfShelves;
    }

    public int Id { get; }
    public string Model
    {
        get => Model;
        set
        {
            if (value == null)
                throw new ArgumentException("A refrigerator model is missing");
            Model = value;
        }
    }
    public string Color { get; set; }
    public int QuantityOfShelves { get; }
    public List<shelf> Shelves { get; set; }

}

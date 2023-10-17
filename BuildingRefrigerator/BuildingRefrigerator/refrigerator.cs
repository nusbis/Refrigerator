using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingRefrigerator;

internal class refrigerator
{
    static private int _uniqueIdentifierRefrigerator = 0;
    public refrigerator( string model, string color, List<shelf> shelves=null)
    {
        Id = ++_uniqueIdentifierRefrigerator;
        Model = model;
        Color = color;
        Shelves = shelves;
    }

    public int Id { get; }
    public string Model { get; set; }
    public string Color { get; set; }
    public List<shelf> Shelves { get; set; }

}

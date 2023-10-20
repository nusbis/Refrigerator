using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuildingRefrigerator;

public static class Tools
{
    public static string ToStringProperty<T>(this T t)
    {
        string str = "";

        //Going over all the properties in their type check the measure and it is a collection will run the function for each object in the collection
        foreach (PropertyInfo item in t!.GetType().GetProperties())
        {
            var val = item.GetValue(t, null);
            if (!(val is string) && val is IEnumerable list)
            {
                foreach (var listItem in list)
                {
                    str += listItem+"\n";
                }
            }
            else
                str += "\n" + item.Name +
                           ": " + item.GetValue(t, null);
        }
        return str;
    }
}

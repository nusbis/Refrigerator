
using BuildingRefrigerator;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Schema;

class Program
{
    static void Main()
    {
        refrigerator? myRefrigerator = buildingRefrigerator();
        Options choice;
        int idItem;

        printMenu();
        if (!Options.TryParse(Console.ReadLine(), out choice)) throw new Exception("invalid choice");
        try
        {
            while ((int)choice != 100)
            {

                switch (choice)
                {
                    case Options.PrintAll:
                        Console.WriteLine("print all of the items in the refrigerator:/n" );
                        Console.WriteLine(myRefrigerator);
                        break;
                    case Options.FreeSpaceInTheFridge:
                        Console.WriteLine("Prints the free space in the refrigerator:/n", myRefrigerator?.HowMuchSpaceIsLeftOnTheRefrigerator());
                        break;
                    case Options.InsertItem:
                        Console.WriteLine("You put an item in the fridge:");
                        myRefrigerator?.InsertingAnItem(buildingItem());
                        break;
                    case Options.TakingItemOut:
                        Console.WriteLine("Insert an item ID to remove it from the water:");
                        if (!int.TryParse(Console.ReadLine(), out idItem)) throw new Exception("idItem is invalid");
                        myRefrigerator?.TakingAnItemOut(idItem);
                        break;
                    case Options.CleaningRefrigerator:
                        Console.WriteLine("we are cleaning the refrigerator ");
                        myRefrigerator?.CleaningRefrigerator();
                        break;
                    case Options.WhatDoYouWantToEat:
                        TypeOfFood type;
                        Cosher cosher;
                        Console.WriteLine(@"enter name,
type:
   1- Food
   2 Drinking, 
cosher:
   1-Dairy
   2-Meat
   3-Fur");
                        if (!TypeOfFood.TryParse(Console.ReadLine(), out type)) throw new Exception("type is invalid");
                        if (!Cosher.TryParse(Console.ReadLine(), out cosher)) throw new Exception("cosher is invalid");
                        myRefrigerator?.WhatDoYouWantToEat(cosher, type);
                        break;
                    case Options.PrintingProductsAccordingToExpirationDate:
                        myRefrigerator?.SortedByExpirationDate();
                        break;
                    case Options.PrintingShelvesAccordingToAvailableSpace:
                        myRefrigerator?.SortByAvailableShelfSpace();
                        break;
                    case Options.PrintingRefrigeratorsAccordingToAvailableSpace:

                        break;
                    case Options.PreparingRefrigeratorForShopping:
                        myRefrigerator?.Shopping();
                        break;
                    default:
                        break;
                }

                printMenu();
                if (!Options.TryParse(Console.ReadLine(), out choice)) throw new Exception("invalid choice");
            }
        }
        catch(Exception ex) { Console.WriteLine(ex); }
    }


    private static refrigerator? buildingRefrigerator()
    {
        string? model, color;
        int maximumShelves, quantityShelvesBuildNow;
        Console.WriteLine(@"Welcome!
We would like to start building the refrigerator
Please follow the instructions.
enter model,
color, 
maximum of shelves");
        model = Console.ReadLine();
        color = Console.ReadLine();
        int.TryParse(Console.ReadLine(), out maximumShelves);
        refrigerator refrigerator = new refrigerator(model, color, maximumShelves);

        Console.WriteLine("How many of the shelves you stated you want to build now?");
        if (!int.TryParse(Console.ReadLine(), out quantityShelvesBuildNow)) throw new Exception("quantityShelvesBuildNow is in valid");
        for (int i = quantityShelvesBuildNow; i > 1; i--)
            refrigerator?.Shelves?.Add(buildingShelf(refrigerator.Shelves.Count));
        return refrigerator;

    }
    public static shelf? buildingShelf(int floorShelf)
    {
        int sizeOfShelf;
        Console.WriteLine("enter size of the shelf:");
        int.TryParse(Console.ReadLine(), out sizeOfShelf);
        return new shelf(floorShelf, sizeOfShelf);
    }
    public static Item? buildingItem()
    {
        string? name;
        TypeOfFood type;
        Cosher cosher;
        DateTime expiryDate;
        double sizeOfItem;
        Console.WriteLine(@"enter name,
type:
   1- Food
   2 Drinking, 
cosher:
   1-Dairy
   2-Meat
   3-Fur,
expiryDate,
size of this Item");
        name = Console.ReadLine();
        if (!TypeOfFood.TryParse(Console.ReadLine(), out type)) throw new Exception("type is invalid");
        if (!Cosher.TryParse(Console.ReadLine(), out cosher)) throw new Exception("cosher is invalid");
        if (!DateTime.TryParse(Console.ReadLine(), out expiryDate)) throw new Exception("expiry Date is invalid");
        if (!double.TryParse(Console.ReadLine(), out sizeOfItem)) throw new Exception("sizeOfItem is invalid");
        return new Item(name, type, cosher, expiryDate, sizeOfItem);
    }
    private static void printMenu()
    {
        Console.WriteLine(@"please choose one of the following:
1: print all
2: free space in the fridge
3: insert item
4: taking item out
5: cleaning refrigerator
6: What do you want to eat?
7: printing products according to expiration date
8: printing shelves according to available space
9: printing refrigerators according to available space
10: preparing refrigerator for shopping
100:exit");
    }
}

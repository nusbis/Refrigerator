using BuildingRefrigerator;
class Program
{
    static void Main()
    {
        Options choice;
        Guid idItem;
        try
        {
            Refrigerator myRefrigerator = BuildingRefrigerator();
            printMenu();
            if (!Options.TryParse(Console.ReadLine(), out choice)) throw new Exception("invalid choice");
            while ((int)choice != 100)
            {
                switch (choice)
                {
                    case Options.PrintAll:
                        Console.WriteLine("print all of the items in the refrigerator:\n{0}", myRefrigerator);
                        break;
                    case Options.FreeSpaceInTheFridge:
                        Console.WriteLine("Prints the free space in the refrigerator:{0}\n", myRefrigerator.HowMuchSpaceIsLeftOnTheRefrigerator());
                        break;
                    case Options.InsertItem:
                        Console.WriteLine("You put an item in the fridge:");
                        myRefrigerator.InsertItem(buildingItem());
                        break;
                    case Options.TakingItemOut:
                        Console.WriteLine("Insert an item ID to remove it from the water:");
                        if (!Guid.TryParse(Console.ReadLine(), out idItem)) throw new Exception("idItem is invalid");
                        myRefrigerator.TakingAnItemOut(idItem);
                        break;
                    case Options.CleaningRefrigerator:
                        Console.WriteLine("we are cleaning the refrigerator ");
                        myRefrigerator.CleaningRefrigerator();
                        break;
                    case Options.WhatDoYouWantToEat:
                        whatDoYouWantToEat(myRefrigerator);
                        break;
                    case Options.PrintingProductsAccordingToExpirationDate:
                        printItems(myRefrigerator.SortedByExpirationDate());
                        break;
                    case Options.PrintingShelvesAccordingToAvailableSpace:
                        printShelves(myRefrigerator.SortByAvailableShelfSpace());
                        break;
                    case Options.PrintingRefrigeratorsAccordingToAvailableSpace:
                        sortRefrigerators();
                        break;
                    case Options.PreparingRefrigeratorForShopping:
                        Console.WriteLine("We are preparing refrigerator for shopping");
                        myRefrigerator.Shopping();
                        break;
                    default:
                        break;
                }

                printMenu();
                if (!Options.TryParse(Console.ReadLine(), out choice)) throw new Exception("invalid choice");
            }
        }
        catch (Exception ex) { Console.WriteLine(@"Dear Customer: 
{0}", ex.Message); }
    }
    private static Item buildingItem()
    {
        string name;
        TypeOfItem type;
        Kashrut cosher;
        DateTime expiryDate;
        double sizeOfItem;
        Console.WriteLine(@"enter name,
type:
     Food
     Drinking 
cosher:
     Dairy
     Meat
     Pareve
expiryDate, in format: d/m/y
size of this Item");
        try
        {
            name = Console.ReadLine();
            if (!(TypeOfItem.TryParse(Console.ReadLine(), out type))) throw new Exception("type is invalid");
            if (!Kashrut.TryParse(Console.ReadLine(), out cosher)) throw new Exception("cosher is invalid");
            if (!DateTime.TryParse(Console.ReadLine(), out expiryDate)) throw new Exception("expiry Date is invalid");
            if (expiryDate < DateTime.Now) throw new ArgumentException("Date is in the past");
            if (!double.TryParse(Console.ReadLine(), out sizeOfItem)) throw new Exception("sizeOfItem is invalid");
            if(sizeOfItem<=0) throw new Exception("sizeOfItem is negative");
            return new Item(name, type, cosher, expiryDate, sizeOfItem);
        }
        catch (Exception ex) { throw ex; }
    }
    private static void whatDoYouWantToEat(Refrigerator myRefrigerator)
    {
        TypeOfItem type;
        Kashrut cosher;
        Console.WriteLine(@"enter type:
     Food
     Drinking, 
cosher:
     Dairy
     Meat
     Pareve");
        try
        {
            if (!TypeOfItem.TryParse(Console.ReadLine(), out type)) throw new Exception("type is invalid");
            if (!Kashrut.TryParse(Console.ReadLine(), out cosher)) throw new Exception("cosher is invalid");
            List<Item> myItems = myRefrigerator.WhatDoYouWantToEat(cosher, type);
            if (myItems == null)
                Console.WriteLine("there are no food like you want");
            else
                printItems(myItems);
        }
        catch (Exception ex) { throw ex; }
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
    private static Refrigerator BuildingRefrigerator()
    {
        string? model, color;
        int maximumShelves = 5;
        Console.WriteLine(@"Welcome!
We would like to start building the refrigerator
Please follow the instructions.
enter model,
color, 
maximum of shelves");
        try
        {
            model = Console.ReadLine();
            color = Console.ReadLine();
            if (!int.TryParse(Console.ReadLine(), out maximumShelves)) throw new Exception("maximumShelves is invalid");
            return new Refrigerator(model, color, maximumShelves);
        }
        catch (Exception ex) { throw ex; }
    }
    private static void printItems(List<Item> items)
    {
        foreach (Item it in items)
            Console.WriteLine(it);
    }
    private static void printShelves(List<Shelf> shelves)
    {
        foreach (Shelf shelf in shelves)
            Console.WriteLine(shelf);
    }
    private static void printRefrigerator(List<Refrigerator> refrigerators)
    {
        foreach (Refrigerator rf in refrigerators)
            Console.WriteLine(rf);
    }
    private static void sortRefrigerators()
    {
        int countOfRefrigerators;
        List<Refrigerator> refrigerators = new List<Refrigerator>();
        Console.WriteLine("How many refrigerators would you like to build?");
        if (!int.TryParse(Console.ReadLine(), out countOfRefrigerators)) throw new Exception("countOfRefrigerators is invalid");
        for (int i = 0; i < countOfRefrigerators; i++)
            refrigerators.Add(BuildingRefrigerator());
        refrigerators.Sort();
        printRefrigerator(refrigerators);
    }
}
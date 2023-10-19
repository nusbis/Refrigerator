namespace BuildingRefrigerator;

public class refrigerator : IComparable<refrigerator?>
{
    static private int _uniqueIdentifierRefrigerator = 0;
    private List<shelf?>? _shelves;
    private string? _model;
    private int _maximumShelves;
    public refrigerator(string? model, string color, int maximumShelves, List<shelf?>? shelves = null)
    {
        Id = ++_uniqueIdentifierRefrigerator;
        Model = model;
        Color = color;
        _maximumShelves = maximumShelves;
        Shelves = shelves;
    }
    public int Id { get; }
    public string? Model
    {
        get=> _model;
        set
        {
            if (value==null)
                throw new Exception("A refrigerator model is missing. Cannot create a fridge");
            _model = value;
        }
    }
    public string Color { get; set; }
    public List<shelf?>? Shelves
    {
        get => _shelves;
        set
        {
            if (value is not null)
            {
                _shelves = value;
            }
            else
            {
                _shelves = new List<shelf?>();
                for (int i = 1; i <= _maximumShelves; i++)
                    _shelves.Add(new shelf(i));
            }
        }
    }
    public override string ToString()
    {
        return "refrigerator:  "+ this.ToStringProperty();
    }
    public double HowMuchSpaceIsLeftOnTheRefrigerator()
    {
        return this.Shelves.Sum(shelf => shelf?.HowMuchSpaceIsLeftOnTheShelf() ?? throw new Exception("Error. No shelf found"));
    }
    public void InsertingAnItem(Item myItem)
    {
        shelf? myShelf = this?.Shelves?.FirstOrDefault(shelf => shelf?.HowMuchSpaceIsLeftOnTheShelf() >= myItem.Place);
        if (myShelf == null)
            throw new Exception("there are no place in the refrigerator");
        else
        {
            myItem.IdShelf = myShelf.Id;
            myShelf?.Items?.Add(myItem);
        }
    }
    public Item? TakingAnItemOut(int idItemIssued)
    {
        Item? returnItem;
        foreach (shelf? shelf in this.Shelves)
            foreach (Item? item in shelf.Items)
                if (item?.Id == idItemIssued)
                {
                    returnItem = item;
                    shelf.Items.Remove(item);
                    return returnItem;
                }
            throw new Exception("There is no item in the refrigerator with the identifier:" + idItemIssued);
    }
    public void CleaningRefrigerator()
    {
        this.Shelves.ForEach(shelf =>
        {
            shelf?.Items.RemoveAll(item => item?.ExpiryDate < DateTime.Now);
        });

    }
    public List<Item?> WhatDoYouWantToEat(Cosher cosher, TypeOfFood type)
    {
       return allItems().Where(item => item?.Cosher == cosher && item.Type == type).ToList();     
    }
    public List<Item?> SortedByExpirationDate()
    {
        List<Item?> items = allItems();
        items.Sort();
        return items;
    }
    public List<shelf?> SortByAvailableShelfSpace()
    {
        List<shelf?> shelfList = this.Shelves;
        shelfList.Sort();
        return shelfList;
    }
    public int CompareTo(refrigerator? other)
    {
        return other.HowMuchSpaceIsLeftOnTheRefrigerator().CompareTo(this?.HowMuchSpaceIsLeftOnTheRefrigerator());
    }
    public void Shopping()
    {
        if (this.HowMuchSpaceIsLeftOnTheRefrigerator() < 20)
        {
            CleaningRefrigerator();
            if (this.HowMuchSpaceIsLeftOnTheRefrigerator() < 20)
            {
                List<Item?> productsSortedByDate = this.SortedByExpirationDate();

                List<Item?> items = productsSortedByDate.Where(item => (item?.ExpiryDate < DateTime.Now.AddDays(3) && item.Cosher == Cosher.Dairy)
                 || (item?.ExpiryDate < DateTime.Now.AddDays(7) && item.Cosher == Cosher.Meat)
                 || (item?.ExpiryDate < DateTime.Now.AddDays(1) && item.Cosher == Cosher.Fur)).ToList();

                productsSortedByDate.RemoveAll(item => (item?.ExpiryDate < DateTime.Now.AddDays(3) && item.Cosher == Cosher.Dairy)
            || (item?.ExpiryDate < DateTime.Now.AddDays(7) && item.Cosher == Cosher.Meat)
            || (item?.ExpiryDate < DateTime.Now.AddDays(1) && item.Cosher == Cosher.Fur));

                if (this.HowMuchSpaceIsLeftOnTheRefrigerator() < 20)
                {
                    productsSortedByDate = (List<Item?>)productsSortedByDate.Concat(items);

                    throw new Exception("Sorry, This is not the time to shop");
                }
                else
                    throw new Exception("Sorry, due to the need to shop we had to throw away some products.");

            }
        }

    }
    private List<Item?> allItems()
    {
        return this?.Shelves?.SelectMany(shelf => shelf.Items).ToList();
    }
}

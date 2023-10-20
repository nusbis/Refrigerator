namespace BuildingRefrigerator;

public class refrigerator : IComparable<refrigerator>
{
    private List<shelf> _shelves;
    private string _model;
    private int _maximumShelves;
    public refrigerator(string model, string color, int maximumShelves, List<shelf> shelves = null)
    {
        ID = Guid.NewGuid();
        Model = model;
        Color = color;
        _maximumShelves = maximumShelves;
        if (shelves == null)
        {
            Shelves = new List<shelf>();
            this.buildAllTheShelves(maximumShelves);
        }
        else
            Shelves = shelves;
    }
    public Guid ID { get; private set; }
    public string Model
    {
        get => _model;
        private set
        {
            if (value == null)
                throw new Exception("A refrigerator model is missing. Cannot create a fridge");
            _model = value;
        }
    }
    public string Color { get; set; }
    public List<shelf> Shelves { get; private set; }
    public override string ToString()
    {
        return "refrigerator:  " + this.ToStringProperty();
    }
    public double HowMuchSpaceIsLeftOnTheRefrigerator()
    {
        return this.Shelves.Sum(shelf => shelf?.HowMuchSpaceIsLeftOnTheShelf() ?? 0);
    }
    public void InsertItem(Item myItem)
    {
        shelf myShelf = this.Shelves.FirstOrDefault(shelf => shelf.HowMuchSpaceIsLeftOnTheShelf() >= myItem.Area);
        if (myShelf == null)
            throw new Exception("there are no space in the refrigerator");
        else
            myShelf.AddItem(myItem);
    }
    public Item TakingAnItemOut(Guid idItemIssued)
    {
        Item returnItem;
        foreach (shelf shelf in this.Shelves)
        {
            returnItem = shelf.RemoveItem(idItemIssued);
            if (returnItem != null)
                return returnItem;
        }
        throw new Exception("There is no item in the refrigerator with the identifier:" + idItemIssued);
    }
    public void CleaningRefrigerator()
    {
        this.Shelves.ForEach(shelf =>
        {
            shelf.DeletingExpiredItems();
        });

    }
    public List<Item> WhatDoYouWantToEat(Kashrut cosher, TypeOfItem type)
    {
        return allItems().Where(item => item.Cosher == cosher && item.Type == type).ToList();
    }
    public List<Item> SortedByExpirationDate()
    {
        List<Item> items = allItems();
        items.Sort();
        return items;
    }
    public List<shelf> SortByAvailableShelfSpace()
    {
        List<shelf> shelfList = this.Shelves.ToList();
        shelfList.Sort();
        return shelfList;
    }
    public int CompareTo(refrigerator other)
    {
        return other.HowMuchSpaceIsLeftOnTheRefrigerator().CompareTo(this.HowMuchSpaceIsLeftOnTheRefrigerator());
    }
    public void Shopping()
    {
        if (this.HowMuchSpaceIsLeftOnTheRefrigerator() < 20)
        {
            CleaningRefrigerator();
            if (this.HowMuchSpaceIsLeftOnTheRefrigerator() < 20)
            {
                List<Item> productsSortedByDate = this.SortedByExpirationDate();

                List<Item> items = productsSortedByDate.Where(item => (item.ExpiryDate < DateTime.Now.AddDays(3) && item.Cosher == Kashrut.Dairy)
                 || (item.ExpiryDate < DateTime.Now.AddDays(7) && item.Cosher == Kashrut.Meat)
                 || (item.ExpiryDate < DateTime.Now.AddDays(1) && item.Cosher == Kashrut.Pareve)).ToList();

                productsSortedByDate.RemoveAll(item => (item.ExpiryDate < DateTime.Now.AddDays(3) && item.Cosher == Kashrut.Dairy)
            || (item.ExpiryDate < DateTime.Now.AddDays(7) && item.Cosher == Kashrut.Meat)
            || (item.ExpiryDate < DateTime.Now.AddDays(1) && item.Cosher == Kashrut.Pareve));

                if (this.HowMuchSpaceIsLeftOnTheRefrigerator() < 20)
                {
                    productsSortedByDate = (List<Item>)productsSortedByDate.Concat(items);

                    throw new Exception("Sorry, This is not the time to shop");
                }
                else
                    throw new Exception("Sorry, due to the need to shop we had to throw away some products.");

            }
        }

    }
    private List<Item> allItems()
    {
        return this.Shelves.SelectMany(shelf => shelf.Items).ToList();
    }
    private void buildAllTheShelves(int maximumShelves)
    {
        for (int i = 1; i <= _maximumShelves; i++)
            this.Shelves.Add(new shelf(i));
    }
}

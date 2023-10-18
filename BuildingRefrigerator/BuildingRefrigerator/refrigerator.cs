using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingRefrigerator;

public class refrigerator : IComparable<refrigerator?>
{
    static private int _uniqueIdentifierRefrigerator = 0;
    public refrigerator(string model, string color, int maximumShelves = 5, List<shelf> shelves = null)
    {
        Id = ++_uniqueIdentifierRefrigerator;
        Model = model;
        Color = color;
        Shelves = shelves;
        MaximumShelves = maximumShelves;
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

    public int MaximumShelves { get; }
    public List<shelf?> Shelves
    {
        get => Shelves;
        set
        {
            if (value == null)
                Shelves = new List<shelf?> { new shelf(1) };
            else
                Shelves = value;
        }
    }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
    public double HowMuchSpaceIsLeftOnTheRefrigerator()
    {
        List<shelf?> shelves = this.Shelves;
        // double leftSpaceInRefrigerator = 0;
        //foreach (shelf shelf in shelves)
        //{
        //    leftSpaceInRefrigerator += shelf.HowMuchSpaceIsLeftOnTheShelf();
        //}
        return shelves.Sum(shelf => shelf?.HowMuchSpaceIsLeftOnTheShelf()??throw new Exception(""));

        // return leftSpaceInRefrigerator;
    }
    public void InsertingAnItem(Item myItem)
    {
        //List<shelf> shelves = this.Shelves;
        //foreach (shelf shelf in shelves)
        //{
        //    if (shelf.HowMuchSpaceIsLeftOnTheShelf() > myItem.Place)
        //    {
        //        myItem.IdShelf = shelf.Id;
        //        return;
        //    }
        //}

        shelf? myShelf = this.Shelves.FirstOrDefault(shelf => shelf.HowMuchSpaceIsLeftOnTheShelf() >= myItem.Place);


        if (myShelf == null)
        {
            if (Shelves.Count == this.MaximumShelves)
                throw new Exception("there are no place in the refrigerator");
            shelf? newShelf = new shelf(Shelves.Count + 1);
            this.Shelves.Add(newShelf);
            myItem.IdShelf = newShelf.Id;
        }
        else
            myItem.IdShelf = myShelf.Id;
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
        //foreach (shelf shelf in this.Shelves)
        //    foreach(Item item in shelf.Items)
        //    {
        //        if(item.ExpiryDate<DateTime.Now)
        //            shelf.Items.Remove(item);
        //    }
        this.Shelves.ForEach(shelf =>
        {
            shelf?.Items.RemoveAll(item => item?.ExpiryDate < DateTime.Now);
        });

    }

    public List<Item?> WhatDoYouWantToEat(Cosher cosher, Type type)
    {
        //List<Item> items = new List<Item>();
        //foreach (shelf shelf in this.Shelves)
        //    foreach(Item item in shelf.Items)
        //    {
        //        if(item.Cosher==cosher&& item.Type==type)
        //            items.Add(item);
        //    }
        //return items;
        List<Item?> returnItems = new List<Item?>();
        List<Item?> items = allItems();
        foreach (Item? item in items)
            if (item?.Cosher == cosher && item.Type == type)
                returnItems.Add(item);
        return returnItems;
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

    private List<Item?> allItems()
    {
        List<Item?> items = new List<Item?>();
        foreach (shelf? shelf in this.Shelves)
            foreach (Item? item in shelf.Items)
                items.Add(item);
        return items;
    }

    public int CompareTo(refrigerator? other)
    {
        throw new NotImplementedException();
    }

    public void Shopping()
    {
        if (this.HowMuchSpaceIsLeftOnTheRefrigerator() < 20)
        {
            CleaningRefrigerator();
            if (this.HowMuchSpaceIsLeftOnTheRefrigerator() < 20)
            {
                List<Item?> productsSortedByDate = this.SortedByExpirationDate();

               List<Item?> items= productsSortedByDate.Where(item => (item?.ExpiryDate < DateTime.Now.AddDays(3) && item.Cosher == Cosher.Dairy)
                || (item?.ExpiryDate < DateTime.Now.AddDays(7) && item.Cosher == Cosher.Meat)
                || (item?.ExpiryDate < DateTime.Now.AddDays(1) && item.Cosher == Cosher.Fur)).ToList();

                    productsSortedByDate.RemoveAll(item => (item?.ExpiryDate < DateTime.Now.AddDays(3) && item.Cosher == Cosher.Dairy)
                || (item?.ExpiryDate < DateTime.Now.AddDays(7) && item.Cosher == Cosher.Meat)
                || (item?.ExpiryDate < DateTime.Now.AddDays(1) && item.Cosher == Cosher.Fur));

                if (this.HowMuchSpaceIsLeftOnTheRefrigerator() < 20)
                {
                    productsSortedByDate = (List<Item?>)productsSortedByDate.Concat(items);
                    
                    Console.WriteLine("Sorry, This is not the time to shop");
                }
                else
                    Console.WriteLine("Sorry, due to the need to shop we had to throw away some products.");

            }
        }

    }
}

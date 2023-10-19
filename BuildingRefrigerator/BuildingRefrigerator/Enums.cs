namespace BuildingRefrigerator;
public enum TypeOfItem { Food=1, Drinking }
public enum Kashrut { Dairy=1, Meat, Pareve }
public enum Options
{
    PrintAll = 1,
    FreeSpaceInTheFridge,
    InsertItem,
    TakingItemOut,
    CleaningRefrigerator,
    WhatDoYouWantToEat,
    PrintingProductsAccordingToExpirationDate,
    PrintingShelvesAccordingToAvailableSpace,
    PrintingRefrigeratorsAccordingToAvailableSpace,
    PreparingRefrigeratorForShopping=10,
    Exit=100
}

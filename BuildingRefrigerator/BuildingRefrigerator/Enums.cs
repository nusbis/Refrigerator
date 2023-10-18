using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingRefrigerator;

public enum TypeOfFood { Food=1, Drinking }

public enum Cosher { Dairy=1, Meat, Fur }

enum Options
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


internal class Enums
{
}

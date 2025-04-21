//Final Project
//Date: 21/04/2025      Name: Gustavo Moreno 
//inventoryManager.cs


using System;

namespace InventoryTracker
{
    public enum ProductCategory
    {
        Electronics = 1,
        Food,
        Construction,
        Cleaning
    }

    public abstract class InventoryItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ItemID { get; set; }
        public ProductCategory Category { get; set; }
        public abstract string GetItemType();
    }

    public class FoodItem : InventoryItem
    {
        public override string GetItemType() => "Food";
    }

    public class ElectronicItem : InventoryItem
    {
        public override string GetItemType() => "Electronics";
    }

    public class ConstructionItem : InventoryItem
    {
        public override string GetItemType() => "Construction";
    }

    public class CleaningItem : InventoryItem
    {
        public override string GetItemType() => "Cleaning";
    }
}

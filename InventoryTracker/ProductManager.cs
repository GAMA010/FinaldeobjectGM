//Final Project
//Date: 21/04/2025      Name: Gustavo Moreno 
//productManager.cs


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryTracker
{
    public static class ProductManager
    {
        public static List<InventoryItem> Products { get; private set; } = new();

        private static readonly Random rand = new();

        public static async Task LoadFromJsonAsync(string filePath)
        {
            if (!File.Exists(filePath)) return;

            var json = await File.ReadAllTextAsync(filePath);
            var rawProducts = JsonSerializer.Deserialize<List<RawProduct>>(json);

            Products = rawProducts.Select(p =>
            {
                InventoryItem item = p.Category switch
                {

                    ProductCategory.Food => new FoodItem(),
                    ProductCategory.Electronics => new ElectronicItem(),
                    ProductCategory.Construction => new ConstructionItem(),
                    ProductCategory.Cleaning => new CleaningItem(),
                    _ => throw new Exception("Invalid category")
                    
                };

                item.Name = p.Name;
                item.Price = p.Price;
                item.ItemID = p.ItemID;
                item.Category = p.Category;
                item.Description = p.Description;
                item.CreatedDate = p.CreatedDate;
                return item;

            }).ToList();
        }

        public static async Task SaveToJsonAsync(string filePath = "products.json")
        {
            var raw = Products.Select(p => new RawProduct
            {

                Name = p.Name,
                Price = p.Price,
                ItemID = p.ItemID,
                Category = p.Category,
                Description = p.Description,
                CreatedDate = p.CreatedDate

            }).ToList();

            var json = JsonSerializer.Serialize(raw, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }

        public static async Task<InventoryItem> CreateProductAsync(ProductCategory category, string name, decimal price)
        {
            if (Products.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {

                throw new Exception($"A product with the name '{name}' already exists.");

            }

            InventoryItem item = category switch
            {

                ProductCategory.Food => new FoodItem(),
                ProductCategory.Electronics => new ElectronicItem(),
                ProductCategory.Construction => new ConstructionItem(),
                ProductCategory.Cleaning => new CleaningItem(),
                _ => throw new ArgumentException("Invalid category")

            };

            item.Name = name;
            item.Price = price;
            item.Category = category;
            item.ItemID = rand.Next(1000000, 9999999);
            item.CreatedDate = DateTime.Now;

            Products.Add(item);
            await SaveToJsonAsync();
            return item;
        }

        public static InventoryItem FindProductByName(string name)
        {

            return Products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        }

        public static List<InventoryItem> FilterByCategory(ProductCategory category)
        {

            return Products.Where(p => p.Category == category).ToList();

        }

        public static bool DeleteProductByName(string name)
        {
            var product = Products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (product != null)
            {

                Products.Remove(product);
                SaveToJsonAsync().Wait();
                return true;

            }
            return false;
        }

        private class RawProduct
        {

            public string Name { get; set; }
            public decimal Price { get; set; }
            public int ItemID { get; set; }
            public ProductCategory Category { get; set; }
            public string Description { get; set; }
            public DateTime CreatedDate { get; set; }

        }
    }
}


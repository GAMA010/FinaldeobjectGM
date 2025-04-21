//Final Project
//Date: 21/04/2025      Name: Gustavo Moreno 
//program.cs


using System;
using System.Threading.Tasks;

namespace InventoryTracker
{
    class Program
    {
        static async Task Main()
        {
            await ProductManager.LoadFromJsonAsync("products.json");

            while (true)
            {
                Console.WriteLine("\nWelcome to GamaCorp System.");
                Console.WriteLine("Available actions:");
                Console.WriteLine("1: Add Product");
                Console.WriteLine("2: Find Product");
                Console.WriteLine("3: Exit");
                Console.WriteLine("4: Filter Products by Category");
                Console.WriteLine("5: Delete Product");

                Console.Write("Enter option: ");
                var option = Console.ReadLine();

                if (option == "1")
                {
                    Console.WriteLine("Select Category: 1: Electronics 2: Food 3: Construction 4: Cleaning");
                    if (!int.TryParse(Console.ReadLine(), out int catVal) || !Enum.IsDefined(typeof(ProductCategory), catVal))
                    {
                        Console.WriteLine("Invalid category.");
                        continue;
                    }

                    Console.Write("Enter product name: ");
                    string name = Console.ReadLine();

                    decimal price;
                    while (true)
                    {
                        Console.Write("Enter product price: ");
                        if (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
                        {
                            Console.WriteLine("Invalid price. Try again.");
                        }
                        else break;
                    }

                    await ProductManager.CreateProductAsync((ProductCategory)catVal, name, price);
                }
                else if (option == "2")
                {
                    Console.Write("Enter product name to find: ");
                    var searchName = Console.ReadLine();
                    var product = ProductManager.FindProductByName(searchName);
                    if (product != null)
                        Console.WriteLine($"{product.Name} ({product.GetItemType()}), Price: {product.Price}, ID: {product.ItemID}");
                    else
                        Console.WriteLine("Product not found.");
                }
                else if (option == "4")
                {
                    Console.WriteLine("Filter by category:");
                    Console.WriteLine("1. Electronics\n2. Food\n3. Construction\n4. Cleaning");
                    if (int.TryParse(Console.ReadLine(), out int selected) &&
                        Enum.IsDefined(typeof(ProductCategory), selected))
                    {
                        var results = ProductManager.FilterByCategory((ProductCategory)selected);
                        if (results.Count == 0)
                        {
                            Console.WriteLine("No products in that category.");
                        }
                        else
                        {
                            Console.WriteLine("Filtered products:");
                            foreach (var item in results)
                            {
                                Console.WriteLine($"- {item.Name} | Price: {item.Price} | ID: {item.ItemID}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid category.");
                    }
                }
                else if (option == "5")
                {
                    Console.Write("Enter product name to delete: ");
                    string nameToDelete = Console.ReadLine()?.Trim();
                    if (ProductManager.DeleteProductByName(nameToDelete))
                        Console.WriteLine("Product deleted successfully.");
                    else
                        Console.WriteLine("Product not found.");
                }
                else if (option == "3")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }
        }
    }
}

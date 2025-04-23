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
                Console.WriteLine("6: Count Products by Category");
                Console.WriteLine("7: Filter Products Above a Minimum Price");

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

                    Console.Write("Enter product description: ");
                    string description = Console.ReadLine();

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

                    try
                    {

                        var item = await ProductManager.CreateProductAsync((ProductCategory)catVal, name, price);
                        item.Description = description;
                        Console.WriteLine("Product added successfully.");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadKey();

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Error: {ex.Message}");

                    }
                }
                else if (option == "2")
                {
                    Console.Write("Enter product name to find: ");
                    var searchName = Console.ReadLine();
                    var product = ProductManager.FindProductByName(searchName);
                    if (product != null)
                    {

                        Console.WriteLine($"{product.Name} ({product.GetItemType()}), Price: {product.Price}, ID: {product.ItemID}");
                        Console.WriteLine($"Description: {product.Description}");
                        Console.WriteLine($"Added on: {product.CreatedDate}");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                    }
                    else
                        Console.WriteLine("Product not found.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
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
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();

                        }
                        else
                        {
                            Console.WriteLine("Filtered products:");
                            foreach (var item in results)
                            {

                                Console.WriteLine($"- {item.Name} | Price: {item.Price} | ID: {item.ItemID}");
                                Console.WriteLine($"  Description: {item.Description}");
                                Console.WriteLine($"Added on: {item.CreatedDate}");

                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();

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
                    {

                        Console.WriteLine("Product deleted successfully.");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadKey();

                    }
                    else
                        Console.WriteLine("Product not found.");

                }
                else if (option == "6")
                {

                    Console.WriteLine("Number of Products by category:");

                    foreach (ProductCategory category in Enum.GetValues(typeof(ProductCategory)))
                    {

                        int count = ProductManager.Products.Count(p => p.Category == category);
                        Console.WriteLine($"- {category}: {count}");

                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();

                }
                else if (option == "7")
                {

                    Console.Write("Enter minimum price: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal minPrice) && minPrice > 0)
                    {

                        var filtered = ProductManager.Products
                            .Where(p => p.Price >= minPrice)
                            .ToList();

                        if (filtered.Count == 0)
                        {

                            Console.WriteLine("No products found above that price.");

                        }
                        else
                        {

                            Console.WriteLine("Products found:");
                            foreach (var item in filtered)
                            {

                                Console.WriteLine($"- {item.Name} | ${item.Price} | {item.GetItemType()} | ID: {item.ItemID}");
                                Console.WriteLine($"  Description: {item.Description}");
                                Console.WriteLine($"  Added on: {item.CreatedDate}");

                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid price.");
                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (option == "3")
                {

                    Console.WriteLine("Goodbye!");
                    break;

                }
                else
                {

                    Console.WriteLine("Invalid option.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadKey();

                }
            }
        }
    }
}

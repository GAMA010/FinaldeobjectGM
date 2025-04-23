//Final Project
//Date: 21/04/2025      Name: Gustavo Moreno 
//ProdcutManagingTests.cs

using NUnit.Framework;
using System.Threading.Tasks;
using InventoryTracker;

namespace InventoryTracker.Tests
{

    [TestFixture]
    public class ProductManagerTests
    {
        [SetUp]
        public void Setup()
        {

            ProductManager.Products.Clear();

        }

        [Test]
        public async Task CreateProduct_AddsProductCorrectly()
        {

            var product = await ProductManager.CreateProductAsync(ProductCategory.Food, "TestApple", 1.50m);

            Assert.That(product, Is.Not.Null);
            Assert.That(product.Name, Is.EqualTo("TestApple"));
            Assert.That(product.Price, Is.EqualTo(1.50m));
            Assert.That(product.Category, Is.EqualTo(ProductCategory.Food));

        }

        [Test]
        public async Task FindProduct_ReturnsCorrectItem()
        {

            await ProductManager.CreateProductAsync(ProductCategory.Electronics, "TestTV", 299.99m);
            var found = ProductManager.FindProductByName("TestTV");

            Assert.That(found, Is.Not.Null);
            Assert.That(found.Name, Is.EqualTo("TestTV"));

        }

        [Test]
        public void CreateProduct_ThrowsOnDuplicateName()
        {
            Assert.ThrowsAsync<System.Exception>(async () =>
            {

                await ProductManager.CreateProductAsync(ProductCategory.Cleaning, "TestSoap", 3.00m);
                await ProductManager.CreateProductAsync(ProductCategory.Cleaning, "TestSoap", 3.00m);

            });
        }

        [Test]
        public async Task CreateProduct_SavesDescription()
        {   

            var product = await ProductManager.CreateProductAsync(ProductCategory.Construction, "TestHammer", 10.00m);
            product.Description = "Strong and durable.";

            Assert.That(product.Description, Is.EqualTo("Strong and durable."));

        }

        [Test]
        public async Task CreateProduct_StoresDescriptionCorrectly()
        {

            var product = await ProductManager.CreateProductAsync(ProductCategory.Cleaning, "TestBleach", 5.00m);
            product.Description = "Used for disinfecting surfaces.";

            Assert.That(product.Description, Is.EqualTo("Used for disinfecting surfaces."));
            
        }

    }
}
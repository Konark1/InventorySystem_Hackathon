using InventorySystem.Core;
using InventorySystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Tests
{
    public class InventoryServiceTests
    {
        private InventoryDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            return new InventoryDbContext(options);
        }

        // TEST 1: The "Stock Logic" Requirement
        // Does 10 + 5 actually equal 15?
        [Fact]
        public async Task UpdateStock_ShouldIncreaseQuantity_WhenStockIn()
        {
            // 1. ARRANGE (Setup)
            var context = CreateInMemoryContext();
            var service = new InventoryService(context);
            var item = new InventoryItem { Name = "Nike Air", Quantity = 10 };
            await service.AddItemAsync(item);

            // 2. ACT (Do the action)
            await service.UpdateStockAsync(item.Id, 5);

            // 3. ASSERT (Check result)
            var allItems = await service.GetAllItemsAsync();
            var updatedItem = allItems.First();

            Assert.Equal(15, updatedItem.Quantity); // Expect 15
        }

        // TEST 2: The "Alert Logic" Requirement
        // Does the system flag low stock?
        [Fact]
        public void IsLowStock_ShouldReturnTrue_WhenQuantityIsLow()
        {
            // 1. ARRANGE
            var item = new InventoryItem
            {
                Name = "Low Stock Item",
                Quantity = 2,
                LowStockThreshold = 5
            };

            // 2. ACT
            bool result = item.IsLowStock();

            // 3. ASSERT
            Assert.True(result);
        }
    }
}
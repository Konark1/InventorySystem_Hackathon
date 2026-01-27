using InventorySystem.Core;
using InventorySystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using System.Linq;
using System.Security.Claims;
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

        private IHttpContextAccessor CreateMockHttpContextAccessor(string userId = "test-user-id")
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }));

            var httpContext = new DefaultHttpContext { User = user };
            var mockAccessor = new Mock<IHttpContextAccessor>();
            mockAccessor.Setup(x => x.HttpContext).Returns(httpContext);
            return mockAccessor.Object;
        }

        // TEST 1: The "Stock Logic" Requirement
        // Does 10 + 5 actually equal 15?
        [Fact]
        public async Task UpdateStock_ShouldIncreaseQuantity_WhenStockIn()
        {
            // 1. ARRANGE (Setup)
            var context = CreateInMemoryContext();
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var service = new InventoryService(context, httpContextAccessor);
            var item = new InventoryItem { Name = "Nike Air", Quantity = 10, UserId = "test-user-id" };
            await service.AddItemAsync(item);

            // 2. ACT (Do the action)
            await service.UpdateStockAsync(item.Id, 5);

            // 3. ASSERT (Check result)
            var allItems = await service.GetAllItemsAsync(null);
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
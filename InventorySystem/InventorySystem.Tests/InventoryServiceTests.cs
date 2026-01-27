using InventorySystem.Core;
using InventorySystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace InventorySystem.Tests
{
    public class InventoryServiceTests
    {
        private InventoryDbContext CreateInMemoryContext()
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<InventoryDbContext>(options =>
                    options.UseInMemoryDatabase(System.Guid.NewGuid().ToString()))
                .AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<InventoryDbContext>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            var context = serviceProvider.GetRequiredService<InventoryDbContext>();
            context.Database.EnsureCreated();
            return context;
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

        // TEST 3: Add Item Test
        [Fact]
        public async Task AddItemAsync_ShouldAddItemWithCorrectUserId()
        {
            // ARRANGE
            var context = CreateInMemoryContext();
            var httpContextAccessor = CreateMockHttpContextAccessor("user-123");
            var service = new InventoryService(context, httpContextAccessor);

            var newItem = new InventoryItem
            {
                Name = "Test Product",
                Quantity = 50,
                LowStockThreshold = 10
            };

            // ACT
            var result = await service.AddItemAsync(newItem);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal("Test Product", result.Name);
            Assert.Equal("user-123", result.UserId);
            Assert.True(result.Id > 0); // Should have an ID assigned
        }

        // TEST 4: Update Item Test
        [Fact]
        public async Task UpdateItemAsync_ShouldUpdateExistingItem()
        {
            // ARRANGE
            var context = CreateInMemoryContext();
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var service = new InventoryService(context, httpContextAccessor);

            var item = new InventoryItem { Name = "Original Name", Quantity = 100, UserId = "test-user-id" };
            await service.AddItemAsync(item);

            var updatedItem = new InventoryItem
            {
                Name = "Updated Name",
                Quantity = 150,
                LowStockThreshold = 20
            };

            // ACT
            var result = await service.UpdateItemAsync(item.Id, updatedItem);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Name);
            Assert.Equal(150, result.Quantity);
            Assert.Equal(20, result.LowStockThreshold);
        }

        // TEST 5: Update Item - User Cannot Update Another User's Item
        [Fact]
        public async Task UpdateItemAsync_ShouldReturnNull_WhenUserDoesNotOwnItem()
        {
            // ARRANGE
            var context = CreateInMemoryContext();
            var httpContextAccessor1 = CreateMockHttpContextAccessor("user-1");
            var service1 = new InventoryService(context, httpContextAccessor1);

            // User 1 creates an item
            var item = new InventoryItem { Name = "User 1 Item", Quantity = 50 };
            await service1.AddItemAsync(item);

            // User 2 tries to update it
            var httpContextAccessor2 = CreateMockHttpContextAccessor("user-2");
            var service2 = new InventoryService(context, httpContextAccessor2);

            var updatedItem = new InventoryItem { Name = "Hacked Name", Quantity = 999 };

            // ACT
            var result = await service2.UpdateItemAsync(item.Id, updatedItem);

            // ASSERT
            Assert.Null(result); // Should not be able to update
        }

        // TEST 6: Delete Item Test
        [Fact]
        public async Task DeleteItemAsync_ShouldRemoveItem()
        {
            // ARRANGE
            var context = CreateInMemoryContext();
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var service = new InventoryService(context, httpContextAccessor);

            var item = new InventoryItem { Name = "Delete Me", Quantity = 10 };
            await service.AddItemAsync(item);

            // ACT
            await service.DeleteItemAsync(item.Id);

            // ASSERT
            var items = await service.GetAllItemsAsync();
            Assert.Empty(items);
        }

        // TEST 7: Delete Item - User Cannot Delete Another User's Item
        [Fact]
        public async Task DeleteItemAsync_ShouldNotDelete_WhenUserDoesNotOwnItem()
        {
            // ARRANGE
            var context = CreateInMemoryContext();
            var httpContextAccessor1 = CreateMockHttpContextAccessor("user-1");
            var service1 = new InventoryService(context, httpContextAccessor1);

            var item = new InventoryItem { Name = "Protected Item", Quantity = 50 };
            await service1.AddItemAsync(item);

            // User 2 tries to delete it
            var httpContextAccessor2 = CreateMockHttpContextAccessor("user-2");
            var service2 = new InventoryService(context, httpContextAccessor2);

            // ACT
            await service2.DeleteItemAsync(item.Id);

            // ASSERT - Item should still exist for user 1
            var items = await service1.GetAllItemsAsync();
            Assert.Single(items);
        }

        // TEST 8: Get Item By ID Test
        [Fact]
        public async Task GetItemByIdAsync_ShouldReturnCorrectItem()
        {
            // ARRANGE
            var context = CreateInMemoryContext();
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var service = new InventoryService(context, httpContextAccessor);

            var item = new InventoryItem { Name = "Find Me", Quantity = 25 };
            await service.AddItemAsync(item);

            // ACT
            var result = await service.GetItemByIdAsync(item.Id);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal("Find Me", result.Name);
            Assert.Equal(25, result.Quantity);
        }

        // TEST 9: Get Item By ID - User Cannot Access Another User's Item
        [Fact]
        public async Task GetItemByIdAsync_ShouldReturnNull_WhenUserDoesNotOwnItem()
        {
            // ARRANGE
            var context = CreateInMemoryContext();
            var httpContextAccessor1 = CreateMockHttpContextAccessor("user-1");
            var service1 = new InventoryService(context, httpContextAccessor1);

            var item = new InventoryItem { Name = "Private Item", Quantity = 50 };
            await service1.AddItemAsync(item);

            // User 2 tries to access it
            var httpContextAccessor2 = CreateMockHttpContextAccessor("user-2");
            var service2 = new InventoryService(context, httpContextAccessor2);

            // ACT
            var result = await service2.GetItemByIdAsync(item.Id);

            // ASSERT
            Assert.Null(result);
        }

        // TEST 10: Stock Decrease (Sale) Test
        [Fact]
        public async Task UpdateStockAsync_ShouldDecreaseQuantity_WhenStockOut()
        {
            // ARRANGE
            var context = CreateInMemoryContext();
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var service = new InventoryService(context, httpContextAccessor);

            var item = new InventoryItem { Name = "Shoes", Quantity = 100 };
            await service.AddItemAsync(item);

            // ACT - Sell 30 units
            await service.UpdateStockAsync(item.Id, -30);

            // ASSERT
            var updatedItem = await service.GetItemByIdAsync(item.Id);
            Assert.Equal(70, updatedItem.Quantity);
        }

        // TEST 11: Stock Cannot Go Negative
        [Fact]
        public async Task UpdateStockAsync_ShouldNotAllowNegativeStock()
        {
            // ARRANGE
            var context = CreateInMemoryContext();
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var service = new InventoryService(context, httpContextAccessor);

            var item = new InventoryItem { Name = "Limited Item", Quantity = 10 };
            await service.AddItemAsync(item);

            // ACT - Try to remove more than available
            await service.UpdateStockAsync(item.Id, -50);

            // ASSERT
            var updatedItem = await service.GetItemByIdAsync(item.Id);
            Assert.Equal(0, updatedItem.Quantity); // Should be capped at 0
        }

        // TEST 12: Transaction History Test
        [Fact]
        public async Task GetItemHistoryAsync_ShouldReturnTransactions()
        {
            // ARRANGE
            var context = CreateInMemoryContext();
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var service = new InventoryService(context, httpContextAccessor);

            var item = new InventoryItem { Name = "Tracked Item", Quantity = 50 };
            await service.AddItemAsync(item);

            // ACT - Make multiple transactions
            await service.UpdateStockAsync(item.Id, 20);  // Restock
            await service.UpdateStockAsync(item.Id, -10); // Sale

            // ASSERT
            var history = await service.GetItemHistoryAsync(item.Id);
            var transactions = history.ToList();

            Assert.Equal(2, transactions.Count);
            Assert.Equal("Restock", transactions[1].TransactionType); // Oldest first after OrderByDescending
            Assert.Equal(20, transactions[1].QuantityChanged);
            Assert.Equal("Sale", transactions[0].TransactionType);
            Assert.Equal(-10, transactions[0].QuantityChanged);
        }

        // TEST 13: Search Functionality Test
        [Fact]
        public async Task GetAllItemsAsync_ShouldFilterBySearch()
        {
            // ARRANGE
            var context = CreateInMemoryContext();
            var httpContextAccessor = CreateMockHttpContextAccessor();
            var service = new InventoryService(context, httpContextAccessor);

            await service.AddItemAsync(new InventoryItem { Name = "Nike Shoes", Quantity = 10 });
            await service.AddItemAsync(new InventoryItem { Name = "Adidas Shoes", Quantity = 20 });
            await service.AddItemAsync(new InventoryItem { Name = "Nike Shirt", Quantity = 15 });

            // ACT
            var results = await service.GetAllItemsAsync("Nike");

            // ASSERT
            Assert.Equal(2, results.Count());
            Assert.All(results, item => Assert.Contains("Nike", item.Name));
        }

        // TEST 14: User Isolation Test - Users Only See Their Own Items
        [Fact]
        public async Task GetAllItemsAsync_ShouldOnlyReturnCurrentUsersItems()
        {
            // ARRANGE
            var context = CreateInMemoryContext();

            // User 1 adds items
            var httpContextAccessor1 = CreateMockHttpContextAccessor("user-1");
            var service1 = new InventoryService(context, httpContextAccessor1);
            await service1.AddItemAsync(new InventoryItem { Name = "User 1 Item A", Quantity = 10 });
            await service1.AddItemAsync(new InventoryItem { Name = "User 1 Item B", Quantity = 20 });

            // User 2 adds items
            var httpContextAccessor2 = CreateMockHttpContextAccessor("user-2");
            var service2 = new InventoryService(context, httpContextAccessor2);
            await service2.AddItemAsync(new InventoryItem { Name = "User 2 Item C", Quantity = 30 });

            // ACT
            var user1Items = await service1.GetAllItemsAsync();
            var user2Items = await service2.GetAllItemsAsync();

            // ASSERT
            Assert.Equal(2, user1Items.Count());
            Assert.Equal(1, user2Items.Count());
            Assert.All(user1Items, item => Assert.Equal("user-1", item.UserId));
            Assert.All(user2Items, item => Assert.Equal("user-2", item.UserId));
        }

        // TEST 15: IsLowStock False Test
        [Fact]
        public void IsLowStock_ShouldReturnFalse_WhenQuantityIsHigh()
        {
            // ARRANGE
            var item = new InventoryItem
            {
                Name = "Well Stocked Item",
                Quantity = 100,
                LowStockThreshold = 10
            };

            // ACT
            bool result = item.IsLowStock();

            // ASSERT
            Assert.False(result);
        }
    }
}
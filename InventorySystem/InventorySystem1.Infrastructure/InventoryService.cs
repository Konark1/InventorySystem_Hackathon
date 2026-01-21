using InventorySystem.Application;
using InventorySystem.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventorySystem.Infrastructure
{
    public class InventoryService : IInventoryService
    {
        private readonly InventoryDbContext _context;

        // Constructor Injection: We ask for the Database Access
        public InventoryService(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryItem>> GetAllItemsAsync()
        {
            // "ToListAsync" executes the SQL query
            return await _context.Items.ToListAsync();
        }

        public async Task<InventoryItem> AddItemAsync(InventoryItem item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync(); // Hits the database
            return item;
        }

        public async Task UpdateStockAsync(int itemId, int quantityChange)
        {
            var item = await _context.Items.FindAsync(itemId);
            if (item == null) return;

            item.Quantity += quantityChange;
            if (item.Quantity < 0) item.Quantity = 0;

            await _context.SaveChangesAsync(); // Saves the changes permanently
        }
    }
}
using InventorySystem.Application;
using InventorySystem.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Infrastructure
{
    public class InventoryService : IInventoryService
    {
        private readonly InventoryDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InventoryService(InventoryDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // HELPER: Get the current user's ID from the "Key Card" (Token)
        private string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) 
            {
                // Fallback for safety (shouldn't happen if [Authorize] is on)
                throw new Exception("User is not logged in!");
            }
            return userId;
        }

        public async Task<IEnumerable<InventoryItem>> GetAllItemsAsync(string search = null)
        {
            var userId = GetCurrentUserId(); // 🕵️‍♂️ Who is this?

            var query = _context.Items
                                .Where(x => x.UserId == userId) // 👈 THE MAGIC WALL
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<InventoryItem?> GetItemByIdAsync(int itemId)
        {
            var userId = GetCurrentUserId();
            return await _context.Items
                .Where(x => x.Id == itemId && x.UserId == userId) // 👈 Ownership check
                .FirstOrDefaultAsync();
        }

        public async Task<InventoryItem> AddItemAsync(InventoryItem item)
        {
            item.UserId = GetCurrentUserId(); // 🏷️ Stamp it!
            
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<InventoryItem?> UpdateItemAsync(int itemId, InventoryItem updatedItem)
        {
            var userId = GetCurrentUserId();
            var item = await _context.Items
                .Where(x => x.Id == itemId && x.UserId == userId) // 👈 Ownership check
                .FirstOrDefaultAsync();
            
            if (item == null) return null;

            // Update the properties
            item.Name = updatedItem.Name;
            item.Quantity = updatedItem.Quantity;
            item.LowStockThreshold = updatedItem.LowStockThreshold;

            await _context.SaveChangesAsync();
            return item;
        }

        public async Task UpdateStockAsync(int itemId, int quantityChange)
        {
            var userId = GetCurrentUserId();
            
            // 1. Find the item (with ownership check)
            var item = await _context.Items
                .Where(x => x.Id == itemId && x.UserId == userId) // 👈 Ownership check
                .FirstOrDefaultAsync();
            
            if (item == null) return; // Safety check

            // 2. Update the Item's Quantity
            item.Quantity += quantityChange;
            
            // Prevent negative stock
            if (item.Quantity < 0) item.Quantity = 0;

            // 3. Record the Transaction (The Audit Log)
            var transaction = new InventoryTransaction
            {
                InventoryItemId = itemId,
                QuantityChanged = quantityChange,
                TransactionDate = DateTime.UtcNow,
                TransactionType = quantityChange > 0 ? "Restock" : "Sale"
            };

            // 4. Add the transaction to the database queue
            _context.Transactions.Add(transaction);

            // 5. Save BOTH changes (Item update + Transaction record) in one go
            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteItemAsync(int id)
        {
            var userId = GetCurrentUserId();
            
            // 1. Find the item in the DB (with ownership check)
            var item = await _context.Items
                .Where(x => x.Id == id && x.UserId == userId) // 👈 Ownership check
                .FirstOrDefaultAsync();
            
            // 2. If it exists, remove it
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<InventoryTransaction>> GetItemHistoryAsync(int itemId)
        {
            var userId = GetCurrentUserId();
            
            // Only show history for items owned by current user
            return await _context.Transactions
                .Where(t => t.InventoryItemId == itemId && 
                           _context.Items.Any(i => i.Id == itemId && i.UserId == userId)) // 👈 Ownership check
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
    }
}
using InventorySystem.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventorySystem.Application
{
    public interface IInventoryService
    {
        // 1. Get the list of all items (with optional search)
        Task<IEnumerable<InventoryItem>> GetAllItemsAsync(string search = null);

        // 2. Get a single item by ID
        Task<InventoryItem?> GetItemByIdAsync(int itemId);

        // 3. Add a new item
        Task<InventoryItem> AddItemAsync(InventoryItem item);

        // 4. Update/Edit an item
        Task<InventoryItem?> UpdateItemAsync(int itemId, InventoryItem updatedItem);

        // 5. Update Stock (+ or -)
        // This is the most critical function for your project.
        Task UpdateStockAsync(int itemId, int quantityChange);

        // 6. Delete an item
        Task DeleteItemAsync(int id);

        // 7. Get transaction history for an item
        Task<IEnumerable<InventoryTransaction>> GetItemHistoryAsync(int itemId);
    }
}
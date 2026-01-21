using InventorySystem.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventorySystem.Application
{
    public interface IInventoryService
    {
        // 1. Get the list of all items
        Task<IEnumerable<InventoryItem>> GetAllItemsAsync();

        // 2. Add a new item
        Task<InventoryItem> AddItemAsync(InventoryItem item);

        // 3. Update Stock (+ or -)
        // This is the most critical function for your project.
        Task UpdateStockAsync(int itemId, int quantityChange);
    }
}
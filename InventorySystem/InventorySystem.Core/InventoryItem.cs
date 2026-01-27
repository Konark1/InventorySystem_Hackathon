using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Core
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }
        
        // NEW: Money matters!
        public decimal Price { get; set; }

        // NEW: The Owner Tag
        // We use string because Identity User Ids are Guids (strings)
        public string? UserId { get; set; }

        public bool IsLowStock()
        {
            return Quantity <= LowStockThreshold;
        }
    }
}
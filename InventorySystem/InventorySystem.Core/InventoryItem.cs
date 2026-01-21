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

        
        public bool IsLowStock()
        {
            return Quantity <= LowStockThreshold;
        }
    }
}
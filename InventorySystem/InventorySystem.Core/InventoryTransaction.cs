using System;

namespace InventorySystem.Core
{
    public class InventoryTransaction
    {
        public int Id { get; set; }
        
        // Who: Which item was changed?
        public int InventoryItemId { get; set; }
        public InventoryItem? InventoryItem { get; set; } // Navigation property

        // What: How many? (+5 or -2)
        public int QuantityChanged { get; set; }
        
        // When: Exact time
        public DateTime TransactionDate { get; set; }

        // Why: "Sale", "Restock", "Adjustment"
        public string TransactionType { get; set; } = string.Empty;
    }
}

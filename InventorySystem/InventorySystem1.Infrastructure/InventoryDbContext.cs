using Microsoft.EntityFrameworkCore;
using InventorySystem.Core;

namespace InventorySystem.Infrastructure
{
    // This class represents the Database Session
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
        }

        // This line says: "Create a Table called 'Items' that stores 'InventoryItem' data"
        public DbSet<InventoryItem> Items { get; set; }
    }
}
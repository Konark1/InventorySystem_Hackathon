using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore;
using InventorySystem.Core;

namespace InventorySystem.Infrastructure
{
    // NOTICE: We now inherit from 'IdentityDbContext', not just 'DbContext'
    public class InventoryDbContext : IdentityDbContext<ApplicationUser>
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
        }

        // This line says: "Create a Table called 'Items' that stores 'InventoryItem' data"
        public DbSet<InventoryItem> Items { get; set; }

        // NEW: The History Table (Transaction Log)
        public DbSet<InventoryTransaction> Transactions { get; set; }
    }
}
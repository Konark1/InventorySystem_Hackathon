using InventorySystem.Core;
using InventorySystem.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// Connection string to Azure SQL
var connectionString = "Server=tcp:inventory-server-konark.database.windows.net,1433;Initial Catalog=InventoryDB;Persist Security Info=False;User ID=sqladmin;Password=InventoryApp@2026;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

// Setup services
var services = new ServiceCollection();
services.AddDbContext<InventoryDbContext>(options => options.UseSqlServer(connectionString));
services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<InventoryDbContext>()
    .AddDefaultTokenProviders();

var serviceProvider = services.BuildServiceProvider();
var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

Console.WriteLine("🔐 Creating Admin User...");

// Check if admin already exists
var existingAdmin = await userManager.FindByEmailAsync("admin@inventory.com");
if (existingAdmin != null)
{
    Console.WriteLine("⚠️  Admin user already exists!");
    Console.WriteLine($"   Email: {existingAdmin.Email}");
    Console.WriteLine($"   Role: {existingAdmin.Role}");
    return;
}

// Create admin user
var adminUser = new ApplicationUser
{
    UserName = "admin@inventory.com",
    Email = "admin@inventory.com",
    EmailConfirmed = true,
    ShopName = "Admin Panel",
    FullName = "System Administrator",
    PhoneNumber = "1234567890",
    PhysicalAddress = "System",
    BusinessCategory = "Administration",
    Age = 30,
    Role = "Admin" // 👈 THIS IS THE KEY!
};

var result = await userManager.CreateAsync(adminUser, "Admin@123");

if (result.Succeeded)
{
    Console.WriteLine("✅ Admin user created successfully!");
    Console.WriteLine("\n📧 Login Credentials:");
    Console.WriteLine("   Email: admin@inventory.com");
    Console.WriteLine("   Password: Admin@123");
    Console.WriteLine("   Role: Admin");
}
else
{
    Console.WriteLine("❌ Failed to create admin user:");
    foreach (var error in result.Errors)
    {
        Console.WriteLine($"   - {error.Description}");
    }
}

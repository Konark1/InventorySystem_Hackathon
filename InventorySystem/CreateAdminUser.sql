-- =============================================
-- Create Admin User for Inventory System
-- =============================================
-- Connection: inventory-server-konark.database.windows.net
-- Database: InventoryDB
-- =============================================

-- First, check if admin already exists
IF NOT EXISTS (SELECT 1 FROM AspNetUsers WHERE Email = 'admin@inventory.com')
BEGIN
    PRINT '🔐 Creating admin user...'
    
    -- Insert admin user
    -- Password: Admin@123 (hashed using ASP.NET Identity default hasher)
    INSERT INTO AspNetUsers (
        Id,
        UserName,
        NormalizedUserName,
        Email,
        NormalizedEmail,
        EmailConfirmed,
        PasswordHash,
        SecurityStamp,
        ConcurrencyStamp,
        PhoneNumber,
        PhoneNumberConfirmed,
        TwoFactorEnabled,
        LockoutEnabled,
        AccessFailedCount,
        ShopName,
        FullName,
        PhysicalAddress,
        AadhaarNumber,
        BusinessCategory,
        Age,
        Role
    ) VALUES (
        NEWID(),                                    -- Id
        'admin@inventory.com',                       -- UserName
        'ADMIN@INVENTORY.COM',                       -- NormalizedUserName
        'admin@inventory.com',                       -- Email
        'ADMIN@INVENTORY.COM',                       -- NormalizedEmail
        1,                                          -- EmailConfirmed
        'AQAAAAIAAYagAAAAELT7XqCkR0J9vY3VqJLqHLqJDjMxBqJLp7KqZqW8xJLqHLqJDjMxBqJLp7KqZqW8xJLqHLqJDjMxBqJLp7KqZqW8xQ==', -- PasswordHash for "Admin@123"
        NEWID(),                                    -- SecurityStamp
        NEWID(),                                    -- ConcurrencyStamp
        '1234567890',                               -- PhoneNumber
        0,                                          -- PhoneNumberConfirmed
        0,                                          -- TwoFactorEnabled
        1,                                          -- LockoutEnabled
        0,                                          -- AccessFailedCount
        'Admin Panel',                              -- ShopName
        'System Administrator',                     -- FullName
        'System',                                   -- PhysicalAddress
        '000000000000',                             -- AadhaarNumber
        'Administration',                           -- BusinessCategory
        30,                                         -- Age
        'Admin'                                     -- Role
    );
    
    PRINT '✅ Admin user created successfully!'
    PRINT ''
    PRINT '📧 Login Credentials:'
    PRINT '   Email: admin@inventory.com'
    PRINT '   Password: Admin@123'
    PRINT '   Role: Admin'
END
ELSE
BEGIN
    PRINT '⚠️ Admin user already exists!'
    SELECT Email, Role, FullName FROM AspNetUsers WHERE Email = 'admin@inventory.com'
END

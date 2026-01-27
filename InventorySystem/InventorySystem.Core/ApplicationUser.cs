using Microsoft.AspNetCore.Identity;

namespace InventorySystem.Core
{
    // We inherit from IdentityUser, which automatically gives us:
    // Id, UserName, Email, PasswordHash, PhoneNumber, etc.
    public class ApplicationUser : IdentityUser
    {
        // Existing
        public string? ShopName { get; set; }

        // NEW FIELDS
        public string? FullName { get; set; }
        public string? PhysicalAddress { get; set; } // "Address" can be a reserved keyword sometimes
        public string? AadhaarNumber { get; set; }   // Storing as string is safer for leading zeros
        public string? BusinessCategory { get; set; } // e.g., Electronics, Grocery
        public int Age { get; set; }

        // NEW: User Role (ShopOwner or Admin)
        public string Role { get; set; } = "ShopOwner"; // Default to ShopOwner
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventorySystem.Application;
using InventorySystem.Core;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InventorySystem.Api.Controllers
{
    [Authorize(Roles = "Admin")] // ?? Only Admin users can access these endpoints
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IInventoryService inventoryService, UserManager<ApplicationUser> userManager)
        {
            _inventoryService = inventoryService;
            _userManager = userManager;
        }

        // GET: api/admin/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetSystemStats()
        {
            // Get total users count
            var totalUsers = _userManager.Users.Count();

            // Get all items across all users
            var allItems = await _inventoryService.GetAllItemsAsync(null);
            var totalItems = allItems.Count();
            var totalValue = allItems.Sum(i => i.Price * i.Quantity);

            return Ok(new
            {
                message = "Welcome, Super Admin! Here are your global stats.",
                statistics = new
                {
                    totalUsers = totalUsers,
                    totalInventoryItems = totalItems,
                    totalInventoryValue = totalValue,
                    timestamp = DateTime.UtcNow
                }
            });
        }

        // GET: api/admin/users
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.Select(u => new
            {
                u.Id,
                u.Email,
                u.ShopName,
                u.FullName,
                u.PhoneNumber,
                u.Role,
                u.BusinessCategory,
                RegisteredDate = u.Id // You can add a proper timestamp field if available
            }).ToList();

            return Ok(users);
        }

        // POST: api/admin/promote/{userId}
        [HttpPost("promote/{userId}")]
        public async Task<IActionResult> PromoteUserToAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            user.Role = "Admin";
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { message = $"User {user.Email} promoted to Admin successfully!" });
            }

            return BadRequest(result.Errors);
        }

        // POST: api/admin/demote/{userId}
        [HttpPost("demote/{userId}")]
        public async Task<IActionResult> DemoteUserToShopOwner(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            user.Role = "ShopOwner";
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { message = $"User {user.Email} demoted to ShopOwner successfully!" });
            }

            return BadRequest(result.Errors);
        }
    }
}

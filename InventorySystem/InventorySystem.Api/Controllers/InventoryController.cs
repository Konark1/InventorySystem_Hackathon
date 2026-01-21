using Microsoft.AspNetCore.Mvc;
using InventorySystem.Application;
using InventorySystem.Core;
using System.Threading.Tasks;

namespace InventorySystem.Api.Controllers
{
    [Route("api/[controller]")] // URL: localhost:xxxx/api/inventory
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        // Constructor Injection:
        // The API hands us the service automatically because we registered it in Program.cs
        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        // 1. GET: api/inventory
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllItemsAsync();
            return Ok(items);
        }

        // 2. POST: api/inventory (Add New Item)
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] InventoryItem item)
        {
            var createdItem = await _service.AddItemAsync(item);
            return Ok(createdItem);
        }

        // 3. POST: api/inventory/stock (Update Stock)
        // We use a clean object for the request
        [HttpPost("stock")]
        public async Task<IActionResult> UpdateStock([FromQuery] int id, [FromQuery] int change)
        {
            // Example URL: api/inventory/stock?id=1&change=5
            await _service.UpdateStockAsync(id, change);
            return Ok(new { Message = "Stock updated" });
        }
    }
}
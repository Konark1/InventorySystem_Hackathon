using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InventorySystem.Application;
using InventorySystem.Core;
using System.Threading.Tasks;

namespace InventorySystem.Api.Controllers
{
    [Authorize] // 👈 This locks the entire controller
    [Route("api/[controller]")] 
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

        // 1. GET: api/inventory (with optional search)
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            var items = await _service.GetAllItemsAsync(search);
            return Ok(items);
        }

        // 2. GET: api/inventory/{id} (Get Single Item by ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetItemByIdAsync(id);
            if (item == null)
                return NotFound(new { Message = $"Item with ID {id} not found" });
            
            return Ok(item);
        }

        // 3. POST: api/inventory (Add New Item)
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] InventoryItem item)
        {
            var createdItem = await _service.AddItemAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
        }

        // 4. PUT: api/inventory/{id} (Update/Edit Item)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] InventoryItem updatedItem)
        {
            var item = await _service.UpdateItemAsync(id, updatedItem);
            if (item == null)
                return NotFound(new { Message = $"Item with ID {id} not found" });
            
            return Ok(item);
        }

        // 5. POST: api/inventory/stock (Update Stock)
        [HttpPost("stock")]
        public async Task<IActionResult> UpdateStock([FromQuery] int id, [FromQuery] int change)
        {
            // Example URL: api/inventory/stock?id=1&change=5
            await _service.UpdateStockAsync(id, change);
            return Ok(new { Message = "Stock updated" });
        }

        // 6. DELETE: api/inventory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _service.DeleteItemAsync(id);
            
            // Return 204 No Content (Standard for Deletes)
            return NoContent(); 
        }

        // 7. GET: api/inventory/{id}/history (Get Transaction History)
        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory(int id)
        {
            var history = await _service.GetItemHistoryAsync(id);
            return Ok(history);
        }
    }
}
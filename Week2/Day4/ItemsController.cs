using Microsoft.AspNetCore.Mvc;

namespace MyFirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        // Hardcoded list
        private static List<string> items = new List<string>
        {
            "Laptop",
            "Mouse",
            "Keyboard"
        };

        // GET: api/items
        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok(items);
        }

        // GET: api/items/1
        [HttpGet("{id}")]
        public IActionResult GetItemById(int id)
        {
            if (id < 0 || id >= items.Count)
            {
                return NotFound();
            }

            return Ok(items[id]);
        }
    }
}
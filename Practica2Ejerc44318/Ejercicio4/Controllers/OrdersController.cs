using Microsoft.AspNetCore.Mvc;
using Ejercicio4.Models;

namespace Ejercicio4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static List<Order> orders = new();
        private static int nextId = 1;

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            if (order.Items.Any(i => i.Quantity < 0))
                return BadRequest("No se permiten cantidades negativas.");

            order.Id = nextId++;
            orders.Add(order);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();

            orders.Remove(order);
            return NoContent();
        }
    }
}


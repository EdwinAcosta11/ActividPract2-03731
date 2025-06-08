using Microsoft.AspNetCore.Mvc;
using Ejercicio6.Models;

namespace Ejercicio6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private static List<Product> productos = new();
        private static int nextId = 1;

        // GET /api/inventory
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(productos);
        }

        // POST /api/inventory
        [HttpPost]
        public IActionResult Create([FromBody] Product nuevo)
        {
            if (nuevo.Stock < 0)
                return BadRequest("El stock no puede ser negativo.");

            nuevo.Id = nextId++;
            productos.Add(nuevo);
            return Ok(nuevo);
        }

        // PUT /api/inventory/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product actualizado)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
                return NotFound();

            if (actualizado.Stock < 0)
                return BadRequest("El stock no puede ser negativo.");

            producto.Nombre = actualizado.Nombre;
            producto.Stock = actualizado.Stock;
            return Ok(producto);
        }

        // DELETE /api/inventory/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
                return NotFound();

            productos.Remove(producto);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Ejercicio5.Models;

namespace Ejercicio5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private static List<Event> eventos = new();
        private static int nextId = 1;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(eventos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Event evento)
        {
            if (evento.Fecha < DateTime.Now)
                return BadRequest("La fecha del evento no puede ser una fecha pasada.");

            evento.Id = nextId++;
            eventos.Add(evento);
            return Ok(evento);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Event updated)
        {
            var existing = eventos.FirstOrDefault(e => e.Id == id);
            if (existing == null)
                return NotFound();

            if (updated.Fecha < DateTime.Now)
                return BadRequest("La fecha del evento no puede ser una fecha pasada.");

            existing.Lugar = updated.Lugar;
            existing.Descripcion = updated.Descripcion;
            existing.Fecha = updated.Fecha;

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var evento = eventos.FirstOrDefault(e => e.Id == id);
            if (evento == null)
                return NotFound();

            eventos.Remove(evento);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ApiReservas.Models;

namespace ApiReservas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private static List<Reserva> reservas = new();
        private static int contadorId = 1;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(reservas);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Reserva reserva)
        {
            if (reserva.Fecha <= DateTime.Now)
                return BadRequest("La fecha debe ser en el futuro.");

            if (reserva.NumeroPersonas > 10)
                return BadRequest("No se pueden reservar más de 10 personas por mesa.");

            reserva.Id = contadorId++;
            reservas.Add(reserva);
            return CreatedAtAction(nameof(Get), new { id = reserva.Id }, reserva);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reserva = reservas.FirstOrDefault(r => r.Id == id);
            if (reserva == null)
                return NotFound();

            reservas.Remove(reserva);
            return NoContent();
        }
    }
}

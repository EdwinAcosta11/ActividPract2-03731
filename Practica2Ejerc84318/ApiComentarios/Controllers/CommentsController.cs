using Microsoft.AspNetCore.Mvc;
using ApiComentarios.Models;

namespace ApiComentarios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private static List<Comentario> comentarios = new();
        private static int contadorId = 1;

        // GET: api/comments
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(comentarios);
        }

        // POST: api/comments
        [HttpPost]
        public IActionResult Post([FromBody] Comentario comentario)
        {
            comentario.Id = contadorId++;
            comentario.Fecha = DateTime.Now;
            comentarios.Add(comentario);
            return CreatedAtAction(nameof(Get), new { id = comentario.Id }, comentario);
        }

        // DELETE: api/comments/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var comentario = comentarios.FirstOrDefault(c => c.Id == id);
            if (comentario == null)
                return NotFound();

            comentarios.Remove(comentario);
            return NoContent();
        }
    }
}


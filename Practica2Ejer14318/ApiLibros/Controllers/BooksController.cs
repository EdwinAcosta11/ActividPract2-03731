using Microsoft.AspNetCore.Mvc;
using ApiLibros.Models;
using ApiLibros.Data;

namespace ApiLibros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> GetAll() => BookRepository.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            var book = BookRepository.GetById(id);
            if (book == null) return NotFound();
            return book;
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Titulo) ||
                string.IsNullOrWhiteSpace(book.Autor) ||
                string.IsNullOrWhiteSpace(book.Genero))
            {
                return BadRequest("El título, autor y género no pueden estar vacíos.");
            }

            if (book.AnioPublicacion <= 0)
            {
                return BadRequest("El año de publicación debe ser un número positivo.");
            }

            BookRepository.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Titulo) ||
                string.IsNullOrWhiteSpace(book.Autor) ||
                string.IsNullOrWhiteSpace(book.Genero))
            {
                return BadRequest("El título, autor y género no pueden estar vacíos.");
            }

            if (book.AnioPublicacion <= 0)
            {
                return BadRequest("El año de publicación debe ser un número positivo.");
            }

            bool updated = BookRepository.Update(id, book);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = BookRepository.Delete(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

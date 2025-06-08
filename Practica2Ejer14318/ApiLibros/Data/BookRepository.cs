using ApiLibros.Models;

namespace ApiLibros.Data
{
    public static class BookRepository
    {
        private static List<Book> books = new List<Book>();
        private static int nextId = 1;

        public static List<Book> GetAll() => books;

        public static Book? GetById(int id) => books.FirstOrDefault(b => b.Id == id);

        public static void Add(Book book)
        {
            book.Id = nextId++;
            books.Add(book);
        }

        public static bool Update(int id, Book updatedBook)
        {
            var book = GetById(id);
            if (book == null) return false;

            book.Titulo = updatedBook.Titulo;
            book.Autor = updatedBook.Autor;
            book.AnioPublicacion = updatedBook.AnioPublicacion;
            book.Genero = updatedBook.Genero;
            return true;
        }

        public static bool Delete(int id)
        {
            var book = GetById(id);
            if (book == null) return false;

            books.Remove(book);
            return true;
        }
    }
}

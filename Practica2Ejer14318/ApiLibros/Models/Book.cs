﻿namespace ApiLibros.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnioPublicacion { get; set; }
        public string Genero { get; set; }
    }
}

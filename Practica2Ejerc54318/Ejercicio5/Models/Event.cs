namespace Ejercicio5.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Lugar { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}


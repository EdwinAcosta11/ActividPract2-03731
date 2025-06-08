namespace ApiReservas.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        public string NombreCliente { get; set; }

        public DateTime Fecha { get; set; }

        public int NumeroPersonas { get; set; }
    }
}


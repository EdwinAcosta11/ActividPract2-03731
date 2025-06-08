namespace Ejercicio4.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public decimal Total => Items.Sum(item => item.Quantity < 0 ? 0 : item.Quantity * item.Price);
    }
}

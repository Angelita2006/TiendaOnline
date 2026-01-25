namespace TiendaOnline.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        public string UsuarioId { get; set; }
        
        public ApplicationUser Usuario { get; set; }

        public List<LineaPedido> LineasPedido { get; set; } = new();
    }
}

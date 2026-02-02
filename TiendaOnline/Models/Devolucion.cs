using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models
{
    public class Devolucion
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Es obligatorio introducir una fecha de devolución.")]
        public DateTime FechaDevolucion { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir un motivo.")]
        public string MotivoDevolucion { get; set; }

        public bool Aceptada { get; set; }

        public int PedidoId { get; set; }

        public Pedido Pedido { get; set; }

        public string UsuarioId { get; set; }

        public ApplicationUser Usuario { get; set; }
    }
}

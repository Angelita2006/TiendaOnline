using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Es obligatorio introducir un nombre.")]
        [StringLength(100)]
        public String Nombre { get; set; }
        [Required(ErrorMessage = "Es obligatorio introducir una descripción.")]
        [StringLength(500)]
        public String Descripcion { get; set; }
        [Required(ErrorMessage = "Es obligatorio introducir un precio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio no puede ser negativo.")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "Es obligatorio introducir la cantidad de existencias.")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad de existencias no puede ser negativa.")]
        public int Existencias { get; set; }
        [StringLength(200)]
        public String? ImagenUrl { get; set; }

    }
}

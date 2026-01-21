using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using System.Diagnostics.Eventing.Reader;
using TiendaOnline.Data;
using TiendaOnline.Helpers;
using TiendaOnline.Models;

namespace TiendaOnline.Controllers
{
    public class CarritoController : Controller
    {
        private ProductoDAO _productoDAO;
        private readonly ISession _session;

        public CarritoController(ApplicationDbContext context)
        {
            _productoDAO = new ProductoDAO(context);
        }

        public async Task<IActionResult> Index()
        {
            var elementos = CarritoHelper.ObtenerCarrito(_session);
            return View(elementos);
        }

        //[HttpGet]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Agregar(int ProductoId)
        {
            var producto = await _productoDAO.GetProductoById(ProductoId);
            if (producto != null)
            {
                var elementos = CarritoHelper.ObtenerCarrito(_session);
                foreach (var item in elementos)
                {
                    if (item.ProductoId == producto.Id)
                    {
                        item.Cantidad += 1;
                        CarritoHelper.GuardarCarrito(_session, elementos);
                        break;
                    }
                }
                var nuevoItem = new CarritoItem
                {
                    ProductoId = producto.Id,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Cantidad = 1
                };
                elementos.Add(nuevoItem);
                CarritoHelper.GuardarCarrito(_session, elementos);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(int ProductoId)
        {
            var elementos = CarritoHelper.ObtenerCarrito(_session);
            var itemAEliminar = elementos.FirstOrDefault(i => i.ProductoId == ProductoId);
            if (itemAEliminar != null)
            {
                elementos.Remove(itemAEliminar);
                CarritoHelper.GuardarCarrito(_session, elementos);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Vaciar()
        {
            CarritoHelper.VaciarCarrito(_session);
            return RedirectToAction("Index");
        }
    }
}

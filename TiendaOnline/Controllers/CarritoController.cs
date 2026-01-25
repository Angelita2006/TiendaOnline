using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TiendaOnline.Data;
using TiendaOnline.Helpers;
using TiendaOnline.Models;

namespace TiendaOnline.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class CarritoController : Controller
    {
        private readonly ProductoDAO _productoDAO;
        private readonly PedidoDAO _pedidoDAO;

        public CarritoController(ApplicationDbContext context)
        {
            _productoDAO = new ProductoDAO(context);
            _pedidoDAO = new PedidoDAO(context);
        }

        public IActionResult Index()
        {
            var elementos = CarritoHelper.ObtenerCarrito(HttpContext.Session);
            return View(elementos);
        }

        [HttpGet]
        public async Task<IActionResult> Agregar(int id)
        {
            var producto = await _productoDAO.GetProductoById(id);
            if (producto != null)
            {
                var carrito = CarritoHelper.ObtenerCarrito(HttpContext.Session);

                var itemExistente = carrito.FirstOrDefault(i => i.ProductoId == producto.Id);
                if (itemExistente != null)
                {
                    itemExistente.Cantidad++;
                }
                else
                {
                    carrito.Add(new CarritoItem
                    {
                        ProductoId = producto.Id,
                        Nombre = producto.Nombre,
                        Precio = producto.Precio,
                        Cantidad = 1
                    });
                }

                CarritoHelper.GuardarCarrito(HttpContext.Session, carrito);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            var carrito = CarritoHelper.ObtenerCarrito(HttpContext.Session);
            var item = carrito.FirstOrDefault(i => i.ProductoId == id);

            if (item != null)
            {
                carrito.Remove(item);
                CarritoHelper.GuardarCarrito(HttpContext.Session, carrito);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Vaciar()
        {
            CarritoHelper.VaciarCarrito(HttpContext.Session);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarCompra()
        {
            var carrito = CarritoHelper.ObtenerCarrito(HttpContext.Session);

            if (!carrito.Any())
                return RedirectToAction("Index");

            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var pedido = new Pedido
            {
                Fecha = DateTime.Now,
                UsuarioId = usuarioId,
                Total = carrito.Sum(i => i.Precio * i.Cantidad),
                LineasPedido = new List<LineaPedido>()
            };

            foreach (var item in carrito)
            {
                pedido.LineasPedido.Add(new LineaPedido
                {
                    ProductoId = item.ProductoId,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.Precio
                });
            }

            await _pedidoDAO.AddPedido(pedido);

            CarritoHelper.VaciarCarrito(HttpContext.Session);

            return RedirectToAction("Confirmacion", new { id = pedido.Id });
        }

        [Authorize]
        public async Task<IActionResult> Confirmacion(int id)
        {
            var pedido = await _pedidoDAO.GetPedidoById(id);
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (pedido == null || pedido.UsuarioId != usuarioId)
                return Unauthorized();

            return View(pedido);
        }
    }
}

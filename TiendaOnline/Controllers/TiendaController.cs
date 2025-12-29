using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TiendaOnline.Data;
using TiendaOnline.Models;

namespace TiendaOnline.Controllers
{
    public class TiendaController : Controller
    {
        private ProductoDAO _productoDAO;

        public TiendaController(ApplicationDbContext context)
        {
            _productoDAO = new ProductoDAO(context);
        }

        public async Task<IActionResult> Index(string texto)
        {
            var productos = await _productoDAO.GetAllProductos();
            ViewData["totalProductos"] = productos.Count;
            if (!texto.IsNullOrEmpty())
            {
                productos = await _productoDAO.GetFiltradoPorTexto(texto);
            }
            else
            {
                texto = "";
            }
            ViewData["Texto"] = texto;
            ViewData["totalProductosFiltrados"] = productos.Count;
            return View(productos);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var producto = await _productoDAO.GetProductoById(id);
            return View(producto);
        }
    }
}
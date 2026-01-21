using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TiendaOnline.Data;
using TiendaOnline.Models;

namespace TiendaOnline.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {
        private ProductoDAO _productoDAO;

        public ProductosController(ApplicationDbContext context)
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

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await _productoDAO.GetProductoById(id);
            return View(producto);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Eliminar(Producto producto)
        {
            await _productoDAO.DeleteProducto(producto.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Editar(int id)
        {
            var producto = await _productoDAO.GetProductoById(id);
            return View(producto);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Editar(IFormFile? ImagenFile, Producto producto)
        {
            if (ModelState.IsValid)
            {
                if (ImagenFile != null && ImagenFile.Length > 0)
                {
                    string rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/images");
                    Directory.CreateDirectory(rutaCarpeta);
                    string nombreArchivo = Guid.NewGuid().ToString() +
                    Path.GetExtension(ImagenFile.FileName);
                    string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);
                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        await ImagenFile.CopyToAsync(stream);
                    }
                    producto.ImagenUrl = nombreArchivo;
                }
                await _productoDAO.UpdateProducto(producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Crear()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Crear(IFormFile? ImagenFile, Producto producto)
        {
            if (ModelState.IsValid)
            {
                if (ImagenFile != null && ImagenFile.Length > 0)
                {
                    string rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                    Directory.CreateDirectory(rutaCarpeta);
                    string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(ImagenFile.FileName);
                    string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);
                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        await ImagenFile.CopyToAsync(stream);
                    }
                    producto.ImagenUrl = nombreArchivo;
                }
                await _productoDAO.AddProducto(producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var producto = await _productoDAO.GetProductoById(id);
            return View(producto);
        }
    }
}
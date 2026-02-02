using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TiendaOnline.Data;
using TiendaOnline.Models;

namespace TiendaOnline.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class MisDevolucionesController : Controller
    {
        private readonly DevolucionDAO _devolucionDAO;

        public MisDevolucionesController(ApplicationDbContext context)
        {
            _devolucionDAO = new DevolucionDAO(context);
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var devoluciones = await _devolucionDAO.GetAllDevolucionesByUsuarioId(usuarioId);
            return View(devoluciones);
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Crear(int pedidoId)
        {
            return View(new Devolucion { PedidoId = pedidoId });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Crear(Devolucion devolucion)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            devolucion.UsuarioId = usuarioId;
            devolucion.FechaDevolucion = DateTime.Now;
            devolucion.Aceptada = false;
            await _devolucionDAO.AddDevolucion(devolucion);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Detalles(int id)
        {
            var devolucion = await _devolucionDAO.GetDevolucionById(id);
            return View(devolucion);
        }
    }
}

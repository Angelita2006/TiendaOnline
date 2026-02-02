using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Data;
using TiendaOnline.Models;

namespace TiendaOnline.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DevolucionesController : Controller
    {
        private readonly DevolucionDAO _devolucionDAO;

        public DevolucionesController(ApplicationDbContext context)
        {
            _devolucionDAO = new DevolucionDAO(context);
        }

        public async Task<IActionResult> Index()
        {
            var devoluciones = await _devolucionDAO.GetAllDevoluciones();
            return View(devoluciones);
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int id)
        {
            var devolucion = await _devolucionDAO.GetDevolucionById(id);
            devolucion.Aceptada = true;
            await _devolucionDAO.Update(devolucion);
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

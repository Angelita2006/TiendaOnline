using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TiendaOnline.Data;

namespace TiendaOnline.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class PedidosController : Controller
    {
        private readonly PedidoDAO _pedidoDAO;

        public PedidosController(ApplicationDbContext context)
        {
            _pedidoDAO = new PedidoDAO(context);
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var pedidos = await _pedidoDAO.GetAllPedidosByUsuarioId(usuarioId);
            return View(pedidos);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var pedido = await _pedidoDAO.GetPedidoById(id);
            return View(pedido);
        }
    }
}


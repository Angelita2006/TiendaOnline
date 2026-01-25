using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Data;

namespace TiendaOnline.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PedidosAdministrador : Controller
    {
        private readonly PedidoDAO _pedidoDAO;

        public PedidosAdministrador(ApplicationDbContext context)
        {
            _pedidoDAO = new PedidoDAO(context);
        }

        public async Task<IActionResult> Index()
        {
            var pedidos = await _pedidoDAO.GetAllPedidos();
            return View(pedidos);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var pedido = await _pedidoDAO.GetPedidoById(id);
            return View(pedido);
        }
    }
}

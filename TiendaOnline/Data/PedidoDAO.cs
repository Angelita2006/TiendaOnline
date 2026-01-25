using Microsoft.EntityFrameworkCore;
using TiendaOnline.Models;

namespace TiendaOnline.Data
{
    public class PedidoDAO
    {
        private readonly ApplicationDbContext _context;

        public PedidoDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> GetPedidoById(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.LineasPedido)
                .ThenInclude(lp => lp.Producto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pedido>> GetAllPedidos()
        {
            return await _context.Pedidos
                .Include(p => p.Usuario)
                .ToListAsync();
        }

        public async Task<List<Pedido>> GetAllPedidosByUsuarioId(string usuarioId)
        {
            return await _context.Pedidos
                .Where(p => p.UsuarioId == usuarioId)
                .Include(p => p.LineasPedido)
                .ToListAsync();
        }

        public async Task AddPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TiendaOnline.Models;

namespace TiendaOnline.Data
{
    public class DevolucionDAO
    {
        private readonly ApplicationDbContext _context;

        public DevolucionDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Devolucion> GetDevolucionById(int id)
        {
            return await _context.Devoluciones
                .Include(p => p.Usuario)
                .Include(p => p.Pedido)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Devolucion>> GetAllDevoluciones()
        {
            return await _context.Devoluciones
                .Include(p => p.Usuario)
                .Include(p => p.Pedido)
                .OrderByDescending(p => p.FechaDevolucion)
                .ToListAsync();
        }

        public async Task<List<Devolucion>> GetAllDevolucionesByUsuarioId(string usuarioId)
        {
            return await _context.Devoluciones
                .Where(p => p.UsuarioId == usuarioId)
                .Include(p => p.Pedido)
                .OrderByDescending(p => p.FechaDevolucion)
                .ToListAsync();
        }

        public async Task AddDevolucion(Devolucion devolucion)
        {
            var pedido = _context.Pedidos.Find(devolucion.PedidoId);
            devolucion.Pedido = pedido;
            _context.Devoluciones.Add(devolucion);
            if (pedido != null)
            {
                pedido.Devuelto = true;
            }
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Devolucion devolucion)
        {
            _context.Devoluciones.Update(devolucion);
            await _context.SaveChangesAsync();
        }
    }
}

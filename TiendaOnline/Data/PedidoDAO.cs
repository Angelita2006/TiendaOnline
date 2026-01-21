namespace TiendaOnline.Data
{
    public class PedidoDAO
    {
        private readonly ApplicationDbContext _context;
        
        public PedidoDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Pedido>> GetPedidoById(int Id)
        {
            return await _context.Pedidos.Where(p => p.UsuarioId == Id).ToListAsync();
        }

        public async Task AddPedido(Models.Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }
    }
}

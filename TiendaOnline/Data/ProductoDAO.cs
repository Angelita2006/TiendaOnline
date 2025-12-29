using Microsoft.EntityFrameworkCore;

namespace TiendaOnline.Data
{
    public class ProductoDAO
    {
        private readonly ApplicationDbContext _context;

        public ProductoDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Producto>> GetAllProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        internal async Task<List<Models.Producto>> GetFiltradoPorTexto(string texto)
        {
            return await _context.Productos.Where(a => a.Nombre.ToLower().Contains(texto.ToLower())).ToListAsync();
        }

        public async Task<Models.Producto?> GetProductoById(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task AddProducto(Models.Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProducto(Models.Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }

    }
}

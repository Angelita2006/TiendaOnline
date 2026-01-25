using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.Models;

namespace TiendaOnline.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //DbSet<TiendaOnline.Models.Usuario> Usuarios { get; set; }
        public DbSet<TiendaOnline.Models.Producto> Productos { get; set; }
        public DbSet<TiendaOnline.Models.Pedido> Pedidos { get; set; }

    }
}

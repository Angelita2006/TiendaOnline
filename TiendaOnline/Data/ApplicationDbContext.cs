using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TiendaOnline.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //DbSet<TiendaOnline.Models.Usuario> Usuarios { get; set; }
        public DbSet<TiendaOnline.Models.Producto> Productos { get; set; }
        
    }
}

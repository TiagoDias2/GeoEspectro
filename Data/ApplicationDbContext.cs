using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Data;
using GeoEspectro.Models;

namespace GeoEspectro.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Utilizadores> Utilizadores { get; set; }
        public DbSet<Recursos> Recursos { get; set; }
    }
}

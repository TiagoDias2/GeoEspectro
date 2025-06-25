using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Artigos> Artigos { get; set; }
        public DbSet<Gostos> Gostos { get; set; }
        public DbSet<Recursos> Recursos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relacionamento ApplicationUser -> Utilizadores
            builder.Entity<ApplicationUser>()
              .HasOne(a => a.Utilizador)
              .WithOne() // RELAÇÃO 1:1
              .HasForeignKey<ApplicationUser>(a => a.UtilizadoresID)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

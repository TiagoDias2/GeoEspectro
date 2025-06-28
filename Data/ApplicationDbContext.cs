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

            // Relacionamento Utilizadores -> ApplicationUser
            builder.Entity<Utilizadores>()
                .HasOne(u => u.IdentityUser)
                .WithOne() // ou .WithOne(a => a.Utilizador) se desejar navegação reversa
                .HasForeignKey<Utilizadores>(u => u.IdentityUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

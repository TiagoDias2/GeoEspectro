using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Models;

namespace GeoEspectro.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Utilizadores> Utilizadores { get; set; }
        public DbSet<Artigos> Artigos { get; set; }
        public DbSet<Gostos> Gostos { get; set; }
        public DbSet<Recursos> Recursos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Detalhes> Detalhes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Utilizadores>()
                .HasOne(u => u.IdentityUser)
                .WithOne()
                .HasForeignKey<Utilizadores>(u => u.IdentityUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index para performance (ok)
            builder.Entity<Artigos>()
                .HasIndex(a => a.AutorFK);

            // Corrigir relação para evitar múltiplos cascades
            builder.Entity<Artigos>()
                .HasOne(a => a.Autor)
                .WithMany(u => u.ListaArtigos)
                .HasForeignKey(a => a.AutorFK)
                .OnDelete(DeleteBehavior.Restrict); // ou .NoAction
        }

    }
}

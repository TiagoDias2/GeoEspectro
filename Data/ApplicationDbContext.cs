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
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Detalhes> Detalhes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Utilizador)
                .WithOne()
                .HasForeignKey<ApplicationUser>(a => a.UtilizadoresID)
                .OnDelete(DeleteBehavior.Restrict);
            // Relacionamento Utilizadores -> Artigos (1:N)
            builder.Entity<Artigos>()
            .HasIndex(a => a.AutorFK);

            // Aqui podes adicionar outras configurações do modelo, se necessário
        }
    }
}

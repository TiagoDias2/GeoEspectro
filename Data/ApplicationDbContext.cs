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

            // Relacionamento Utilizadores -> Artigos (1:N)
            builder.Entity<Artigos>()
            .HasIndex(a => a.AutorFK);


        }
    }
}

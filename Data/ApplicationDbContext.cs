using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Models;

namespace GeoEspectro.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Recursos> Recursos { get; set; }

    public DbSet<Artigos> Artigos { get; set; }

    public DbSet<Utilizadores> Utilizadores { get; set; }

    public DbSet<Categorias> Categorias { get; set; }

    public DbSet<Detalhes> Detalhes { get; set; }
}

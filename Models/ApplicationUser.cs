using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Models;
using Microsoft.AspNetCore.Identity;

namespace GeoEspectro.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int? UtilizadorID { get; set; } // FK para Utilizadores
        public Utilizadores? Utilizador { get; set; } // Propriedade de navegação
    }
}

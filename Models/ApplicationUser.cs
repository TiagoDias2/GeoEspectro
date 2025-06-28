using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoEspectro.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Chave estrangeira
        [ForeignKey(nameof(Utilizador))]
        public int? UtilizadoresID { get; set; }

        // Propriedade de navegação
        public Utilizadores Utilizador { get; set; }
    }
}

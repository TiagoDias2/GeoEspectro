using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoEspectro.Data
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("UtilizadoresID")]
        public int? UtilizadoresID { get; set; } // FK para Utilizadores

        public Utilizadores? Utilizador { get; set; } // Propriedade de navegação
    }
}

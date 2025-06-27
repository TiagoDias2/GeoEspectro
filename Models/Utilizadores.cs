using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeoEspectro.Data;
using Microsoft.AspNetCore.Identity;

namespace GeoEspectro.Models
{
    public class Utilizadores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Nome")]
        [StringLength(50)]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Morada")]
        [StringLength(50)]
        public string Morada { get; set; }

        [Display(Name = "Código Postal")]
        [StringLength(50)]
        [RegularExpression("[1-9][0-9]{3}-[0-9]{3} [A-Za-z ]+",
            ErrorMessage = "No {0} só são aceites algarismos e letras inglesas.")]
        public string CodPostal { get; set; }

        [Display(Name = "País")]
        [StringLength(50)]
        public string Pais { get; set; }

        [Display(Name = "NIF")]
        [StringLength(9)]
        [RegularExpression("[1-9][0-9]{8}", ErrorMessage = "Deve escrever apenas 9 digitos no {0}")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Nif { get; set; }

        [Display(Name = "Telemóvel")]
        [StringLength(18)]
        [RegularExpression("(([+]|00)[0-9]{1,5})?[1-9][0-9]{5,10}", ErrorMessage = "Escreva um nº de telefone. Pode adicionar indicativo do país.")]
        public string Telemovel { get; set; }

        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        // 🔗 Chave estrangeira para AspNetUsers
        [Required]
        [StringLength(450)]
        public string IdentityUserId { get; set; }

        [ForeignKey("IdentityUserId")]
        public ApplicationUser IdentityUser { get; set; }

        public ICollection<Gostos> ListaGostos { get; set; }
        public ICollection<Recursos> ListaRecursos { get; set; }
        public ICollection<Artigos> ListaArtigos { get; set; }
    }
}

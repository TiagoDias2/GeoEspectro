using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoEspectro.Models
{
    /// <summary>
    /// Utilizadores não anónimos da aplicação 
    /// </summary>
    public class Utilizadores
    {
        /// <summary>
        /// Identificador da classe do utilizador
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// Nome do Utilizador
        /// </summary>
        [Display(Name = "Nome")]
        [StringLength(50)]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// Morada do Utilizador
        /// </summary>
        [Display(Name = "Morada")]
        [StringLength(50)]
        public string Morada { get; set; }

        /// <summary>
        /// Código Postal do Utilizador
        /// </summary>
        [Display(Name = "Código Postal")]
        [StringLength(50)]
        [RegularExpression("[1-9][0-9]{3}-[0-9]{3} [A-Za-z ]+",
            ErrorMessage = "No {0} só são aceites algarismos e letras inglesas.")]
        public string CodPostal { get; set; }

        /// <summary>
        /// País do Utilizador
        /// </summary>
        [Display(Name = "País")]
        [StringLength(50)]
        public string Pais { get; set; }

        /// <summary>
        /// Número de Identificação Fiscal
        /// </summary>
        [Display(Name = "NIF")]
        [StringLength(9)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "O {0} deve conter exatamente 9 dígitos.")]
        public string Nif { get; set; }

        /// <summary>
        /// Número de Telemóvel
        /// </summary>
        [Display(Name = "Telemóvel")]
        [StringLength(18)]
        [RegularExpression(@"9[1236][0-9]{7}", ErrorMessage = "O número de telemóvel deve ser válido.")]
        public string Telemovel { get; set; }

        /// <summary>
        /// Nome de utilizador (ligação à autenticação)
        /// </summary>
        [Display(Name = "Username")]
        [StringLength(50)]
        public string UserName { get; set; }

        // Relacionamentos

        public ICollection<Artigos> ListaArtigos { get; set; } = [];
        public ICollection<Recursos> ListaRecursos { get; set; } = [];
        public ICollection<Gostos> ListaGostos { get; set; } = [];
    }
}

using System.ComponentModel.DataAnnotations;

namespace GeoEspectro.Models
{
    /// <summary>
    /// Utilizadores não anonimos da aplicação 
    /// </summary>
    public class Utilizadores
    {

        /// <summary>
        /// Identificador da class do utilizador
        /// </summary>
        [Key]
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
        /// Numero de Identificação Fiscal do Utilizador
        /// </summary>
        [Display(Name = "NIF")]
        [StringLength(9)]
        [RegularExpression("[1-9][0-9]{8}", ErrorMessage = "Deve escrever apenas 9 digitos no {0}")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Nif { get; set; }

        /// <summary>
        /// Número do dispositivo protátil do Utilizador
        /// </summary>
        [Display(Name = "Telemóvel")]
        [StringLength(18)]
        [RegularExpression("(([+]|00)[0-9]{1,5})?[1-9][0-9]{5,10}", ErrorMessage = "Escreva um nº de telefone. Pode adicionar indicativo do país.")]
        public string Telemovel { get; set; }


        /// <summary>
        /// Este atributo servirá para fazer a 'ponte' 
        /// entre a tabela dos Utilizadores e a 
        /// tabela da Autenticação da Microsoft Identity
        /// </summary>
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        // Relacionamentos M - N

        /// <summary>
        /// Lista dos artigos do Utilizador
        /// </summary>
        public ICollection<Gostos> ListaGostos { get; set; }

        /// <summary>
        /// Lista dos Recursos Multimédia do Utilizador
        /// </summary>
        public ICollection<Recursos> ListaRecursos { get; set; }

        /// <summary>
        /// Lista de Artigos Associados a Utilizadores
        /// </summary>
        public ICollection<Artigos> ListaArtigos { get; set;}
    }
}

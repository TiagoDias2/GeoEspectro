using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoEspectro.Models
{
    public class Recursos
    {
        /// <summary>
        /// Identificador do Recurso
        /// </summary>
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Caminho { get; set; } = "";
        /// <summary>
        /// Nome do Recurso
        /// </summary>
        public string Nome { get; set; } = "";
        /// <summary>
        /// Data de Publicação dos Recursos
        /// </summary>
        public DateTime Data { get; set; }
        
        /// <summary>
        /// Tipo de Recursos Multimédia
        /// </summary>
        public string Tipo { get; set; }
        /// <summary>
        /// Local retratado pelo Recurso Multimédia
        /// </summary>
        public string Local { get; set; }

        /// <summary>
        /// Documento com o Recurso
        /// </summary>
        public string Ficheiro { get; set; }

        /// <summary>
        /// Observações para os Recursos Multimédias
        /// </summary>
        public string? Observacao { get; set; }

        // Relacionamentos N - 1

        /// <summary>
        /// ForeignKey para o Autor do Recurso Multimédia
        /// </summary>
        [ForeignKey(nameof(Autor))]
        public int AutorFK { get; set; }

        /// <summary>
        /// ForeignKey para o Autor do Recurso Multimédia
        /// </summary>
        public Utilizadores Autor { get; set; } = null!;

        // Relacionamento M - N

        /// <summary>
        /// Lista de Detalhes dos Recursos Multimédia 
        /// </summary>
        public ICollection<Detalhes> ListaArtigos { get; set; } = [];
    }
}

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
        /// <summary>
        /// Nome da biblioteca
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Data de Publicação dos Recursos
        /// </summary>
        public DateTime Data { get; set; }
        /// <summary>
        /// Tipo de Recursos Multimédia
        /// </summary>
        public string Tipo { get; set; }
        /// <summary>
        /// Local dos Recursos Multimédia
        /// </summary>
        public String Local { get; set; }
        /// <summary>
        /// Observações para os Recursos Multimédias
        /// </summary>
        public String Observação { get; set; }

        // Relacionamentos N - 1

        /// <summary>
        /// ForeignKey para o utilizador associado à biblioteca
        /// </summary>
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }

        /// <summary>
        /// ForeignKey para o utilizador associado à biblioteca
        /// </summary>
        public Utilizadores Utilizador { get; set; }

        // Relacionamento M - N

        /// <summary>
        /// Lista dos artigos que compõem as bibliotecas
        /// </summary>
        public ICollection<Artigos> ListaArtigos { get; set; }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoEspectro.Models
{
    public class Artigos
    {
        /// <summary>
        /// Numero de Identificação do Artigo 
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Titulo do artigo
        /// </summary>
        [Display(Name = "Título")]
        [StringLength(120)]
        public string Titulo { get; set; }
        /// <summary>
        /// Fotografia associada ao artigo
        /// </summary>
        public string Fotografia { get; set; }
        /// <summary>
        /// Conteudo do artigo
        /// </summary>
        public string Texto { get; set; }
        /// <summary>
        /// Data de publicação do artigo
        /// </summary>
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        // Relacionamento 1 - N

        /// <summary>
        /// ForeignKey para o utilizador associado ao artigo
        /// </summary>
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }

        /// <summary>
        /// ForeignKey para o utilizador associado ao artigo
        /// </summary>
        public Utilizadores Utilizador {  get; set; }

        // Relacionamentos M - N

        /// <summary>
        /// Lista de todas as categorias
        /// </summary>
        public ICollection<Categorias> ListaCategorias { get; set; }
            
        /// <summary>
        /// Lista de todos os artigos
        /// </summary>
        public ICollection<Gostos> ListaGostos { get; set; }

        /// <summary>
        /// Lista dos artigos que compõem os Recursos
        /// </summary>
        public ICollection<Recursos> ListaRecursos { get; set; }
    }
}

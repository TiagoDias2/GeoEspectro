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
        public string Titulo { get; set; } = "";

        /// <summary>
        /// Fotografia associada ao artigo (Descarte)
        /// </summary>
        public string Fotografia { get; set; } = null!;

        /// <summary>
        /// Conteudo do artigo
        /// </summary>
        public string Texto { get; set; } = "";
        /// <summary>
        /// Data de publicação do artigo
        /// </summary>
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        // Relacionamento 1 - N

        /// <summary>
        /// ForeignKey para o autor do artigo
        /// </summary>
        [ForeignKey(nameof(Autor))]
        public int AutorFK { get; set; }

        /// <summary>
        /// ForeignKey para o autor do artigo
        /// </summary>
        public Utilizadores Autor { get; set; } = null!;

        // Relacionamentos M - N

        /// <summary>
        /// Lista de todas as categorias
        /// </summary>
        public ICollection<Categorias> ListaCategorias { get; set; } = [];


        /// <summary>
        /// Lista de categorias selecionadas
        /// </summary>
        [NotMapped]
        public List<int> ListaCategoriasSelecionadas { get; set; } = new List<int>();

        /// <summary>
        /// Lista de Gostos associados a cada artigo
        /// </summary>
        public ICollection<Gostos> ListaGostos { get; set; } = [];

        /// <summary>
        /// Lista dos artigos que compõem os Recursos
        /// </summary>
        public ICollection<Detalhes> ListaRecursos { get; set; } = [];

        //public ICollection<Utilizadores> ListaUtilizadores { get; set; }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoEspectro.Models
{
    [Table("Artigos")]
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
        [DataType(DataType.Date)] // transforma o atributo, na BD, em 'Date'
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
                     ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        public DateTime Data { get; set; }

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

        /// <summary>
        /// Lista de utilizadores associados aos artigos
        /// </summary>
        public ICollection<Utilizadores> ListaUtilizados { get; set; }
    }
}

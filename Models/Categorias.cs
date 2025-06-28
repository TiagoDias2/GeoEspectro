using System.ComponentModel.DataAnnotations;

namespace GeoEspectro.Models
{
    /// <summary>
    /// Categorias de filtragem para cada artigo
    /// </summary>
    public class Categorias
    {
        /// <summary>
        /// Identificador da classe das Categorias
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome das categorias dos artigos
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [StringLength(20)]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; } = "";

        /// <summary>
        /// Lista dos artigos associados às categorias
        /// </summary>
        public ICollection<ArtigosCategoria> ListaArtigos { get; set; } = [];
    }
}

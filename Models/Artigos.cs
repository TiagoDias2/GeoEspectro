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
        [Display(Name = "Título")]
        [StringLength(120)]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        public string Titulo { get; set; } = "";

        /// <summary>
        /// Conteúdo do artigo
        /// </summary>
        public string Texto { get; set; } = "";

        /// <summary>
        /// Data de publicação do artigo
        /// </summary>
        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        public DateTime Data { get; set; }

        /// <summary>
        /// Chave estrangeira para o autor do artigo
        /// </summary>
        [ForeignKey(nameof(Autor))]
        public int AutorFK { get; set; }

        /// <summary>
        /// Autor do artigo
        /// </summary>
        public Utilizadores Autor { get; set; } = null!;

        /// <summary>
        /// Lista de todas as categorias associadas
        /// </summary>
        public ICollection<ArtigosCategoria> ListaCategorias { get; set; } = [];

        /// <summary>
        /// Lista de gostos associados a este artigo
        /// </summary>
        public ICollection<Gostos> ListaGostos { get; set; } = [];

        /// <summary>
        /// Lista de recursos multimédia associados
        /// </summary>
        public ICollection<Detalhes> ListaRecursos { get; set; } = [];
    }
}


        /// <summary>
        /// Lista dos artigos que compõem os Recursos
        /// </summary>
        public ICollection<Detalhes> ListaRecursos { get; set; } = [];

        //public ICollection<Utilizadores> ListaUtilizadores { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace GeoEspectro.Models
{
    public class Detalhes
    {
        /// <summary>
        /// Detalhe Multimedia Princapal
        /// </summary>
        public bool principal { get; set; }

        /// <summary>
        /// ForeignKey para referenciar o utilizador ao recurso
        /// </summary>
        [ForeignKey(nameof(Recurso))]
        public int RecursoFK { get; set; }

        /// <summary>
        /// ForeignKey para referenciar o utilizador ao recurso
        /// </summary>
        public Recursos Recurso { get; set; }

        /// <summary>
        /// ForeignKey para o artigo que o utilizador gostou
        /// </summary>
        [ForeignKey(nameof(Artigo))]
        public int ArtigoFK { get; set; }

        /// <summary>
        /// ForeignKey para o artigo que o utilizador gostou
        /// </summary>
        public Artigos Artigo { get; set; }

    }
}

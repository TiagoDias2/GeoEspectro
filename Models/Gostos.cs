using System.ComponentModel.DataAnnotations.Schema;

namespace GeoEspectro.Models
{
    public class Gostos
    {
        /// <summary>
        /// Data em que o utilizador gostou do artigo
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// ForeignKey para referenciar o utilizador que gosta do artigo
        /// </summary>
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFk { get; set; }

        /// <summary>
        /// ForeignKey para referenciar o utilizador que gosta do artigo
        /// </summary>
        public Utilizadores Utilizador { get; set; }

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

using System.ComponentModel.DataAnnotations.Schema;

namespace GeoEspectro.Models
{
    public class Bibliotecas
    {
        /// <summary>
        /// Identificador da Biblioteca
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nome da biblioteca
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Data de Publicação da Biblioteca
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// ForeignKey para o utilizador associado à biblioteca
        /// </summary>
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }

        /// <summary>
        /// ForeignKey para o utilizador associado à biblioteca
        /// </summary>
        public Utilizadores Utilizador { get; set; }

        /// <summary>
        /// Lista dos artigos que compõem as bibliotecas
        /// </summary>
        public ICollection<Artigos> ListaArtigos { get; set; }
    }
}

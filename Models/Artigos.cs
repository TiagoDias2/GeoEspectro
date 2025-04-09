using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoEspectro.Models
{
    public class Artigos
    {
        /// <summary>
        /// Numero de Identificação do Artigo 
        /// </summary>
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
        public DateTime Data { get; set; }

 
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

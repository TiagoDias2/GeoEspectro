﻿namespace GeoEspectro.Models
{
    /// <summary>
    /// Categorias de filtragem para cada artigo
    /// </summary>
    public class Categorias
    {
        /// <summary>
        /// Identificador da class das Categorias
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome das categorias dos artigos
        /// </summary>
        public string Categoria { get; set; }
        /// <summary>
        /// Lista dos artigos associados ás categorias
        /// </summary>
        public ICollection<Artigos> ListaArtigos { get; set; }
    }
}

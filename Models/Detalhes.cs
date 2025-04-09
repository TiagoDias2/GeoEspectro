﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GeoEspectro.Models
{
    public class Detalhes
    {
        /// <summary>
        /// Detalhe Multimedia Princapal
        /// </summary>
        public bool principal { get; set; }

        /// <summary>
        /// ForeignKey para referenciar o artigos ao recuso
        /// </summary>
        [ForeignKey(nameof(Recurso))]
        public int RecursoFK { get; set; }

        /// <summary>
        /// ForeignKey para referenciar o artigos ao recuso
        /// </summary>
        public Recursos Recurso { get; set; }

        /// <summary>
        /// ForeignKey para referenciar o recurso ao artigo
        /// </summary>
        [ForeignKey(nameof(Artigo))]
        public int ArtigoFK { get; set; }

        /// <summary>
        /// ForeignKey para referenciar o recurso ao artigo
        /// </summary>
        public Artigos Artigo { get; set; }

    }
}

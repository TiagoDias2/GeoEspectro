namespace GeoEspectro.Models
{
    /// <summary>
    /// Utilizadores não anonimos da aplicação 
    /// </summary>
    public class Utilizadores
    {

        /// <summary>
        /// Identificador da class do utilizador
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Nome do Utilizador
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Morada do Utilizador
        /// </summary>
        public string Morada { get; set; }

        /// <summary>
        /// Código Postal do Utilizador
        /// </summary>
        public string CodPostal { get; set; }

        /// <summary>
        /// País do Utilizador
        /// </summary>
        public string Pais { get; set; }

        /// <summary>
        /// Numero de Identificação Fiscal do Utilizador
        /// </summary>
        public string Nif { get; set; }

        /// <summary>
        /// Número do dispositivo protátil do Utilizador
        /// </summary>
        public string Telemovel { get; set; }

        // Relacionamentos M - N

        /// <summary>
        /// Lista dos artigos do Utilizador
        /// </summary>
        public ICollection<Gostos> ListaGostos { get; set; }

        /// <summary>
        /// Lista dos Recursos Multimédia do Utilizador
        /// </summary>
        public ICollection<Recursos> ListaRecursos { get; set; }

        /// <summary>
        /// Lista de Artigos Associados a Utilizadores
        /// </summary>
        public ICollection<Artigos> ListaArtigos { get; set;}
    }
}

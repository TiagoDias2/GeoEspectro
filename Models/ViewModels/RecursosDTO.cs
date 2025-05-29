namespace GeoEspectro.Models.ViewModels
{
    /// <summary>
    /// Dados de um recurso multimédia para serem usados na API
    /// </summary>
    public class RecursosDTO
    {
        /// <summary>
        /// Nome do Recurso
        /// </summary>
        public string Nome { get; set; } = "";

        /// <summary>
        /// Data de Publicação do Recurso
        /// </summary>
        public DateTime Data { get; set; }
        
        /// <summary>
        /// Tipo de Recurso Multimédia
        /// </summary>
        public string Tipo { get; set; }
        
        /// <summary>
        /// Local retratado pelo Recurso
        /// </summary>
        public string Local { get; set; }

        /// <summary>
        /// Observações para os Recursos Multimédia
        /// </summary>
        public string? Observacao { get; set; }
    }
}

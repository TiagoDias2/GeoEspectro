namespace GeoEspectro.Models.ViewModels
{
    /// <summary>
    /// Dados de um recurso multimédia para serem usados na API
    /// </summary>
    public class RecursosbyUserDTO : RecursosDTO
    {
        /// <summary>
        /// Nome do Dono de um Recurso
        /// </summary>
        public string DonoRecurso { get; set; } = "";
    }
}

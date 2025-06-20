using Microsoft.AspNetCore.Mvc.Rendering;

namespace GeoEspectro.Models.ViewModels
{
    public class ArtigosIndexDTO
    {
        public List<Artigos> Artigos { get; set; } = new();
        public List<SelectListItem> Categorias { get; set; } = new();

        public string? SearchString { get; set; }
        public string? CategoriaId { get; set; }
    }
}

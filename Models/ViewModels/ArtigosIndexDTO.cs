using Microsoft.AspNetCore.Mvc.Rendering;

namespace GeoEspectro.Models.ViewModels
{
    public class ArtigosIndexDTO
    {
        public string SearchString { get; set; }
        public string CategoriaId { get; set; }

        public List<Artigos> Artigos { get; set; }
        public SelectList Categorias { get; set; }
    }
}

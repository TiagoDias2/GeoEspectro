using GeoEspectro.Data.Migrations;
using Microsoft.EntityFrameworkCore;

namespace GeoEspectro.Models
{
    [PrimaryKey(nameof(ArtigosId), nameof(CategoriaId))]
    public class ArtigosCategoria
    {
        public int ArtigosId { get; set; }
        public Artigos Artigos { get; set; }

        public int CategoriaId { get; set; }
        public Categorias Categoria { get; set; }
    }
}

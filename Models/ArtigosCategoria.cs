using GeoEspectro.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoEspectro.Models
{
    [PrimaryKey(nameof(ArtigosId), nameof(CategoriaId))]
    public class ArtigosCategoria
    {
        [ForeignKey(nameof(Artigos))]
        public int ArtigosId { get; set; }
        public Artigos Artigos { get; set; }

        [ForeignKey(nameof(Categoria))]
        public int CategoriaId { get; set; }
        public Categorias Categoria { get; set; }
    }
}

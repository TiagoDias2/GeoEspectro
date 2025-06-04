using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GeoEspectro.Models.ViewModels
{
    public class ArtigoDTO
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(120)]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = "";

        [Required]
        public string Texto { get; set; } = "";

        [Display(Name = "Utilizador")]
        [Required(ErrorMessage = "Tem de escolher um utilizador.")]
        public int UtilizadorFK { get; set; }

        [Display(Name = "Categorias")]
        [Required(ErrorMessage = "Tem de escolher pelo menos uma categoria.")]
        public List<int> ListaCategoriasSelecionadas { get; set; } = [];

        public MultiSelectList? ListaCategorias { get; set; }

        public SelectList? ListaUtilizadores { get; set; }
    }
}

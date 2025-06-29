#nullable enable

using System.ComponentModel.DataAnnotations;

namespace GeoEspectro.Models.ViewModels
{
    public class UtilizadorEditDTO
    {
        public int ID { get; set; }

        [Display(Name = "Nome")]
        [StringLength(50)]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Morada")]
        [StringLength(50)]
        public string Morada { get; set; }

        [Display(Name = "Código Postal")]
        [StringLength(50)]
        [RegularExpression("[1-9][0-9]{3}-[0-9]{3} [A-Za-z ]+",
            ErrorMessage = "No {0} só são aceites algarismos e letras inglesas.")]
        public string CodPostal { get; set; }

        [Display(Name = "País")]
        [StringLength(50)]
        public string Pais { get; set; }

        [Display(Name = "NIF")]
        [StringLength(9)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "O {0} deve conter exatamente 9 dígitos.")]
        public string Nif { get; set; }

        [Display(Name = "Telemóvel")]
        [StringLength(18)]
        [RegularExpression(@"9[1236][0-9]{7}", ErrorMessage = "O número de telemóvel deve ser válido.")]
        public string Telemovel { get; set; }

        // Propriedade para o checkbox de administrador
        [Display(Name = "É Administrador?")]
        public bool IsAdmin { get; set; }
    }
}
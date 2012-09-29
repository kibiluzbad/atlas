using System.ComponentModel.DataAnnotations;
using Atlas.UI.Domain;

namespace Atlas.UI.Models
{
    public class TelefoneViewModel
    {
        [Required(ErrorMessage = "Infome o número.")]
        [RegularExpression("\\d*", ErrorMessage = "Apenas digitos.")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Informe a operadora.")]
        public Operadora Operadora { get; set; }
    }
}
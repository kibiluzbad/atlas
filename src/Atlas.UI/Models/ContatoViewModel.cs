using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Atlas.UI.Models
{
    public class ContatoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do contato")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o apelido do contato")]
        public string Apelido { get; set; }
        public IEnumerable<TelefoneViewModel> Telefones { get; set; }
    }
}
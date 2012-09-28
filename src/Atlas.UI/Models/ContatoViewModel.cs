using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atlas.UI.Models
{
    public class ContatoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public IEnumerable<TelefoneViewModel> Telefones { get; set; }
    }
}
using System.Collections.Generic;

namespace Atlas.UI.Domain
{
    public class Contato
    {
        private readonly ICollection<Telefone> _telefones;

        public virtual string Nome { get; set; }

        public virtual string Apelido { get; set; }

        public virtual IEnumerable<Telefone> Telefones
        {
            get { return _telefones; }
        }

        public Contato()
        {
            _telefones = new HashSet<Telefone>();
        }

        public virtual void IncluiTelefone(string numero, Operadora operadora)
        {
            _telefones.Add(new Telefone(numero, operadora));
        }
    }
}
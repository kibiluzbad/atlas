using System.Collections.Generic;

namespace Atlas.UI.Domain
{
    public class Contato
    {
        private readonly ICollection<Telefone> _telefones;

        public int Id { get; protected set; }

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
            IncluiTelefone(new Telefone(numero, operadora));
        }

        public virtual void IncluiTelefone(Telefone telefone)
        {
            _telefones.Add(telefone);
        }
    }
}
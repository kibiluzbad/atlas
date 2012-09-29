using System.Collections.Generic;
using Newtonsoft.Json;

namespace Atlas.UI.Domain
{
    public class Contato
    {
        public int Id { get; protected set; }

        public virtual string Nome { get; set; }

        public virtual string Apelido { get; set; }

        [JsonProperty]
        public virtual ICollection<Telefone> Telefones { get;  protected set; }

        public Contato()
        {
            Telefones = new HashSet<Telefone>();
        }

        public virtual void IncluiTelefone(string numero, Operadora operadora)
        {
            IncluiTelefone(new Telefone(numero, operadora));
        }

        public virtual void IncluiTelefone(Telefone telefone)
        {
            Telefones.Add(telefone);
        }
    }
}
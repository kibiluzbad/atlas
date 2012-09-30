using System.Linq;
using Atlas.UI.Domain;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Atlas.UI.Infra
{
    public class Contato_Search : AbstractIndexCreationTask<Contato, Contato_Search.ContatoSearch>
    {
        public Contato_Search()
        {
            Map = contatos => from c in contatos
                              select new
                                  {
                                      c.Id,
                                      c.Nome,
                                      c.Apelido,
                                      Query = new[] {c.Nome, c.Apelido}.Concat(c.Telefones.Select(t => t.Numero))
                                  };

            Indexes.Add(c => c.Query, FieldIndexing.Analyzed);
        }

        public class ContatoSearch
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Apelido { get; set; }
            public string Query { get; set; }
        }
    }
}
using System.Linq;
using System.Reflection;
using Atlas.UI.Domain;
using Autofac;
using Autofac.Integration.Mvc;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Module = Autofac.Module;

namespace Atlas.UI.Infra
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
                {
                    var store =new EmbeddableDocumentStore
                        {
                            DataDirectory = "Data",
                            UseEmbeddedHttpServer = true
                        }.Initialize();

                    IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(),store);

                    return store;
                })
                .As<IDocumentStore>()
                .SingleInstance();

            builder.Register(c => c.Resolve<IDocumentStore>().OpenSession())
                .As<IDocumentSession>()
                .InstancePerHttpRequest();
        }
    }

    public class Contato_Search : AbstractIndexCreationTask<Contato,Contato_Search.ContatoSearch>
    {
        public Contato_Search()
        {
            Map = contatos => from c in contatos
                              select new
                                  {
                                      c.Id,
                                      c.Nome,
                                      c.Apelido,
                                      Query = new[] {c.Nome, c.Apelido}.Concat(c.Telefones.Select(t=>t.Numero))
                                  };
            
            Indexes.Add(c=>c.Query,FieldIndexing.Analyzed);
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
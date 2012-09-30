using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
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
                    var store = new EmbeddableDocumentStore
                        {
                            DataDirectory = "Data",
                            UseEmbeddedHttpServer = true
                        };

                    store.Initialize();

                    IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), store);

                    return store;
                })
                .As<IDocumentStore>()
                .SingleInstance();

            builder.Register(c => c.Resolve<IDocumentStore>().OpenSession())
                .As<IDocumentSession>()
                .InstancePerHttpRequest();
        }
    }
}
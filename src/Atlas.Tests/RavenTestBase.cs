using NUnit.Framework;
using Raven.Client;
using Raven.Client.Embedded;

namespace Atlas.Tests
{
    public abstract class RavenTestBase
    {
        protected IDocumentStore Store;
        protected IDocumentSession Session;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            FixtureSetupFunc();
        }

        [SetUp]
        public void Setup()
        {
            SetupFunc();
        }

        [TearDown]
        public void TearDown()
        {
            TearDownFunc();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            TestFixtureTearDownFunc();
        }

        protected virtual void FixtureSetupFunc()
        {
            Store = new EmbeddableDocumentStore
                        {
                            RunInMemory = true
                        }.Initialize();
        }

        protected virtual void SetupFunc()
        {
            Session = Store.OpenSession();
        }

        protected virtual void TearDownFunc()
        {
            Session.Dispose();
        }

        protected virtual void TestFixtureTearDownFunc()
        {
            Store.Dispose();
        }
    }
}
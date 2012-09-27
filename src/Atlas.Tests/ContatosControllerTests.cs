using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Atlas.UI.Controllers;
using Moq;
using NUnit.Framework;
using Raven.Client;

namespace Atlas.Tests
{
    [TestFixture]
    public class ContatosControllerTests
    {
        [Test]
        public void Devo_receber_uma_view_index_ao_chamar_a_acao_index_do_controller()
        {
            var fakeDocumentSession = new Mock<IDocumentSession>();

            var controller = new ContatosController(fakeDocumentSession.Object);

            var result = controller.Index() as ViewResult;

            Assert.That(result.ViewName,
                Is.EqualTo("Index"));
        }
    }
}

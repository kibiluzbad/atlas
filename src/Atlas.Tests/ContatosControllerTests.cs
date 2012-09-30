using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Atlas.UI;
using Atlas.UI.Controllers;
using Atlas.UI.Domain;
using Atlas.UI.Infra;
using Atlas.UI.Models;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Raven.Client.Indexes;
using Raven.Client.Linq;

namespace Atlas.Tests
{
    [TestFixture]
    public class ContatosControllerTests : RavenTestBase
    {
        [Test]
        public void Devo_receber_uma_view_index_ao_chamar_a_acao_index_do_controller()
        {
            var controller = new ContatosController(Session);

            var result = controller.Index() as ViewResult;

            Assert.That(result.ViewName,
                Is.EqualTo("Index"));
        }

        [Test]
        public void Devo_receber_uma_view_de_criacao_de_contatos_ao_chamar_a_acao_New()
        {
            var controller = new ContatosController(Session);

            var result = controller.New() as ViewResult;

            Assert.That(result.ViewName,
                Is.EqualTo("New"));
        }

        [Test]
        public void Posso_inserir_novos_contatos()
        {
            var controller = new ContatosController(Session);

            Mapper.AddProfile<ContatoProfile>();

            var contato = new ContatoViewModel
                              {
                                  Apelido = "Apelido",
                                  Nome = "Nome"
                              };

            var result = controller.Create(contato);

            Assert.That(result,
                Is.InstanceOf<RedirectToRouteResult>());
        }

        [Test]
        public void Devo_alterar_o_contato_ao_efetuar_um_Put_para_contatos_com_id_do_contato()
        {
            var controller = new ContatosController(Session);

            Mapper.AddProfile<ContatoProfile>();

            var contato = new Contato
            {
                Apelido = "Apelido",
                Nome = "Nome"
            };

            Session.Store(contato);
            Session.SaveChanges();

            var viewModel = new ContatoViewModel
                                {
                                    Apelido = "Apelido1", 
                                    Nome = "Nome1"
                                };


            controller.Update(contato.Id, viewModel);

            Session.SaveChanges();

            Assert.That(Session.Load<Contato>(contato.Id).Nome,
                Is.EqualTo(viewModel.Nome));
        }

        [Test]
        public void Posso_remover_um_contato()
        {
            var controller = new ContatosController(Session);

            Mapper.AddProfile<ContatoProfile>();

            var contato = new Contato
            {
                Apelido = "Apelido",
                Nome = "Nome"
            };

            Session.Store(contato);
            Session.SaveChanges();

            controller.Delete(contato.Id);

            Session.SaveChanges();

            Assert.That(Session.Load<Contato>(contato.Id),
                Is.Null);
        }

        [Test]
        public void Posso_incluir_telefones_em_um_contato()
        {
            var controller = new ContatosController(Session);

            Mapper.AddProfile<ContatoProfile>();
            Mapper.AddProfile<TelefoneProfile>();

            var contato = new Contato
            {
                Apelido = "Apelido",
                Nome = "Nome"
            };

            Session.Store(contato);
            Session.SaveChanges();

            var viewModel = new TelefoneViewModel
                                {
                                    Numero = "011981234567", 
                                    Operadora = Operadora.Tim
                                };

            controller.AddPhone(contato.Id, viewModel);

            Session.SaveChanges();

            Assert.That(Session.Load<Contato>(contato.Id).Telefones.Count(),
                Is.EqualTo(1));
        }


        [Test]
        public void Posso_editar_os_dados_do_contato()
        {
            var controller = new ContatosController(Session);

            Mapper.AddProfile<ContatoProfile>();
            Mapper.AddProfile<TelefoneProfile>();

            var contato = new Contato
            {
                Apelido = "Apelido",
                Nome = "Nome"
            };

            contato.IncluiTelefone("011981234567",Operadora.Tim);

            Session.Store(contato);
            Session.SaveChanges();
           
            var result = controller.Edit(contato.Id) as ViewResult;

            Assert.That(result.ViewName,
                Is.EqualTo("Edit"));
        }

        [Test]
        public void Devo_exibir_a_view_Index_ao_pesquisar_contatos()
        {
            var controller = new ContatosController(Session);

            Mapper.AddProfile<ContatoProfile>();
            Mapper.AddProfile<TelefoneProfile>();

            var contato = new Contato
            {
                Apelido = "Apelido",
                Nome = "Nome"
            };

            contato.IncluiTelefone("011981234567", Operadora.Tim);

            Session.Store(contato);
            Session.SaveChanges();

            var result = controller.Index("Nome") as ViewResult;

            Assert.That(result.ViewName,
                Is.EqualTo("Index"));
        }
    }

    
}

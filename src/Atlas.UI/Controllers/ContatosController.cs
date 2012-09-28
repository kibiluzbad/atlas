using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Atlas.UI.Domain;
using Atlas.UI.Infra;
using Atlas.UI.Models;
using AutoMapper;
using Raven.Client;

namespace Atlas.UI.Controllers
{
    public class ContatosController : RavenController
    {
        public ContatosController(IDocumentSession documentSession) 
            : base(documentSession)
        { }

        //
        // GET: /Contatos/
        [HttpGet]
        public ActionResult Index(int page = 1, int size = 20)
        {
            var contatos = DocumentSession.Query<Contato>()
                .Skip((page - 1)*size)
                .Take(size)
                .ToList();

            return View("Index",
                        Mapper.Map<IEnumerable<Contato>, IEnumerable<ContatoViewModel>>(contatos));
        }

        //
        // POST: /Contatos/
        [HttpPost]
        public ActionResult Create(ContatoViewModel contatoViewModel)
        {
            var contato = Mapper.Map<ContatoViewModel, Contato>(contatoViewModel);

            DocumentSession.Store(contato);

            TempData["success"] = "Contato incluido com sucesso";

            return View("Index");
        }

        //
        // GET: /Contato/New
        [HttpGet]
        public ActionResult New()
        {
            return View("New");
        }

        //
        // GET: /Contato/[id]
        [HttpGet]
        public ActionResult Show(int id)
        {
            var contato = DocumentSession.Load<Contato>(id);

            return View("Show",Mapper.Map<Contato,ContatoViewModel>(contato));
        }

        //
        // PUT: /Contato/[id]
        [HttpPut]
        public ActionResult Update(int id, ContatoViewModel viewModel)
        {
            var contato = DocumentSession.Load<Contato>(id);

            contato.Apelido = viewModel.Apelido;
            contato.Nome = viewModel.Nome;

            TempData["success"] = "Contato atualizado com sucesso";

            return View("Index");
        }

        //
        // DELETE: /Contato/[id]
        [HttpPut]
        public ActionResult Delete(int id)
        {
            var contato = DocumentSession.Load<Contato>(id);
            DocumentSession.Delete(contato);

            TempData["success"] = "Contato deletado com sucesso";

            return View("Index");
        }

        //
        // PUT: /Contato/[id]/AddPhone
        [HttpPut]
        public ActionResult AddPhone(int id, TelefoneViewModel viewModel)
        {
            var contato = DocumentSession.Load<Contato>(id);
            var telefone = Mapper.Map<Telefone>(viewModel);

            contato.IncluiTelefone(telefone);

            TempData["success"] = string.Format("Telefone {0} adicionado.",telefone);

            return View("Show",Mapper.Map<Contato,ContatoViewModel>(contato));
        }
    }
}

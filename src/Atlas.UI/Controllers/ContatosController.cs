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
using Raven.Client.Linq;

namespace Atlas.UI.Controllers
{
    public class ContatosController : RavenController
    {
        public ContatosController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }

        //
        // GET: /
        [HttpGet]
        public ActionResult Index(string term = null, int page = 1, int size = 20)
        {
            RavenQueryStatistics stats;
            IRavenQueryable<Contato_Search.ContatoSearch> query = null;

            if (!string.IsNullOrEmpty(term))
                query = DocumentSession.Query<Contato_Search.ContatoSearch, Contato_Search>()
                .Statistics(out stats)
                    .Search(c => c.Query,string.Format("*{0}*",term), escapeQueryOptions: EscapeQueryOptions.AllowAllWildcards);
            else
                query = DocumentSession.Query<Contato_Search.ContatoSearch, Contato_Search>()
                    .Statistics(out stats);
             
                var contatos = query
                .Skip((page - 1)*size)
                .Take(size)
                .OrderBy(c=>c.Nome)
                .As<Contato>()
                .ToList();

            ViewBag.Term = term ?? string.Empty;

            var result = new PagedResultViewModel<ContatoViewModel>
                {
                    Page = page,
                    Size = size,
                    Result = Mapper.Map<IEnumerable<Contato>, IEnumerable<ContatoViewModel>>(contatos),
                    Count = stats.TotalResults
                };

            
            return RespondTo(normal: () => View("Index", result),
                             ajax: () => PartialView("_Contatos", result));

        }

        //
        // POST: /Contatos/
        [HttpPost]
        public ActionResult Create(ContatoViewModel contatoViewModel)
        {
            if (!ModelState.IsValid) return View("New", contatoViewModel);

            var contato = DocumentSession
               .Query<Contato_Search.ContatoSearch, Contato_Search>()
               .As<Contato>()
               .FirstOrDefault(c => c.Nome == contatoViewModel.Nome);

            if(null == contato)
            {
                contato = Mapper.Map<ContatoViewModel, Contato>(contatoViewModel);
                DocumentSession.Store(contato);
                TempData["success"] = "Contato incluido com sucesso";
            }
            else
            {
                TempData["error"] = string.Format("Já existe um contato com o nome {0}", contato.Nome);
            }

            return RedirectToAction("Edit", new {contato.Id});
         
        }

        //
        // GET: /Contatos/New
        [HttpGet]
        public ActionResult New()
        {
            return View("New");
        }

        //
        // GET: /Contatos/[id]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var contato = DocumentSession.Load<Contato>(id);

            return View("Edit", Mapper.Map<Contato, ContatoViewModel>(contato));
        }

        //
        // POST: /Contatos/[id]
        [HttpPost]
        public ActionResult Update(int id, ContatoViewModel viewModel)
        {
            var contato = DocumentSession
                .Query<Contato_Search.ContatoSearch, Contato_Search>()
                .As<Contato>()
                .FirstOrDefault(c => c.Nome == viewModel.Nome &&
                                     c.Id != viewModel.Id);

            if (null == contato)
            {
                contato = DocumentSession.Load<Contato>(id);

                contato.Apelido = viewModel.Apelido;
                contato.Nome = viewModel.Nome;

                TempData["success"] = "Contato atualizado com sucesso";

                return RedirectToAction("Index");
            }

            TempData["error"] = string.Format("Já existe um contato com o nome {0}", contato.Nome);

            return RedirectToAction("Edit", new {id = contato.Id});
        }

        //
        // DELETE: /Contatos/[id]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var contato = DocumentSession.Load<Contato>(id);
            DocumentSession.Delete(contato);

            TempData["success"] = "Contato deletado com sucesso";

            return null;
        }

        //
        // POST: /Contatos/[id]/AddPhone
        [HttpPost]
        public ActionResult AddPhone(int id, TelefoneViewModel viewModel)
        {
            if (!ModelState.IsValid) return RedirectToAction("Edit",new {id});

            var contato = DocumentSession.Load<Contato>(id);
            var telefone = Mapper.Map<Telefone>(viewModel);

            contato.IncluiTelefone(telefone);

            TempData["success"] = string.Format("Telefone {0} adicionado.", telefone);

            return View("Edit", Mapper.Map<Contato, ContatoViewModel>(contato));
        }

        //
        // POST: /Contatos/[id]/DeletePhone
        [HttpPost]
        public ActionResult DeletePhone(int id, TelefoneViewModel telefoneViewModel)
        {
            var telefone = Mapper.Map<TelefoneViewModel, Telefone>(telefoneViewModel);
            
            var contato = DocumentSession.Load<Contato>(id);

            contato.RemoveTelefone(telefone);

            TempData["success"] = string.Format("Telefone {0} removido.", telefone);

            return null;
        }

        //
        // GET: /Contatos/[id]/AddPhone
        [HttpGet]
        public ActionResult AddPhone(int id)
        {
            ViewBag.ContatoId = id;
            return PartialView("_AddPhone");
        }
    }
}

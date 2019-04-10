using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntidadesDAL;
using SMSAPP.Filtros;
using PagedList;

namespace SMSAPP.Controllers
{
    public class MensagemController : Controller
    {
        private SMSWHATSAPPModelos db = new SMSWHATSAPPModelos();

        // GET: Mensagem
        [CustomActionFilter]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeSortParm = sortOrder == "titular" ? "titular_desc" : "titular";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var mensagem = db.mensagem.Include(m => m.cliente);

            if (!String.IsNullOrEmpty(searchString))
            {
                mensagem = mensagem.Where(s => s.texto.Contains(searchString)
                                       || s.texto.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "titular_desc":
                    mensagem = mensagem.OrderByDescending(s => s.texto);
                    break;
                default:
                    mensagem = mensagem.OrderBy(c => c.texto);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(mensagem.ToPagedList(pageNumber, pageSize));
        }

        // GET: Mensagem/Details/5
        [CustomActionFilter]
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mensagem mensagem = await db.mensagem.FindAsync(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            return View(mensagem);
        }

        // GET: Mensagem/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaoSocial");
            return View();
        }

        // POST: Mensagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Create([Bind(Include = "idMensagem,texto,dtCriacao,dtAltercao,ativo,idCliente")] mensagem mensagem)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (ModelState.IsValid)
            {
                db.mensagem.Add(mensagem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaoSocial", mensagem.idCliente);
            return View(mensagem);
        }

        // GET: Mensagem/Edit/5
        [CustomActionFilter]
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mensagem mensagem = await db.mensagem.FindAsync(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaoSocial", mensagem.idCliente);
            mensagem.dtAltercao = DateTime.Now;
            return View(mensagem);
        }

        // POST: Mensagem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Edit([Bind(Include = "idMensagem,texto,dtCriacao,dtAltercao,ativo,idCliente")] mensagem mensagem)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (ModelState.IsValid)
            {
                db.Entry(mensagem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaoSocial", mensagem.idCliente);
            return View(mensagem);
        }

        // GET: Mensagem/Delete/5
        [CustomActionFilter]
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mensagem mensagem = await db.mensagem.FindAsync(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            return View(mensagem);
        }

        // POST: Mensagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            mensagem mensagem = await db.mensagem.FindAsync(id);
            db.mensagem.Remove(mensagem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [CustomActionFilter]
        public ActionResult GeraPDF()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            var arqpdf = new Rotativa.ViewAsPdf
            {
                ViewName = "_PDFView",
                Model = db.mensagem,
                FileName = "Mensagem.pdf",
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = { Left = 5, Bottom = 10, Right = 5, Top = 10 },
                CustomSwitches = "--footer-right \"  Criado em: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Página: [page]/[toPage]\"" + " --footer-line --footer-font-size \"10\" --footer-spacing 1 --footer-font-name \"Segoe UI\""
            };
            return arqpdf;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

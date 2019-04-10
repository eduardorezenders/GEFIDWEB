using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntidadesSOL;
using SMSAPP.Filtros;
using PagedList;

namespace SMSAPP.Controllers
{
    public class TipoPrioridadeController : Controller
    {
        private SOLModelos db = new SOLModelos();

        // GET: TipoPrioridade
        [CustomActionFilter]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(db.tipoPrioridade.OrderBy(c => c.idtTipoPrioridade).ToPagedList(pageNumber, pageSize));
        }

        [CustomActionFilter]
        public ActionResult GeraPDF()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            var arqpdf = new Rotativa.ViewAsPdf
            {
                ViewName = "_PDFView",
                Model = db.tipoPrioridade,
                FileName = "TipoPrioridade.pdf",
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = { Left = 5, Bottom = 10, Right = 5, Top = 10 },
                CustomSwitches = "--footer-center \"  Criado em: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Página: [page]/[toPage]\"" + " --footer-line --footer-font-size \"10\" --footer-spacing 1 --footer-font-name \"Segoe UI\""
            };
            return arqpdf;
        }

        // GET: TipoPrioridade/Details/5
        [CustomActionFilter]
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipoPrioridade tipoPrioridade = await db.tipoPrioridade.FindAsync(id);
            if (tipoPrioridade == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrioridade);
        }

        // GET: TipoPrioridade/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            return View();
        }

        // POST: TipoPrioridade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Create([Bind(Include = "idtTipoPrioridade,descricao,ativo")] tipoPrioridade tipoPrioridade)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (ModelState.IsValid)
            {
                db.tipoPrioridade.Add(tipoPrioridade);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoPrioridade);
        }

        // GET: TipoPrioridade/Edit/5
        [CustomActionFilter]
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipoPrioridade tipoPrioridade = await db.tipoPrioridade.FindAsync(id);
            if (tipoPrioridade == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrioridade);
        }

        // POST: TipoPrioridade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Edit([Bind(Include = "idtTipoPrioridade,descricao,ativo")] tipoPrioridade tipoPrioridade)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (ModelState.IsValid)
            {
                db.Entry(tipoPrioridade).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoPrioridade);
        }

        // GET: TipoPrioridade/Delete/5
        [CustomActionFilter]
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipoPrioridade tipoPrioridade = await db.tipoPrioridade.FindAsync(id);
            if (tipoPrioridade == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrioridade);
        }

        // POST: TipoPrioridade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            tipoPrioridade tipoPrioridade = await db.tipoPrioridade.FindAsync(id);
            db.tipoPrioridade.Remove(tipoPrioridade);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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

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
    public class TipoContatoController : Controller
    {
        private SOLModelos db = new SOLModelos();

        // GET: TipoContato
        [CustomActionFilter]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(db.tipoContato.OrderBy(c => c.idTipoContato).ToPagedList(pageNumber, pageSize));
        }

        [CustomActionFilter]
        public ActionResult GeraPDF()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            var arqpdf = new Rotativa.ViewAsPdf
            {
                ViewName = "_PDFView",
                Model = db.tipoContato,
                FileName = "TipoContato.pdf",
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = { Left = 5, Bottom = 10, Right = 5, Top = 10 },
                CustomSwitches = "--footer-center \"  Criado em: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Página: [page]/[toPage]\"" + " --footer-line --footer-font-size \"10\" --footer-spacing 1 --footer-font-name \"Segoe UI\""
            };
            return arqpdf;
        }

        // GET: TipoContato/Details/5
        [CustomActionFilter]
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipoContato tipoContato = await db.tipoContato.FindAsync(id);
            if (tipoContato == null)
            {
                return HttpNotFound();
            }
            return View(tipoContato);
        }

        // GET: TipoContato/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            return View();
        }

        // POST: TipoContato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Create([Bind(Include = "idTipoContato,nome,mostrarSolicitacao,orgaoPublico")] tipoContato tipoContato)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (ModelState.IsValid)
            {
                db.tipoContato.Add(tipoContato);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoContato);
        }

        // GET: TipoContato/Edit/5
        [CustomActionFilter]
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipoContato tipoContato = await db.tipoContato.FindAsync(id);
            if (tipoContato == null)
            {
                return HttpNotFound();
            }
            return View(tipoContato);
        }

        // POST: TipoContato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Edit([Bind(Include = "idTipoContato,nome,mostrarSolicitacao,orgaoPublico")] tipoContato tipoContato)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (ModelState.IsValid)
            {
                db.Entry(tipoContato).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoContato);
        }

        // GET: TipoContato/Delete/5
        [CustomActionFilter]
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipoContato tipoContato = await db.tipoContato.FindAsync(id);
            if (tipoContato == null)
            {
                return HttpNotFound();
            }
            return View(tipoContato);
        }

        // POST: TipoContato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            tipoContato tipoContato = await db.tipoContato.FindAsync(id);
            db.tipoContato.Remove(tipoContato);
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

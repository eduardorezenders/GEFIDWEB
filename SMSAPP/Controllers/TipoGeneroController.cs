using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using EntidadesDAL;
using System;
using SMSAPP.Filtros;
using System.Linq;
using PagedList;

namespace SMSAPP.Controllers
{
    public class TipoGeneroController : Controller
    {
        private SMSWHATSAPPModelos db = new SMSWHATSAPPModelos();

        // GET: TipoGenero
        [CustomActionFilter]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(db.tipoGenero.OrderBy(c => c.idGenero).ToPagedList(pageNumber, pageSize));
        }

        [CustomActionFilter]
        public ActionResult GeraPDF()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            var arqpdf = new Rotativa.ViewAsPdf
            {
                ViewName = "_PDFView",
                Model = db.tipoGenero,
                FileName = "TipoGenero.pdf",
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = { Left = 5, Bottom = 10, Right = 5, Top = 10 },
                CustomSwitches = "--footer-center \"  Criado em: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Página: [page]/[toPage]\"" + " --footer-line --footer-font-size \"10\" --footer-spacing 1 --footer-font-name \"Segoe UI\""
            };
            return arqpdf;
        }

        // GET: TipoGenero/Details/5
        [CustomActionFilter]
        public async Task<ActionResult> Details(string id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipoGenero tipoGenero = await db.tipoGenero.FindAsync(id);
            if (tipoGenero == null)
            {
                return HttpNotFound();
            }
            return View(tipoGenero);
        }

        // GET: TipoGenero/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            return View();
        }

        // POST: TipoGenero/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Create([Bind(Include = "idGenero,descricao,ativo")] tipoGenero tipoGenero)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (ModelState.IsValid)
            {
                db.tipoGenero.Add(tipoGenero);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoGenero);
        }

        // GET: TipoGenero/Edit/5
        [CustomActionFilter]
        public async Task<ActionResult> Edit(string id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipoGenero tipoGenero = await db.tipoGenero.FindAsync(id);
            if (tipoGenero == null)
            {
                return HttpNotFound();
            }
            return View(tipoGenero);
        }

        // POST: TipoGenero/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Edit([Bind(Include = "idGenero,descricao,ativo")] tipoGenero tipoGenero)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (ModelState.IsValid)
            {
                db.Entry(tipoGenero).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoGenero);
        }

        // GET: TipoGenero/Delete/5
        [CustomActionFilter]
        public async Task<ActionResult> Delete(string id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipoGenero tipoGenero = await db.tipoGenero.FindAsync(id);
            if (tipoGenero == null)
            {
                return HttpNotFound();
            }
            return View(tipoGenero);
        }

        // POST: TipoGenero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            tipoGenero tipoGenero = await db.tipoGenero.FindAsync(id);
            db.tipoGenero.Remove(tipoGenero);
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

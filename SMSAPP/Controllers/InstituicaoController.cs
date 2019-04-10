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
    public class InstituicaoController : Controller
    {
        private SMSWHATSAPPModelos db = new SMSWHATSAPPModelos();

        // GET: Instituicao
        [CustomActionFilter]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitularSortParm = sortOrder == "titular" ? "titular_desc" : "titular";
            ViewBag.DataSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var cliente = from s in db.cliente select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                cliente = cliente.Where(s => s.razaoSocial.Contains(searchString)
                                       || s.nomeTitular.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "titular_desc":
                    cliente = cliente.OrderByDescending(s => s.nomeTitular);
                    break;
                case "titular":
                    cliente = cliente.OrderBy(s => s.nomeTitular);
                    break;
                case "Date":
                    cliente = cliente.OrderBy(s => s.dtCadastro);
                    break;
                case "date_desc":
                    cliente = cliente.OrderByDescending(s => s.dtCadastro);
                    break;
                default:
                    cliente = cliente.OrderBy(c => c.razaoSocial);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(cliente.ToPagedList(pageNumber, pageSize));
        }

        // GET: Instituicao/Details/5
        [CustomActionFilter]
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = await db.cliente.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Instituicao/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            var cliente = new cliente();
            cliente.dtCadastro = DateTime.Now;
            return View(cliente);
        }

        // POST: Instituicao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Create([Bind(Include = "razaoSocial,nomeTitular,login,dtCadastro,cpf,cnpj,ativo,chaveapi")] cliente cliente)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (ModelState.IsValid)
            {
                db.cliente.Add(cliente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Instituicao/Edit/5
        [CustomActionFilter]
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = await db.cliente.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Instituicao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Edit([Bind(Include = "idCliente,razaoSocial,nomeTitular,login,dtCadastro,cpf,cnpj,ativo,chaveapi")] cliente cliente)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Instituicao/Delete/5
        [CustomActionFilter]
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = await db.cliente.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Instituicao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            cliente cliente = await db.cliente.FindAsync(id);
            db.cliente.Remove(cliente);
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
                Model = db.cliente,
                FileName = "Instituicao.pdf",
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

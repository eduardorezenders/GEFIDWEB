using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using EntidadesDAL;
using SMSAPP.Filtros;

namespace SMSAPP.Controllers
{
    public class AcessoGrupoController : Controller
    {
        private SMSWHATSAPPModelos db = new SMSWHATSAPPModelos();

        public int CODCLI => Convert.ToInt32(Session["ULIDC"]);

        // GET: AcessoGrupos
        [CustomActionFilter]
        public async Task<ActionResult> Index()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            var acessoGrupoes = db.acessoGrupo.Where(g=>g.idCliente==CODCLI);
            return View(await acessoGrupoes.ToListAsync());
        }

        // GET: AcessoGrupos/Details/5
        [CustomActionFilter]
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acessoGrupo acessoGrupo = await db.acessoGrupo.FindAsync(id);
            if (acessoGrupo == null)
            {
                return HttpNotFound();
            }
            return View(acessoGrupo);
        }

        // GET: AcessoGrupos/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            return View();
        }

        // POST: AcessoGrupos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Create([Bind(Include = "idCliente,IdGrupo,nome,idInstituicao,deletar,ativo")] acessoGrupo acessoGrupo)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (ModelState.IsValid)
            {
                db.acessoGrupo.Add(acessoGrupo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(acessoGrupo);
        }

        // GET: AcessoGrupos/Edit/5
        [CustomActionFilter]
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acessoGrupo acessoGrupo = await db.acessoGrupo.FindAsync(id);
            if (acessoGrupo == null)
            {
                return HttpNotFound();
            }
            return View(acessoGrupo);
        }

        // POST: AcessoGrupos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Edit([Bind(Include = "idCliente,IdGrupo,nome,idInstituicao,deletar,ativo")] acessoGrupo acessoGrupo)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (ModelState.IsValid)
            {
                db.Entry(acessoGrupo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(acessoGrupo);
        }

        // GET: AcessoGrupos/Delete/5
        [CustomActionFilter]
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acessoGrupo acessoGrupo = await db.acessoGrupo.FindAsync(id);
            if (acessoGrupo == null)
            {
                return HttpNotFound();
            }
            return View(acessoGrupo);
        }

        // POST: AcessoGrupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            acessoGrupo acessoGrupo = await db.acessoGrupo.FindAsync(id);
            db.acessoGrupo.Remove(acessoGrupo);
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

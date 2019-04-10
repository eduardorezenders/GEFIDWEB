using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using EntidadesDAL;
using System.Linq;
using SMSAPP.Filtros;

namespace SMSAPP.Controllers
{
    public class AcessoPerfilController : Controller
    {
        private SMSWHATSAPPModelos db = new SMSWHATSAPPModelos();

        public int CODCLI => Convert.ToInt32(Session["ULIDC"]);

        // GET: AcessoPerfil
        [CustomActionFilter]
        public async Task<ActionResult> Index()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            var acessoPerfils = db.acessoPerfil.Where(g => g.idCliente == CODCLI);
            acessoPerfils = acessoPerfils.Include(a => a.acessoGrupo).Include(a => a.acessoModulo);
            return View(await acessoPerfils.ToListAsync());
        }

        // GET: AcessoPerfil/Details/5
        [CustomActionFilter]
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acessoPerfil acessoPerfil = await db.acessoPerfil.FindAsync(id);
            if (acessoPerfil == null)
            {
                return HttpNotFound();
            }
            return View(acessoPerfil);
        }

        // GET: AcessoPerfil/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            ViewBag.idGrupo = new SelectList(db.acessoGrupo.Where(g => g.idCliente == CODCLI), "IdGrupo", "nome");
            ViewBag.idModulo = new SelectList(db.acessoModulo, "idModulo", "descricao");
            return View();
        }

        // POST: AcessoPerfil/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Create([Bind(Include = "idCliente,idPerfil,idGrupo,idModulo,al,ap,cr,ex,le")] acessoPerfil acessoPerfil)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (ModelState.IsValid)
            {
                db.acessoPerfil.Add(acessoPerfil);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idGrupo = new SelectList(db.acessoGrupo.Where(g => g.idCliente == CODCLI), "IdGrupo", "nome", acessoPerfil.idGrupo);
            ViewBag.idModulo = new SelectList(db.acessoModulo, "idModulo", "descricao", acessoPerfil.idModulo);
            return View(acessoPerfil);
        }

        // GET: AcessoPerfil/Edit/5
        [CustomActionFilter]
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acessoPerfil acessoPerfil = await db.acessoPerfil.FindAsync(id);
            if (acessoPerfil == null)
            {
                return HttpNotFound();
            }
            ViewBag.idGrupo = new SelectList(db.acessoGrupo, "IdGrupo", "nome", acessoPerfil.idGrupo);
            ViewBag.idModulo = new SelectList(db.acessoModulo, "idModulo", "descricao", acessoPerfil.idModulo);
            return View(acessoPerfil);
        }

        // POST: AcessoPerfil/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Edit([Bind(Include = "idCliente,idPerfil,idGrupo,idModulo,al,ap,cr,ex,le")] acessoPerfil acessoPerfil)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (ModelState.IsValid)
            {
                db.Entry(acessoPerfil).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idGrupo = new SelectList(db.acessoGrupo, "IdGrupo", "nome", acessoPerfil.idGrupo);
            ViewBag.idModulo = new SelectList(db.acessoModulo, "idModulo", "descricao", acessoPerfil.idModulo);
            return View(acessoPerfil);
        }

        // GET: AcessoPerfil/Delete/5
        [CustomActionFilter]
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acessoPerfil acessoPerfil = await db.acessoPerfil.FindAsync(id);
            if (acessoPerfil == null)
            {
                return HttpNotFound();
            }
            return View(acessoPerfil);
        }

        // POST: AcessoPerfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            acessoPerfil acessoPerfil = await db.acessoPerfil.FindAsync(id);
            db.acessoPerfil.Remove(acessoPerfil);
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

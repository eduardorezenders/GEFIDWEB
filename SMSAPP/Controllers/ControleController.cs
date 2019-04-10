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

namespace SMSAPP.Controllers
{
    public class ControleController : Controller
    {
        private SMSWHATSAPPModelos db = new SMSWHATSAPPModelos();

        // GET: Controle
        [CustomActionFilter]
        public async Task<ActionResult> Index()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            var controle = db.controle.Include(c => c.cliente);
            return View(await controle.ToListAsync());
        }

        // GET: Controle/Details/5
        [CustomActionFilter]
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            controle controle = await db.controle.FindAsync(id);
            if (controle == null)
            {
                return HttpNotFound();
            }
            return View(controle);
        }

        // GET: Controle/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaoSocial");
            return View();
        }

        // POST: Controle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Create([Bind(Include = "idControle,idCliente,priSms,segSms,terSms,quaSms,quiSms,aCada,envioAuto")] controle controle)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (ModelState.IsValid)
            {
                db.controle.Add(controle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaoSocial", controle.idCliente);
            return View(controle);
        }

        // GET: Controle/Edit/5
        [CustomActionFilter]
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            controle controle = await db.controle.FindAsync(id);
            if (controle == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaoSocial", controle.idCliente);
            return View(controle);
        }

        // POST: Controle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Edit([Bind(Include = "idControle,idCliente,priSms,segSms,terSms,quaSms,quiSms,aCada,envioAuto")] controle controle)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (ModelState.IsValid)
            {
                db.Entry(controle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaoSocial", controle.idCliente);
            return View(controle);
        }

        // GET: Controle/Delete/5
        [CustomActionFilter]
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            controle controle = await db.controle.FindAsync(id);
            if (controle == null)
            {
                return HttpNotFound();
            }
            return View(controle);
        }

        // POST: Controle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            controle controle = await db.controle.FindAsync(id);
            db.controle.Remove(controle);
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

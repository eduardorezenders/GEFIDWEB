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

namespace SMSAPP.Controllers
{
    public class ContatoController : Controller
    {
        private SOLModelos db = new SOLModelos();

        // GET: Contato
        public async Task<ActionResult> Index()
        {
            var contato = db.contato.Include(c => c.tipoContato).Include(c => c.tratamentoPessoa);
            return View(await contato.ToListAsync());
        }

        // GET: Contato/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contato contato = await db.contato.FindAsync(id);
            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        // GET: Contato/Create
        public ActionResult Create()
        {
            ViewBag.idTipoContato = new SelectList(db.tipoContato, "idTipoContato", "nome");
            ViewBag.idTratamentoPessoa = new SelectList(db.tratamentoPessoa, "idTratamentoPessoa", "descricao");
            return View();
        }

        // POST: Contato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idContato,nomeContato,ocupacao,descricao,abreviacao,idTipoContato,idOperador,idEstruturaOrgao,idTratamentoPessoa")] contato contato)
        {
            if (ModelState.IsValid)
            {
                db.contato.Add(contato);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idTipoContato = new SelectList(db.tipoContato, "idTipoContato", "nome", contato.idTipoContato);
            ViewBag.idTratamentoPessoa = new SelectList(db.tratamentoPessoa, "idTratamentoPessoa", "descricao", contato.idTratamentoPessoa);
            return View(contato);
        }

        // GET: Contato/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contato contato = await db.contato.FindAsync(id);
            if (contato == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTipoContato = new SelectList(db.tipoContato, "idTipoContato", "nome", contato.idTipoContato);
            ViewBag.idTratamentoPessoa = new SelectList(db.tratamentoPessoa, "idTratamentoPessoa", "descricao", contato.idTratamentoPessoa);
            return View(contato);
        }

        // POST: Contato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idContato,nomeContato,ocupacao,descricao,abreviacao,idTipoContato,idOperador,idEstruturaOrgao,idTratamentoPessoa")] contato contato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contato).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idTipoContato = new SelectList(db.tipoContato, "idTipoContato", "nome", contato.idTipoContato);
            ViewBag.idTratamentoPessoa = new SelectList(db.tratamentoPessoa, "idTratamentoPessoa", "descricao", contato.idTratamentoPessoa);
            return View(contato);
        }

        // GET: Contato/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contato contato = await db.contato.FindAsync(id);
            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        // POST: Contato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            contato contato = await db.contato.FindAsync(id);
            db.contato.Remove(contato);
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

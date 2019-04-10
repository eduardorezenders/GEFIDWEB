using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using EntidadesDAL;
using SMSAPP.ViewModels;
using SMSAPP.Models;
using SMSAPP.Filtros;
using PagedList;
using System;

namespace SMSAPP.Controllers
{
    public class AcessoController : Controller
    {
        private SMSWHATSAPPModelos db = new SMSWHATSAPPModelos();

        public int CODCLI => Convert.ToInt32(Session["ULIDC"]);

        // GET: Acesso
        [CustomActionFilter]
        public async Task<ActionResult> Index()
        {
            if (Session["ULID"]==null) { return RedirectToAction("Login","Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!","Atenção"); }
            IQueryable<acesso> acessoes = db.acesso.Where(g => g.idCliente == CODCLI);
            acessoes = acessoes.Include(a => a.acessoGrupo);
            return View(await acessoes.ToListAsync());
        }

        [CustomActionFilter]
        public ActionResult Logs(int? page, string searchString, string currentFilter)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            IOrderedQueryable<actionsLog> logs = from s in db.actionsLog.Where(g => g.idCliente == CODCLI) orderby s.DateAndTime select s;
            if (!string.IsNullOrEmpty(searchString))
            {
                logs  = from s in logs.Where(s => s.Usuario.Contains(searchString)
                                       || s.cpf.Equals(searchString)) orderby s.DateAndTime select s;
            }
            int paginaTamanho = 20;
            int paginaNumero = (page ?? 1);
            return View(logs.ToPagedList(paginaNumero, paginaTamanho));
        }

        public ActionResult Licenca()
        {
            LicencaVM licenca = new LicencaVM();
            return View("Licenca",licenca);
        }

        [HttpPost]
        public ActionResult Licenca(LicencaVM licenca)
        {
            string valores = licenca.chave.ToString();
            string[] array = valores.Split('-');
            if (array.Count()<4 || array.Count() > 4)
            {
                return View("Licenca").Mensagem("Licença não confere. Por favor informe uma licença válida!!","Erro");
            }
            licenca li = db.licenca.Where(l=>l.idCliente==CODCLI).FirstOrDefault();
            string chave1 = li.chave1.ToString();
            string chave2 = li.chave2.ToString();
            if (Criptografia.CriptografiaHelper.Decriptar(chave1, chave2,array[3])!="N1ContactCenter")
            {
                return View("Licenca").Mensagem("Licença não confere. Por favor informe uma licença válida!!", "Erro");
            }

            li.chave = licenca.chave;
            db.Entry(li).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Login").Mensagem("Obrigado por registrar sua aplicação!!","Atenção");
        }

        public ActionResult Login()
        {
            AcessoVM usuario = new AcessoVM();
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CustomActionFilter]
        public ActionResult Login(AcessoVM u)
        {
            IQueryable<cliente> cliente = db.cliente.Where(cl => cl.login == u.login && cl.ativo.Equals(true));

            if (cliente.Count()<=0)
            {
                ModelState.AddModelError("login", "Login de cliente inválido");
                return View();
            } else {
                Session["ULCC"] = cliente.First().chaveapi.ToString();
                Session["ULRZ"] = cliente.First().razaoSocial.ToString();
                Session["ULIDC"] = cliente.First().idCliente.ToString();
                Session["ULLC"] = cliente.First().login.ToString();

                //--------------------------------------Verifica Licensa
                licenca licencas = (from l in db.licenca.Where(l=>l.idCliente==CODCLI) select l).FirstOrDefault();

                if (licencas.chave != null && licencas.chave.ToString() != "")
                {
                    string valores = licencas.chave.ToString();
                    string[] array = valores.Split('-');
                    if (array.Count() < 4 || array.Count() > 4)
                    {
                        return RedirectToAction("Licenca").Mensagem("Licença não confere. Por favor informe uma licença válida!!", "Erro");
                    }
                    string chave1 = licencas.chave1.ToString();
                    string chave2 = licencas.chave2.ToString();
                    DateTime data = DateTime.Now.Date;
                    string dtexpiracao = Criptografia.CriptografiaHelper.Decriptar(chave1, chave2, array[1]);
                    DateTime dt = Convert.ToDateTime(dtexpiracao);
                    int result = DateTime.Compare(data, dt);
                    if (result == 0 || result > 0)
                    {
                        return RedirectToAction("Licenca").Mensagem("Não foi encontradata uma Licença, ou sua Licença Expirou.\nInforme uma Licença válida para esta aplicação!!", "Atenção");
                    }
                }
                else
                {
                    return RedirectToAction("Licenca").Mensagem("Não foi encontradata uma Licença, ou sua Licença Expirou.\nInforme uma Licença válida para esta aplicação!!", "Atenção");
                }
            }

            if (!Verificar.isCPFCNPJ(u.cpf, false))
            {
                ModelState.AddModelError("cpf", "CPF Inválido!!");
                return View();
            }

            if (ModelState.IsValid) //verifica se é válido
            {
                string senhahash = MD5Hash.CalculaHash(u.senha);
                acesso v = db.acesso.Where(a => a.cpf.Equals(u.cpf) && a.senha.Equals(senhahash) && a.ativo.Equals(true) && a.idCliente==CODCLI).FirstOrDefault();
                if (v != null)
                {
                    Session["ULID"] = v.idLogin.ToString();
                    Session["ULIDG"] = v.idGrupo.ToString();
                    Session["ULN"] = v.nome.ToString();
                    Session["ULSN"] = v.sobrenome.ToString();
                    Session["ULC"] = v.cpf.ToString();

                    System.Collections.Generic.List<acessoPerfil> pf = (from p in db.acessoPerfil select p).Where(p=>p.idGrupo==v.idGrupo && v.idCliente == CODCLI).ToList();
                    string[,] idList = new string[30,5];
                    foreach (acessoPerfil item in pf)
                    {
                        idList[item.idModulo,0] = item.al.ToString();
                        idList[item.idModulo,1] = item.ap.ToString();
                        idList[item.idModulo,2] = item.cr.ToString();
                        idList[item.idModulo,3] = item.ex.ToString();
                        idList[item.idModulo,4] = item.le.ToString();
                    }
                    Session["ULPR"] = idList;
                    return RedirectToAction("Index", "Home");
                } else {
                    ModelState.AddModelError("cpf", "Usário não encontrado!!");
                }
            }
            return View();
        }

        [CustomActionFilter]
        public ActionResult TrocarSenha()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            TrocarSenhaVM trocar = new TrocarSenhaVM();
            return View(trocar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public ActionResult TrocarSenha(TrocarSenhaVM model)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (ModelState.IsValid)
            {
                acesso acesso = db.acesso.Find(model.idLogin);
                if (acesso == null || acesso.idLogin == 0)
                {
                    ModelState.Clear();
                    return View();
                }
                string str1 = acesso.senha.ToString();
                string str2 = MD5Hash.CalculaHash(model.senhaantiga).ToString();
                bool resultado = str1.Equals(str2);
                if (resultado)
                {
                    acesso.senha = MD5Hash.CalculaHash(model.senha).ToString();
                    db.Entry(acesso).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("LogOff", "Acesso");

                } else { 
                    ModelState.AddModelError("senhaantiga", "Senha ãtual não confere!!");
                }
            }
            //ModelState.Clear();
            return View();
        }

        [HttpGet]
        [CustomActionFilter]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Acesso");
        }

        // GET: Acesso/Details/5
        [CustomActionFilter]
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acesso acesso = await db.acesso.FindAsync(id);
            if (acesso == null)
            {
                return HttpNotFound();
            }
            return View(acesso);
        }

        // GET: Acesso/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            ViewBag.idGrupo = new SelectList(db.acessoGrupo.Where(g => g.idCliente == CODCLI), "IdGrupo", "nome");
            return View();
        }

        // POST: Acesso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Create([Bind(Include = "idCliente,idLogin,idGrupo,email,senha,cpf,ativo,nome,sobrenome")] acesso acesso)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (ModelState.IsValid)
            {
                acesso.senha = MD5Hash.CalculaHash(acesso.senha);
                db.acesso.Add(acesso);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idGrupo = new SelectList(db.acessoGrupo, "IdGrupo", "nome", acesso.idGrupo);
            return View(acesso);
        }

        // GET: Acesso/Edit/5
        [CustomActionFilter]
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acesso acesso = await db.acesso.FindAsync(id);
            if (acesso == null)
            {
                return HttpNotFound();
            }
            ViewBag.idGrupo = new SelectList(db.acessoGrupo.Where(g => g.idCliente == CODCLI), "IdGrupo", "nome", acesso.idGrupo);
            return View(acesso);
        }

        // POST: Acesso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Edit([Bind(Include = "idCliente,idLogin,idGrupo,email,senha,cpf,ativo,nome,sobrenome")] acesso acesso)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (ModelState.IsValid)
            {
                acesso.senha = MD5Hash.CalculaHash(acesso.senha);
                db.Entry(acesso).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idGrupo = new SelectList(db.acessoGrupo, "IdGrupo", "nome", acesso.idGrupo);
            return View(acesso);
        }

        // GET: Acesso/Delete/5
        [CustomActionFilter]
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            acesso acesso = await db.acesso.FindAsync(id);
            if (acesso == null)
            {
                return HttpNotFound();
            }
            return View(acesso);
        }

        // POST: Acesso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            acesso acesso = await db.acesso.FindAsync(id);
            db.acesso.Remove(acesso);
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

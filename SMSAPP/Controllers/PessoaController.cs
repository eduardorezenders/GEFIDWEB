using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using EntidadesDAL;
using SMSAPP.ViewModels;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;
using SMSAPP.Filtros;
using PagedList;

namespace SMSAPP.Controllers
{
    public class PessoaController : Controller
    {
        private SMSWHATSAPPModelos db = new SMSWHATSAPPModelos();

        // GET: Pessoa
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

            var pessoa = db.pessoa.Include(p => p.cliente).Include(p => p.tipoGenero);

            if (!String.IsNullOrEmpty(searchString))
            {
                pessoa = pessoa.Where(s => s.nomeCompleto.Contains(searchString)
                                       || s.nomeCompleto.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "titular_desc":
                    pessoa = pessoa.OrderByDescending(s => s.nomeCompleto);
                    break;
                case "titular":
                    pessoa = pessoa.OrderBy(s => s.nomeCompleto);
                    break;
                default:
                    pessoa = pessoa.OrderBy(c => c.nomeCompleto);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(pessoa.ToPagedList(pageNumber, pageSize));
        }

        // GET: Pessoa/Details/5
        [CustomActionFilter]
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pessoa pessoa = await db.pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Pessoa/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            ViewBag.idTipoTelefone = new SelectList(db.tipoTelefone, "idTipoTelefone", "descricao");
            ViewBag.idGenero = new SelectList(db.tipoGenero, "idGenero", "descricao");
            ViewBag.idTipoLogradouro = new SelectList(db.tipoLogradouro, "idTipoLogradouro", "descricao");
            ViewBag.idTipoEndereco = new SelectList(db.tipoEndereco, "idTipoEndereco", "descricao");
            ViewBag.uf = new SelectList(db.cepbr_estado, "uf", "uf");
            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Create([Bind(Include = "idPessoa,idCliente,nomeCompleto,dataNascimento,ativo,idGenero,cpf")] pessoa pessoa)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (!Verificar.isCPFCNPJ(pessoa.cpf, false))
            {
                ModelState.AddModelError("cpf", "CPF Inválido!!");
                ViewBag.idGenero = new SelectList(db.tipoGenero, "idGenero", "descricao");
                return View();
            }

            if (ModelState.IsValid)
            {
                db.pessoa.Add(pessoa);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaoSocial", pessoa.idCliente);
            ViewBag.idGenero = new SelectList(db.tipoGenero, "idGenero", "descricao", pessoa.idGenero);
            return View(pessoa);
        }

        // GET: Pessoa/Edit/5
        [CustomActionFilter]
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pessoa pessoa = await db.pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            //ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaoSocial", pessoa.idCliente);
            ViewBag.idGenero = new SelectList(db.tipoGenero, "idGenero", "descricao", pessoa.idGenero);
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> Edit([Bind(Include = "idPessoa,idCliente,nomeCompleto,dataNascimento,ativo,idGenero,cpf")] pessoa pessoa)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (!Verificar.isCPFCNPJ(pessoa.cpf, false))
            {
                ModelState.AddModelError("cpf", "CPF Inválido!!");
                ViewBag.idGenero = new SelectList(db.tipoGenero, "idGenero", "descricao");
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Entry(pessoa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "razaoSocial", pessoa.idCliente);
            ViewBag.idGenero = new SelectList(db.tipoGenero, "idGenero", "descricao", pessoa.idGenero);
            return View(pessoa);
        }

        // GET: Pessoa/Delete/5
        [CustomActionFilter]
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pessoa pessoa = await db.pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            pessoa pessoa = await db.pessoa.FindAsync(id);
            db.pessoa.Remove(pessoa);
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
                Model = db.pessoa,
                FileName = "Contato.pdf",
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = { Left = 5, Bottom = 10, Right = 5, Top = 10 },
                CustomSwitches = "--footer-right \"  Criado em: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Página: [page]/[toPage]\"" + " --footer-line --footer-font-size \"10\" --footer-spacing 1 --footer-font-name \"Segoe UI\""
            };
            return arqpdf;
        }

        [CustomActionFilter]
        public ActionResult Importacao()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            return View();
        }

        [HttpPost]
        [CustomActionFilter]
        public ActionResult Importacao(ImportacaoVM model)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            try
            {
                //specify the file name where its actually exist   
                string filepath = model.arquivo;

                //open the excel using openxml sdk  
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filepath, false))
                {

                    //create the object for workbook part  
                    WorkbookPart wbPart = doc.WorkbookPart;

                    //statement to get the count of the worksheet  
                    int worksheetcount = doc.WorkbookPart.Workbook.Sheets.Count();

                    //statement to get the sheet object  
                    Sheet mysheet = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);

                    //statement to get the worksheet object by using the sheet id  
                    Worksheet Worksheet = ((WorksheetPart)wbPart.GetPartById(mysheet.Id)).Worksheet;

                    //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
                    int wkschildno = 4;


                    //statement to get the sheetdata which contains the rows and cell in table  
                    SheetData Rows = (SheetData)Worksheet.ChildElements.GetItem(wkschildno);


                    //getting the row as per the specified index of getitem method  
                    Row currentrow = (Row)Rows.ChildElements.GetItem(1);

                    //getting the cell as per the specified index of getitem method  
                    Cell currentcell = (Cell)currentrow.ChildElements.GetItem(1);

                    //statement to take the integer value  
                    string currentcellvalue = currentcell.InnerText;

                }
            }
            catch (Exception Ex)
            {

                return View().Mensagem(Ex.Message,"Erro");
            }

            return View();
        }

        public JsonResult ListaCidade(string SiglaUf)
        {
            var ret = from c in db.cepbr_cidade
                      orderby c.cidade
                      where c.uf == SiglaUf
                      select new
                      {
                          idCidade = c.id_cidade,
                          cidade = c.cidade
                      };

            return Json(ret.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListaBairro(int? id)
        {
            var ret = from c in db.cepbr_bairro
                      orderby c.bairro
                      where c.id_cidade == id
                      select new
                      {
                          idBairro = c.id_bairro,
                          bairro = c.bairro
                      };

            return Json(ret.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscaCEP(string _cep) {
            try
            {
                var Result = (from a in db.cepbr_endereco where a.cep == _cep select new
                              {
                                  a.logradouro,
                                  a.complemento,
                                  a.id_cidade,
                                  a.id_bairro,
                                  a.tipo_logradouro,
                                  a.cepbr_cidade.uf
                              }).ToList();

                return Json(new { Result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSAPP.Controllers
{
    public class WHATSAPPController : Controller
    {
        // GET: WHATSAPP
        public ActionResult Index()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            return View();
        }
    }
}
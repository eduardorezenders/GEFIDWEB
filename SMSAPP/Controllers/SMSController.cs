using System;
using System.Linq;
using System.Web.Mvc;
using EntidadesDAL;
using SMSAPP.Filtros;
using SMSAPP.ViewModels;
using Comtele.Sdk.Core.Resources;
using Comtele.Sdk.Services;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

namespace SMSAPP.Controllers
{
    public class SMSController : Controller
    {
        private SMSWHATSAPPModelos db = new SMSWHATSAPPModelos();

        public string APIKEY => (string)Session["ULCC"];

        [CustomActionFilter]
        public ActionResult SMSInstantaneo()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            ViewBag.Mensagem = new SelectList(db.mensagem.Where(m => m.ativo == true), "Texto", "Texto");
            return View();
        }

        [CustomActionFilter]
        public ActionResult SMSAniversario()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            ViewBag.Mensagem = new SelectList(db.mensagem.Where(m => m.ativo == true), "Texto", "Texto");
            return View();
        }

        [CustomActionFilter]
        public ActionResult SMSCampanha()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            ViewBag.Mensagem = new SelectList(db.mensagem.Where(m => m.ativo == true), "Texto", "Texto");
            ViewBag.idCampanha = new SelectList(db.campanha.Where(c => c.ativo == true), "idCampanha", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public ActionResult SMSInstantaneo(SMSInstantaneoVM model)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            ViewBag.Mensagem = new SelectList(db.mensagem.Where(m => m.ativo == true), "Texto", "Texto");

            if (ModelState.IsValid) { 
                if (model.Mensagem==null && model.Content==null)
                {
                    return View("SMSInstantaneo").Mensagem("Escreva manualmente ou selecione uma mensagem pré-definida!", "Atenção");
                }
                if (model.Mensagem != null && model.Content != null)
                {
                    return View("SMSInstantaneo").Mensagem("Escolha apenas uma forma de mensagem, manual ou pré-definida!", "Atenção");
                }
                string Sender = model.Sender;
                string Content = model.Content?? model.Mensagem;
                string[] Receivers = new string[model.Receivers.Count()];
                Receivers = model.Receivers.Split(',');

                TextMessageService textMessageService = new TextMessageService(APIKEY);
                var result = textMessageService.Send(Sender, Content, Receivers);
            
                if (result==null)
                {
                    return View("SMSInstantaneo").Mensagem("Não foi possivél enviar seu SMS ", "Erro");
                }
                if (result.Success.Equals(false))
                {
                    return View("SMSInstantaneo").Mensagem("Não foi possivél enviar seu SMS " + result.Message,"Erro");
                } else
                {
                    return RedirectToAction("SMSInstantaneo").Mensagem("SMS's enviado(s) com sucesso!! " + result.Message, "Sucesso");
                }
            }
            return View("SMSInstantaneo");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public ActionResult SMSAniversario(SMSAniversarioVM model)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            ViewBag.Mensagem = new SelectList(db.mensagem.Where(m => m.ativo == true), "Texto", "Texto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionFilter]
        public ActionResult SMSCampanha(SMSCampanhaVM model)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            ViewBag.Mensagem = new SelectList(db.mensagem.Where(m => m.ativo == true), "Texto", "Texto");
            ViewBag.idCampanha = new SelectList(db.campanha.Where(c => c.ativo == true), "idCampanha", "Nome");

            if (model.idCampanha == 0)
            {
                ModelState.AddModelError("idCampanha", "Seleciona uma campanha!!");
                return View();
            }
            if (ModelState.IsValid)
            {
                if (model.Mensagem == null && model.Content == null)
                {
                    return View("SMSCampanha").Mensagem("Escreva manualmente ou selecione uma mensagem pré-definida!", "Atenção");
                }
                if (model.Mensagem != null && model.Content != null)
                {
                    return View("SMSCampanha").Mensagem("Escolha apenas uma forma de mensagem, manual ou pré-definida!", "Atenção");
                }
                string Sender = model.Sender;
                string Content = model.Content ?? model.Mensagem;

                var m = (from c in db.controleCampanha.Where(c => c.idCliente==model.idCliente && c.idCampanha==model.idCampanha) select c).ToList();
                if (m.Count>0)
                {
                    foreach (controleCampanha item in m)
                    {
                        var Receivers = item.telefone.ToString();
                        TextMessageService textMessageService = new TextMessageService(APIKEY);
                        var result = textMessageService.Send(Sender, Content, Receivers);
                        if (result == null)
                        {
                            return RedirectToAction("SMSCampanha").Mensagem("Não foi possivél enviar seu SMS ", "Erro");
                        }
                    }
                    return RedirectToAction("SMSCampanha").Mensagem("SMS's enviado(s) com sucesso!! ", "Sucesso");
                } else
                {
                    return RedirectToAction("SMSCampanha").Mensagem("Campanha não possue registros para envio de SMS!! ", "Erro");
                }
            }
            return View();
        }

        public ActionResult RSMSED()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            var pesquisaDT = new PesquisaDTVM();
            pesquisaDT.dtInicial = DateTime.Now;
            pesquisaDT.dtFinal = DateTime.Now;
            return View(pesquisaDT);
        }

        [HttpPost]
        [CustomActionFilter]
        public ActionResult RSMSED(PesquisaDTVM pesquisaDTVM)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            string data1 = pesquisaDTVM.dtInicial.ToString("yyyy-MM-dd HH:mm:ss");
            string data2 = pesquisaDTVM.dtFinal.ToString("yyyy-MM-dd 23:59:59");

            if (Convert.ToDateTime(data1) > Convert.ToDateTime(data2))
            {
                ModelState.AddModelError("dtInicial", "Data inicial maior que Data Final!");
                return View();
            }

            var textMessageService = new TextMessageService(APIKEY);
            object resultado = null;

            if (pesquisaDTVM.tipo=="Todos")
            {
                resultado = textMessageService.GetDetailedReport(Convert.ToDateTime(data1), Convert.ToDateTime(data2), DeliveryStatus.All);
            }
            else if (pesquisaDTVM.tipo=="Entregues")
            {
                resultado = textMessageService.GetDetailedReport(Convert.ToDateTime(data1), Convert.ToDateTime(data2), DeliveryStatus.Delivered);
            }
            else
            {
                resultado = textMessageService.GetDetailedReport(Convert.ToDateTime(data1), Convert.ToDateTime(data2), DeliveryStatus.Undelivered);
            }

            if (resultado==null)
            {
                return View().Mensagem("Não foram encontrados registros para gerar o relatório!!", "Atenção");
            }

            ViewBag.retornoComtele = resultado;
            return View("RetornoComteleD");
        }

        public ActionResult RSMSEC()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            var pesquisaDT = new PesquisaDTVM();
            pesquisaDT.dtInicial = DateTime.Now;
            pesquisaDT.dtFinal = DateTime.Now;
            return View(pesquisaDT);
        }

        [HttpPost]
        [CustomActionFilter]
        public ActionResult RSMSEC(PesquisaDTVM pesquisaDTVM)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            string data1 = pesquisaDTVM.dtInicial.ToString("yyyy-MM-dd HH:mm:ss");
            string data2 = pesquisaDTVM.dtFinal.ToString("yyyy-MM-dd 23:59:59");

            if (Convert.ToDateTime(data1) > Convert.ToDateTime(data2))
            {
                ModelState.AddModelError("dtInicial", "Data inicial maior que Data Final!");
                return View();
            }

            var textMessageService = new TextMessageService(APIKEY);
            object resultado = null;

            if (pesquisaDTVM.tipo == "Mensal")
            {
                resultado = textMessageService.GetConsolidatedReport(Convert.ToDateTime(data1), Convert.ToDateTime(data2), ReportGroupType.Monthly);
            }
            else 
            {
                resultado = textMessageService.GetConsolidatedReport(Convert.ToDateTime(data1), Convert.ToDateTime(data2), ReportGroupType.Daily);
            }

            if (resultado == null)
            {
                return View().Mensagem("Não foram encontrados registros para gerar o relatório!!", "Atenção");
            }

            ViewBag.retornoComtele = resultado;
            return View("RetornoComteleC");
        }

        public ActionResult RRSMSE()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            var pesquisaDT = new PesquisaDTVM();
            pesquisaDT.dtInicial = DateTime.Now;
            pesquisaDT.dtFinal = DateTime.Now;
            return View(pesquisaDT);
        }

        [HttpPost]
        [CustomActionFilter]
        public ActionResult RRSMSE(PesquisaDTVM pesquisaDTVM)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            string data1 = pesquisaDTVM.dtInicial.ToString("yyyy-MM-dd HH:mm:ss");
            string data2 = pesquisaDTVM.dtFinal.ToString("yyyy-MM-dd 23:59:59");

            if (Convert.ToDateTime(data1) > Convert.ToDateTime(data2))
            {
                ModelState.AddModelError("dtInicial", "Data inicial maior que Data Final!");
                return View();
            }

            var replyService = new ReplyService(APIKEY);
            object resultado = null;

            resultado = replyService.GetReport(Convert.ToDateTime(data1), Convert.ToDateTime(data2), pesquisaDTVM.numero);

            if (resultado == null)
            {
                return View().Mensagem("Não foram encontrados registros para gerar o relatório!!", "Atenção");
            }

            ViewBag.retornoComtele = resultado;
            return View("RetornoComteleR");
        }

        public ActionResult HRU()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            var pesquisaDT = new PesquisaDTVM();
            pesquisaDT.usuario = Session["ULLC"].ToString();
            return View(pesquisaDT);
        }

        [HttpPost]
        [CustomActionFilter]
        public ActionResult HRU(PesquisaDTVM pesquisaDTVM)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            var creditService = new CreditService(APIKEY);
            object resultado = null;

            var result = creditService.GetHistory(pesquisaDTVM.usuario);

            if (resultado == null)
            {
                return View().Mensagem("Não foram encontrados registros para gerar o relatório!!", "Atenção");
            }

            ViewBag.retornoComtele = resultado;
            return View("RetornoComteleH");
        }

        public ActionResult CSU()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            var pesquisaDT = new PesquisaDTVM();
            pesquisaDT.usuario = Session["ULLC"].ToString();
            return View(pesquisaDT);
        }

        [HttpPost]
        [CustomActionFilter]
        public ActionResult CSU(PesquisaDTVM pesquisaDTVM)
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            var creditService = new CreditService(APIKEY);
            object resultado = null;

            var result = creditService.GetCredits(pesquisaDTVM.usuario);

            if (resultado == null)
            {
                return View().Mensagem("Não foram encontrados registros para gerar o relatório!!", "Atenção");
            }

            ViewBag.retornoComtele = resultado;
            return View("RetornoComteleCU");
        }

        public ActionResult CSU2()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }
            return View();
        }

        [HttpPost]
        [CustomActionFilter]
        public ActionResult CSU2a()
        {
            if (Session["ULID"] == null) { return RedirectToAction("Login", "Acesso").Mensagem("Necessário estar logado para executar esta ação, ou sua seção expirou!!", "Atenção"); }

            var creditService = new CreditService(APIKEY);
            object resultado = null;

            var result = creditService.GetMyCredits();

            if (resultado == null)
            {
                return View("CSU2").Mensagem("Não foram encontrados registros para gerar o relatório!!", "Atenção");
            }

            ViewBag.retornoComtele = resultado;
            return View("RetornoComteleCU2");
        }
    }
}
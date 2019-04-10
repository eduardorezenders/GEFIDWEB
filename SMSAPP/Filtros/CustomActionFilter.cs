using System;
using System.Web.Mvc;
using EntidadesDAL;

namespace SMSAPP.Filtros
{
    public class CustomActionFilter : ActionFilterAttribute, IActionFilter
    {        
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            // TODO: Add your action filter's tasks here
            var usuario = filterContext.HttpContext.Session.Contents["ULN"];
            var CPF = filterContext.HttpContext.Session.Contents["ULC"];
            var CODCLI = Convert.ToInt32(filterContext.HttpContext.Session.Contents["ULIDC"]);
            if (usuario==null) { usuario = "Logando"; }
            if (CPF==null) { CPF = "Logando"; }

            // Log Action Filter call
            SMSWHATSAPPModelos storeDb = new SMSWHATSAPPModelos();

            actionsLog log = new actionsLog()
            {
                ActionsLogId = Guid.NewGuid().ToString().ToUpper(),
                Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                Action = string.Concat(filterContext.ActionDescriptor.ActionName, " (Logged By: Custom Action Filter)"),
                Ip = filterContext.HttpContext.Request.UserHostAddress,
                DateAndTime = filterContext.HttpContext.Timestamp,
                Usuario = usuario.ToString(),
                cpf = CPF.ToString(),
                idCliente = CODCLI
            };

            storeDb.actionsLog.Add(log);
            storeDb.SaveChanges();
            OnActionExecuting(filterContext);
        }
    }
}
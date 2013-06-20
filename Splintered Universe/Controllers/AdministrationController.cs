using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Cumis.Models;
using Cumis.Models.VM;

using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;

namespace Splintered_Universe.Controllers
{
    public class AdministrationController : Controller
    {
        private SUContext db = new SUContext();
        //
        // GET: /Administration/
        public ActionResult UserErrorLog()
        {
            return View("ErrorLog");
        }

        public ActionResult ElmahRead([DataSourceRequest]DataSourceRequest request)
        {
            var tElmah = db.ELMAH_Error;
            DataSourceResult result = tElmah.ToDataSourceResult(request, so => new ElmahErrorVM
            {
                Id = so.ErrorId,
                Application = so.Application,
                Host = so.Host,
                Message = so.Message,
                Sequence = so.Sequence,
                Source = so.Source,
                StatusCode = so.StatusCode,
                TimeUTC = so.TimeUtc.ToLocalTime(),
                Type = so.Type,
                User = so.User
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}

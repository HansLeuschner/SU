using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cumis.Controllers
{
    public class ErrorController : Controller
    {

        [HttpPost]
        public void LogJavascriptError(string message)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new JavascriptExecption(message));
        }

    }
}

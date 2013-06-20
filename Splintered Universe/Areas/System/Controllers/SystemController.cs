using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Splintered_Universe.Models;
using Splintered_Universe.Models.VM;

using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;

namespace Splintered_Universe.Areas.System.Controllers
{
    public class SystemController : Controller
    {
        //
        // GET: /System/
        private SUContext db = new SUContext();

        public ActionResult Systems()
        {
            return PartialView();
        }

        public JsonResult AvailableSystemsListing([DataSourceRequest]DataSourceRequest request, bool AddDefault = false)
        {
            IQueryable<SystemRule> tObj = null;
            int iUserId = ((UserSessionVM)HttpContext.Session["UserSession"]).UserId;
            tObj = db.SystemRules.Where( w => w.Campaigns.Any(wc => wc.UserUserId == iUserId)).OrderBy(o => o.SystemName);
            List<SystemRuleVM> resultList = new List<SystemRuleVM>();
            SystemRuleVM firstItem = new SystemRuleVM();
            if (AddDefault)
            {
                firstItem.SystemRuleId = -1;
                firstItem.SystemName = "None";
                resultList.Add(firstItem);
            }
            foreach (var item in tObj)
            {
                SystemRuleVM newItem = new SystemRuleVM();
                newItem.SystemRuleId = item.SystemRuleId;
                newItem.SystemName = item.SystemName;
                resultList.Add(newItem);
            }

            return Json(resultList, JsonRequestBehavior.AllowGet);
        } // end Products


        public JsonResult SystemsListing([DataSourceRequest]DataSourceRequest request, bool AddDefault = false)
        {
            IQueryable<SystemRule> tObj = null;
            tObj = db.SystemRules.OrderBy(o => o.SystemName);
            List<SystemRuleVM> resultList = new List<SystemRuleVM>();
            SystemRuleVM firstItem = new SystemRuleVM();
            if (AddDefault)
            {
                firstItem.SystemRuleId = -1;
                firstItem.SystemName = "None";
                resultList.Add(firstItem);
            }
            foreach (var item in tObj)
            {
                SystemRuleVM newItem = new SystemRuleVM();
                newItem.SystemRuleId = item.SystemRuleId;
                newItem.SystemName = item.SystemName;
                resultList.Add(newItem);
            }

            return Json(resultList, JsonRequestBehavior.AllowGet);
        } // end Products

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var secObj = db.SystemRules;
            DataSourceResult result = secObj.ToDataSourceResult(request, so => new SystemRuleVM
            {
                 SystemRuleId = so.SystemRuleId,
                 SystemName = so.SystemName,
                 Description = so.Description
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, SystemRuleVM viewModel)
        {
            try
            {
                if (viewModel != null && ModelState.IsValid)
                {
                    SystemRule newviewModel = new SystemRule();

                    newviewModel.SystemName = viewModel.SystemName;
                    newviewModel.Description = viewModel.Description;

                    db.SystemRules.Add(newviewModel);
                    db.SaveChanges();
                    viewModel.SystemRuleId = newviewModel.SystemRuleId;
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Data error");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, SystemRuleVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SystemRule newviewModel = new SystemRule();

                    newviewModel.SystemName = viewModel.SystemName;
                    newviewModel.Description = viewModel.Description;
                    newviewModel.SystemRuleId = viewModel.SystemRuleId;

                    db.Entry(newviewModel).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Data error");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, SystemRuleVM viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    ModelState.Clear();
                    SystemRule delviewModel = db.SystemRules.Find(viewModel.SystemRuleId);
                    if (delviewModel != null)
                    {
                        db.SystemRules.Remove(delviewModel);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException dbUpdEx)
            {
                ModelState.AddModelError("showerror", "Delete failed.  This is most likely due to related child information that needs to be deleted first.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("Delete failed.  This is most likely due to related child information that needs to be deleted first."));
                Elmah.ErrorSignal.FromCurrentContext().Raise(dbUpdEx);
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError("hideerror", "Data error");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }
            return Json(ModelState.IsValid ? new object() : ModelState.ToDataSourceResult());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;

using System.Net.Http;
using Splintered_Universe.Models;
using Splintered_Universe.Models.VM;

using Splintered_Universe;

namespace Splintered_Universe.Areas.System.Controllers
{   
    public class SystemStatController : Controller
    {
        private SUContext db = new SUContext();

        public ActionResult SystemStats()
        {
            return PartialView();
        }

		// SpringGage
        public JsonResult SystemStatsListing([DataSourceRequest]DataSourceRequest request, int? SystemRuleId, bool AddDefault = false)
		{
		    IQueryable<SystemStat> tObj = null;
            if (SystemRuleId.HasValue)
            {
                tObj = db.SystemStats.Where(w => w.SystemRuleSystemRuleId == SystemRuleId).OrderBy(o => o.Name);
            }
            else
            {
                tObj = db.SystemStats.OrderBy(o => o.Name);
            }
            List<SystemStatVM> resultList = new List<SystemStatVM>();
            SystemStatVM firstItem = new SystemStatVM();
            if (AddDefault)
            {
                firstItem.SystemStatId = -1;
                firstItem.Name = "None";
                resultList.Add(firstItem);
            }
            foreach (var item in tObj)
            {
                SystemStatVM newItem = new SystemStatVM();
                newItem.SystemStatId = item.SystemStatId;
                newItem.Name = item.Name;
                resultList.Add(newItem);
            }
            return Json(resultList, JsonRequestBehavior.AllowGet);
		}

        public ActionResult ReadSystemStats([DataSourceRequest]DataSourceRequest request, int? SystemRuleId)
        {
            IQueryable<SystemStat> tObj;
            if (SystemRuleId.HasValue)
            {
                tObj = db.SystemStats.Where(w => w.SystemRuleSystemRuleId == SystemRuleId);
            }
            else
            {
                tObj = db.SystemStats;
            }
            DataSourceResult result = tObj.ToDataSourceResult(request, model => Mapper.Map<SystemStatVM>(model));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

		[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateSystemStat([DataSourceRequest] DataSourceRequest request, SystemStatVM viewModel)
        {
            try
            {
                if (viewModel != null && ModelState.IsValid)
                {
                    SystemStat newviewModel = new SystemStat();

                    newviewModel = Mapper.Map<SystemStatVM, SystemStat>(viewModel, newviewModel);

                    db.SystemStats.Add(newviewModel);
                    db.SaveChanges();
                    viewModel.SystemStatId = newviewModel.SystemStatId;
                    viewModel.SystemRuleSystemName = db.SystemRules.Find(viewModel.SystemRuleSystemRuleId).SystemName;
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Could not add SystemStat.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateSystemStat([DataSourceRequest] DataSourceRequest request, SystemStatVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SystemStat newviewModel = new SystemStat();

                    newviewModel = Mapper.Map<SystemStatVM, SystemStat>(viewModel, newviewModel);

                    db.Entry(newviewModel).State = EntityState.Modified;
                    db.SaveChanges();
                    viewModel.SystemRuleSystemName = db.SystemRules.Find(viewModel.SystemRuleSystemRuleId).SystemName;
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Could not update SystemStat.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroySystemStat([DataSourceRequest] DataSourceRequest request, SystemStatVM viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    ModelState.Clear();
                    SystemStat delviewModel = db.SystemStats.Find(viewModel.SystemStatId);
                    if (delviewModel != null)
                    {
                        db.SystemStats.Remove(delviewModel);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException dbUpdEx)
            {
                ModelState.AddModelError("showerror", "Delete failed.  This is most likely due to related child information that needs to be deleted first.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dbUpdEx);
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError("hideerror", "Could not delete SystemStat.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }
            return Json(ModelState.IsValid ? new object() : ModelState.ToDataSourceResult());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
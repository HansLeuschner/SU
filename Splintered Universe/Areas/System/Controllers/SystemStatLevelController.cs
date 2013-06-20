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
    public class SystemStatLevelController : Controller
    {
        private SUContext db = new SUContext();

        public ActionResult SystemStatLevels()
        {
            return PartialView();
        }

		// SpringGage
        public JsonResult SystemStatLevelsListing([DataSourceRequest]DataSourceRequest request, bool AddDefault = false)
		{
		    IQueryable<SystemStatLevel> tObj = null;
            tObj = db.SystemStatLevels.OrderBy(o => o.Level);
            List<SystemStatLevelVM> resultList = new List<SystemStatLevelVM>();
            SystemStatLevelVM firstItem = new SystemStatLevelVM();
            if (AddDefault)
            {
                firstItem.SystemStatLevelId = -1;
                firstItem.Level = 0;
                resultList.Add(firstItem);
            }
            foreach (var item in tObj)
            {
                SystemStatLevelVM newItem = new SystemStatLevelVM();
                newItem.SystemStatLevelId = item.SystemStatLevelId;
                newItem.Level = item.Level;
                resultList.Add(newItem);
            }
            return Json(resultList, JsonRequestBehavior.AllowGet);
		}

        public ActionResult ReadSystemStatLevels([DataSourceRequest]DataSourceRequest request, int? SystemRuleId)
        {
            IQueryable<SystemStatLevel> tObj;
            if (SystemRuleId.HasValue)
            {
                tObj = db.SystemStatLevels.Where(w => w.SystemStat.SystemRuleSystemRuleId == SystemRuleId);
            }
            else
            {
                tObj = db.SystemStatLevels;
            }
            
            DataSourceResult result = tObj.ToDataSourceResult(request, model => Mapper.Map<SystemStatLevelVM>(model));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

		[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateSystemStatLevel([DataSourceRequest] DataSourceRequest request, SystemStatLevelVM viewModel)
        {
            try
            {
                if (viewModel != null && ModelState.IsValid)
                {
                    SystemStatLevel newviewModel = new SystemStatLevel();

                    newviewModel = Mapper.Map<SystemStatLevelVM, SystemStatLevel>(viewModel, newviewModel);

                    db.SystemStatLevels.Add(newviewModel);
                    db.SaveChanges();
                    viewModel.SystemStatLevelId = newviewModel.SystemStatLevelId;
                    viewModel.SystemStatName = db.SystemStats.Find(viewModel.SystemStatSystemStatId).Name;

                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Could not add SystemStatLevel.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateSystemStatLevel([DataSourceRequest] DataSourceRequest request, SystemStatLevelVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SystemStatLevel newviewModel = new SystemStatLevel();

                    newviewModel = Mapper.Map<SystemStatLevelVM, SystemStatLevel>(viewModel, newviewModel);

                    db.Entry(newviewModel).State = EntityState.Modified;
                    db.SaveChanges();
                    viewModel.SystemStatName = db.SystemStats.Find(viewModel.SystemStatSystemStatId).Name;
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Could not update SystemStatLevel.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroySystemStatLevel([DataSourceRequest] DataSourceRequest request, SystemStatLevelVM viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    ModelState.Clear();
                    SystemStatLevel delviewModel = db.SystemStatLevels.Find(viewModel.SystemStatLevelId);
                    if (delviewModel != null)
                    {
                        db.SystemStatLevels.Remove(delviewModel);
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
                ModelState.AddModelError("hideerror", "Could not delete SystemStatLevel.");
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
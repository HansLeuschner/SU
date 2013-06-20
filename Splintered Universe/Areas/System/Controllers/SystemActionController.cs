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
    public class SystemActionController : Controller
    {
        private SUContext db = new SUContext();

        public ActionResult SystemActions()
        {
            return PartialView();
        }

		// SpringGage
        public JsonResult SystemActionsListing([DataSourceRequest]DataSourceRequest request, int? SystemRuleId, bool AddDefault = false)
		{
		    IQueryable<SystemAction> tObj = null;
            if (SystemRuleId.HasValue)
            {
                tObj = db.SystemActions.Where(w => w.SystemActionCategory.SystemRuleSystemRuleId == SystemRuleId).OrderBy(o => o.Name);
            }
            else
            {
                tObj = db.SystemActions.OrderBy(o => o.Name);
            }
            List<SystemActionVM> resultList = new List<SystemActionVM>();
            SystemActionVM firstItem = new SystemActionVM();
            if (AddDefault)
            {
                firstItem.SystemActionId = -1;
                firstItem.Name = "None";
                resultList.Add(firstItem);
            }
            foreach (var item in tObj)
            {
                SystemActionVM newItem = new SystemActionVM();
                newItem.SystemActionId = item.SystemActionId;
                newItem.Name = item.Name;
                resultList.Add(newItem);
            }
            return Json(resultList, JsonRequestBehavior.AllowGet);
		}

        public ActionResult ReadSystemActions([DataSourceRequest]DataSourceRequest request, int? SystemRuleId)
        {
            IQueryable<SystemAction> tObj;
            if (SystemRuleId.HasValue)
            {
                tObj = db.SystemActions.Where(w => w.SystemActionCategory.SystemRuleSystemRuleId == SystemRuleId);
            }
            else
            {

                tObj = db.SystemActions;
            }
            DataSourceResult result = tObj.ToDataSourceResult(request, model => Mapper.Map<SystemActionVM>(model));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

		[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateSystemAction([DataSourceRequest] DataSourceRequest request, SystemActionVM viewModel)
        {
            try
            {
                if (viewModel != null && ModelState.IsValid)
                {
                    SystemAction newviewModel = new SystemAction();

                    newviewModel = Mapper.Map<SystemActionVM, SystemAction>(viewModel, newviewModel);

                    db.SystemActions.Add(newviewModel);
                    db.SaveChanges();
                    viewModel.SystemActionId = newviewModel.SystemActionId;
                    viewModel.SystemActionCategoryName = db.SystemActionCategories.Find(viewModel.SystemActionCategorySystemActionCategoryId).Name;
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Could not add SystemAction.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateSystemAction([DataSourceRequest] DataSourceRequest request, SystemActionVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SystemAction newviewModel = new SystemAction();

                    newviewModel = Mapper.Map<SystemActionVM, SystemAction>(viewModel, newviewModel);

                    db.Entry(newviewModel).State = EntityState.Modified;
                    db.SaveChanges();
                    viewModel.SystemActionCategoryName = db.SystemActionCategories.Find(viewModel.SystemActionCategorySystemActionCategoryId).Name;
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Could not update SystemAction.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroySystemAction([DataSourceRequest] DataSourceRequest request, SystemActionVM viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    ModelState.Clear();
                    SystemAction delviewModel = db.SystemActions.Find(viewModel.SystemActionId);
                    if (delviewModel != null)
                    {
                        db.SystemActions.Remove(delviewModel);
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
                ModelState.AddModelError("hideerror", "Could not delete SystemAction.");
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
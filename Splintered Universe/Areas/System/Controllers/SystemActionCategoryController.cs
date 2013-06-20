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
    public class SystemActionCategoryController : Controller
    {
        private SUContext db = new SUContext();

        public ActionResult SystemActionCategories()
        {
            return PartialView();
        }

		// SpringGage
        public JsonResult SystemActionCategoriesListing([DataSourceRequest]DataSourceRequest request, int? SystemRuleId, bool AddDefault = false)
		{
		    IQueryable<SystemActionCategory> tObj = null;
            if (SystemRuleId.HasValue)
            {
                tObj = db.SystemActionCategories.Where(w => w.SystemRuleSystemRuleId == SystemRuleId).OrderBy(o => o.Name);
            }
            else
            {
                tObj = db.SystemActionCategories.OrderBy(o => o.Name);
            }
            
            List<SystemActionCategoryVM> resultList = new List<SystemActionCategoryVM>();
            SystemActionCategoryVM firstItem = new SystemActionCategoryVM();
            if (AddDefault)
            {
                firstItem.SystemActionCategoryId = -1;
                firstItem.Name = "None";
                resultList.Add(firstItem);
            }
            foreach (var item in tObj)
            {
                SystemActionCategoryVM newItem = new SystemActionCategoryVM();
                newItem.SystemActionCategoryId = item.SystemActionCategoryId;
                newItem.Name = item.Name;
                resultList.Add(newItem);
            }
            return Json(resultList, JsonRequestBehavior.AllowGet);
		}

        public ActionResult ReadSystemActionCategories([DataSourceRequest]DataSourceRequest request, int? SystemRuleId)
        {
            IQueryable<SystemActionCategory> tObj;
            if (SystemRuleId.HasValue)
            {
                tObj = db.SystemActionCategories.Where(w => w.SystemRuleSystemRuleId == SystemRuleId);
            }
            else
            {
                tObj = db.SystemActionCategories;
            }
            DataSourceResult result = tObj.ToDataSourceResult(request, model => Mapper.Map<SystemActionCategoryVM>(model));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

		[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateSystemActionCategory([DataSourceRequest] DataSourceRequest request, SystemActionCategoryVM viewModel)
        {
            try
            {
                if (viewModel != null && ModelState.IsValid)
                {
                    SystemActionCategory newviewModel = new SystemActionCategory();

                    newviewModel = Mapper.Map<SystemActionCategoryVM, SystemActionCategory>(viewModel, newviewModel);

                    db.SystemActionCategories.Add(newviewModel);
                    db.SaveChanges();
                    viewModel.SystemActionCategoryId = newviewModel.SystemActionCategoryId;
                    viewModel.SystemRuleSystemName = db.SystemRules.Find(viewModel.SystemRuleSystemRuleId).SystemName;
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Could not add SystemActionCategory.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateSystemActionCategory([DataSourceRequest] DataSourceRequest request, SystemActionCategoryVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SystemActionCategory newviewModel = new SystemActionCategory();

                    newviewModel = Mapper.Map<SystemActionCategoryVM, SystemActionCategory>(viewModel, newviewModel);

                    db.Entry(newviewModel).State = EntityState.Modified;
                    db.SaveChanges();
                    viewModel.SystemRuleSystemName = db.SystemRules.Find(viewModel.SystemRuleSystemRuleId).SystemName;
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Could not update SystemActionCategory.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroySystemActionCategory([DataSourceRequest] DataSourceRequest request, SystemActionCategoryVM viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    ModelState.Clear();
                    SystemActionCategory delviewModel = db.SystemActionCategories.Find(viewModel.SystemActionCategoryId);
                    if (delviewModel != null)
                    {
                        db.SystemActionCategories.Remove(delviewModel);
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
                ModelState.AddModelError("hideerror", "Could not delete SystemActionCategory.");
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
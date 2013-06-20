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
    public class SystemSkillCategoryController : Controller
    {
        private SUContext db = new SUContext();

        public ActionResult SystemSkillCategories()
        {
            return PartialView();
        }

		// SpringGage
        public JsonResult SystemSkillCategoriesListing([DataSourceRequest]DataSourceRequest request, int? SystemRuleId, bool AddDefault = false)
		{
		    IQueryable<SystemSkillCategory> tObj = null;
            if (SystemRuleId.HasValue)
            {
                tObj = db.SystemSkillCategories.Where(w => w.SystemRuleSystemRuleId == SystemRuleId).OrderBy(o => o.Name);
            }
            else
            {
                tObj = db.SystemSkillCategories.OrderBy(o => o.Name);
            }
            List<SystemSkillCategoryVM> resultList = new List<SystemSkillCategoryVM>();
            SystemSkillCategoryVM firstItem = new SystemSkillCategoryVM();
            if (AddDefault)
            {
                firstItem.SystemSkillCategoryId = -1;
                firstItem.Name = "None";
                resultList.Add(firstItem);
            }
            foreach (var item in tObj)
            {
                SystemSkillCategoryVM newItem = new SystemSkillCategoryVM();
                newItem.SystemSkillCategoryId = item.SystemSkillCategoryId;
                newItem.Name = item.Name;
                resultList.Add(newItem);
            }
            return Json(resultList, JsonRequestBehavior.AllowGet);
		}

        public ActionResult ReadSystemSkillCategories([DataSourceRequest]DataSourceRequest request, int? SystemRuleId )
        {
            IQueryable<SystemSkillCategory> tObj;
            if (SystemRuleId.HasValue)
            {
                tObj = db.SystemSkillCategories.Where(w => w.SystemRuleSystemRuleId == SystemRuleId);
            }
            else
            {
                tObj = db.SystemSkillCategories;
            }
            DataSourceResult result = tObj.ToDataSourceResult(request, model => Mapper.Map<SystemSkillCategoryVM>(model));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

		[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateSystemSkillCategory([DataSourceRequest] DataSourceRequest request, SystemSkillCategoryVM viewModel, int? SystemRuleId)
        {
            try
            {
                if (viewModel != null && ModelState.IsValid)
                {
                    SystemSkillCategory newviewModel = new SystemSkillCategory();

                    newviewModel = Mapper.Map<SystemSkillCategoryVM, SystemSkillCategory>(viewModel, newviewModel);
                    if (SystemRuleId.HasValue)
                    {
                        newviewModel.SystemRuleSystemRuleId = (int)SystemRuleId;
                        viewModel.SystemRuleSystemRuleId = (int)SystemRuleId;
                    }

                    db.SystemSkillCategories.Add(newviewModel);
                    db.SaveChanges();
                    viewModel.SystemSkillCategoryId = newviewModel.SystemSkillCategoryId;
                    viewModel.SystemRuleSystemName = db.SystemRules.Find(viewModel.SystemRuleSystemRuleId).SystemName;
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Could not add SystemSkillCategory.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateSystemSkillCategory([DataSourceRequest] DataSourceRequest request, SystemSkillCategoryVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SystemSkillCategory newviewModel = new SystemSkillCategory();

                    newviewModel = Mapper.Map<SystemSkillCategoryVM, SystemSkillCategory>(viewModel, newviewModel);

                    db.Entry(newviewModel).State = EntityState.Modified;
                    db.SaveChanges();
                    viewModel.SystemRuleSystemName = db.SystemRules.Find(viewModel.SystemRuleSystemRuleId).SystemName;
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Could not update SystemSkillCategory.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroySystemSkillCategory([DataSourceRequest] DataSourceRequest request, SystemSkillCategoryVM viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    ModelState.Clear();
                    SystemSkillCategory delviewModel = db.SystemSkillCategories.Find(viewModel.SystemSkillCategoryId);
                    if (delviewModel != null)
                    {
                        db.SystemSkillCategories.Remove(delviewModel);
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
                ModelState.AddModelError("hideerror", "Could not delete SystemSkillCategory.");
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
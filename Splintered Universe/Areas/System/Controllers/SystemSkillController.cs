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
    public class SystemSkillController : Controller
    {
        private SUContext db = new SUContext();

        public ActionResult SystemSkills()
        {
            return PartialView();
        }

		// SpringGage
        public JsonResult SystemSkillsListing([DataSourceRequest]DataSourceRequest request, bool AddDefault = false)
		{
		    IQueryable<SystemSkill> tObj = null;
            tObj = db.SystemSkills.OrderBy(o => o.Name);
            List<SystemSkillVM> resultList = new List<SystemSkillVM>();
            SystemSkillVM firstItem = new SystemSkillVM();
            if (AddDefault)
            {
                firstItem.SystemSkillId = -1;
                firstItem.Name = "None";
                resultList.Add(firstItem);
            }
            foreach (var item in tObj)
            {
                SystemSkillVM newItem = new SystemSkillVM();
                newItem.SystemSkillId = item.SystemSkillId;
                newItem.Name = item.Name;
                resultList.Add(newItem);
            }
            return Json(resultList, JsonRequestBehavior.AllowGet);
		}

        public ActionResult ReadSystemSkills([DataSourceRequest]DataSourceRequest request, int? SystemRuleId)
        {
            IQueryable<SystemSkill> tObj;
            if (SystemRuleId.HasValue)
            {
                tObj = db.SystemSkills.Where(w => w.SystemSkillCategory.SystemRuleSystemRuleId == SystemRuleId);
            }
            else
            {
                tObj = db.SystemSkills;
            }
            DataSourceResult result = tObj.ToDataSourceResult(request, model => Mapper.Map<SystemSkillVM>(model));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

		[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateSystemSkill([DataSourceRequest] DataSourceRequest request, SystemSkillVM viewModel)
        {
            try
            {
                if (viewModel != null && ModelState.IsValid)
                {
                    SystemSkill newviewModel = new SystemSkill();

                    newviewModel = Mapper.Map<SystemSkillVM, SystemSkill>(viewModel, newviewModel);

                    db.SystemSkills.Add(newviewModel);
                    db.SaveChanges();
                    viewModel.SystemSkillId = newviewModel.SystemSkillId;
                    viewModel.SystemSkillCategoryName = db.SystemSkillCategories.Find(viewModel.SystemSkillCategorySystemSkillCategoryId).Name;
                    viewModel.SystemActionName = db.SystemActions.Find(viewModel.SystemActionSystemActionId).Name;
                    viewModel.SystemStatName = db.SystemStats.Find(viewModel.SystemStatSystemStatId).Name;
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Could not add SystemSkill.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateSystemSkill([DataSourceRequest] DataSourceRequest request, SystemSkillVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SystemSkill newviewModel = new SystemSkill();

                    newviewModel = Mapper.Map<SystemSkillVM, SystemSkill>(viewModel, newviewModel);

                    db.Entry(newviewModel).State = EntityState.Modified;
                    db.SaveChanges();
                    viewModel.SystemSkillCategoryName = db.SystemSkillCategories.Find(viewModel.SystemSkillCategorySystemSkillCategoryId).Name;
                    viewModel.SystemActionName = db.SystemActions.Find(viewModel.SystemActionSystemActionId).Name;
                    viewModel.SystemStatName = db.SystemStats.Find(viewModel.SystemStatSystemStatId).Name;
                }
            }
            catch (DataException dataEx)
            {
                ModelState.AddModelError(string.Empty, "Could not update SystemSkill.");
                Elmah.ErrorSignal.FromCurrentContext().Raise(dataEx);
            }

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroySystemSkill([DataSourceRequest] DataSourceRequest request, SystemSkillVM viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    ModelState.Clear();
                    SystemSkill delviewModel = db.SystemSkills.Find(viewModel.SystemSkillId);
                    if (delviewModel != null)
                    {
                        db.SystemSkills.Remove(delviewModel);
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
                ModelState.AddModelError("hideerror", "Could not delete SystemSkill.");
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
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

namespace Splintered_Universe.Controllers
{
    public class CampaignController : Controller
    {
        //
        // GET: /Campaign/
        private SUContext db = new SUContext();

        public ActionResult Management()
        {
            return View();
        }

        public ActionResult Campaigns()
        {
            return PartialView();
        }

        public ActionResult Configuration()
        {
            return View();
        }

        public JsonResult CampaignsListing([DataSourceRequest]DataSourceRequest request, bool AddDefault = false)
        {
            IQueryable<Campaign> tObj = null;
            tObj = db.Campaigns.OrderBy(o => o.Name);
            List<CampaignVM> resultList = new List<CampaignVM>();
            CampaignVM firstItem = new CampaignVM();
            if (AddDefault)
            {
                firstItem.CampaignId = -1;
                firstItem.Name = "None";
                resultList.Add(firstItem);
            }
            foreach (var item in tObj)
            {
                CampaignVM newItem = new CampaignVM();
                newItem.CampaignId = item.CampaignId;
                newItem.Name = item.Name;
                resultList.Add(newItem);
            }
            return Json(resultList, JsonRequestBehavior.AllowGet);
        } // end PlayersListing

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var secObj = db.Campaigns;
            DataSourceResult result = secObj.ToDataSourceResult(request, so => new CampaignVM
            {
                CampaignId = so.CampaignId,
                EndDate = so.EndDate,
                StartDate = so.StartDate,
                GameMasterId = so.UserUserId,
                GameMaster = so.User.Name,
                SystemId = so.SystemRuleSystemRuleId,
                System = so.SystemRule.SystemName,
                Description = so.Description,
                Name = so.Name
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,CampaignVM viewModel)
        {
            try
            {
                if (viewModel != null && ModelState.IsValid)
                {
                    Campaign newviewModel = new Campaign();

                    newviewModel.EndDate = viewModel.EndDate;
                    newviewModel.Name = viewModel.Name;
                    newviewModel.StartDate = viewModel.StartDate;
                    newviewModel.SystemRuleSystemRuleId = viewModel.SystemId;
                    newviewModel.UserUserId = viewModel.GameMasterId;
                    newviewModel.Description = viewModel.Description;

                    db.Campaigns.Add(newviewModel);
                    db.SaveChanges();
                    viewModel.CampaignId = newviewModel.CampaignId;
                    viewModel.System = db.SystemRules.Find(viewModel.SystemId).SystemName;
                    viewModel.GameMaster = db.Users.Find(viewModel.GameMasterId).Name; 
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
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, CampaignVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Campaign newviewModel = new Campaign();

                    newviewModel.CampaignId = viewModel.CampaignId;
                    newviewModel.EndDate = viewModel.EndDate;
                    newviewModel.Name = viewModel.Name;
                    newviewModel.StartDate = viewModel.StartDate;
                    newviewModel.SystemRuleSystemRuleId = viewModel.SystemId;
                    newviewModel.UserUserId = viewModel.GameMasterId;
                    newviewModel.Description = viewModel.Description;

                    db.Entry(newviewModel).State = EntityState.Modified;
                    db.SaveChanges();
                    viewModel.System = db.SystemRules.Find(viewModel.SystemId).SystemName;
                    viewModel.GameMaster = db.Users.Find(viewModel.GameMasterId).Name;
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
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, CampaignVM viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    ModelState.Clear();
                    Campaign delviewModel = db.Campaigns.Find(viewModel.CampaignId);
                    if (delviewModel != null)
                    {
                        db.Campaigns.Remove(delviewModel);
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

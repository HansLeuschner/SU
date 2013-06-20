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
    public class CharacterController : Controller
    {
        //
        // GET: /Character/
        private SUContext db = new SUContext();


        public ActionResult Characters()
        {
            return PartialView();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var tObj = db.Characters;
            DataSourceResult result = tObj.ToDataSourceResult(request, so => new CharacterVM
            {
                CharacterId = so.CharacterId,
                Name = so.Name,
                Campaign = so.Campaign.Name,
                CampaignId = so.CampaignCampaignId,
                Player = so.User.Name,
                PlayerId = so.UserUserId
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, CharacterVM viewModel)
        {
            try
            {
                if (viewModel != null && ModelState.IsValid)
                {
                    Character newviewModel = new Character();

                    newviewModel.Name = viewModel.Name;
                    newviewModel.UserUserId = viewModel.PlayerId;
                    newviewModel.CampaignCampaignId = viewModel.CampaignId;

                    db.Characters.Add(newviewModel);
                    db.SaveChanges();
                    viewModel.CharacterId = newviewModel.CharacterId;
                    viewModel.Campaign = db.Campaigns.Find(viewModel.CampaignId).Name;
                    viewModel.Player = db.Users.Find(viewModel.PlayerId).Name;
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
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, CharacterVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Character newviewModel = new Character();

                    newviewModel.CharacterId = viewModel.CharacterId;
                    newviewModel.Name = viewModel.Name;
                    newviewModel.UserUserId = viewModel.PlayerId;
                    newviewModel.CampaignCampaignId = viewModel.CampaignId;

                    db.Entry(newviewModel).State = EntityState.Modified;
                    db.SaveChanges();
                    viewModel.Campaign = db.Campaigns.Find(viewModel.CampaignId).Name;
                    viewModel.Player = db.Users.Find(viewModel.PlayerId).Name;
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
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, CharacterVM viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    ModelState.Clear();
                    Character delviewModel = db.Characters.Find(viewModel.CharacterId);
                    if (delviewModel != null)
                    {
                        db.Characters.Remove(delviewModel);
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

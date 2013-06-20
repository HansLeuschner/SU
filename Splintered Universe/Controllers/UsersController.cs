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
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        private SUContext db = new SUContext();

        public ActionResult UserManagement()
        {
            return View();
        }
        public ActionResult Users()
        {
            return PartialView();
        }

        public JsonResult GMsListing([DataSourceRequest]DataSourceRequest request, bool AddDefault = false)
        {
            IQueryable<User> tObj = null;
            tObj = db.Users.Where(w => w.CanBeGM == true).OrderBy(o => o.Name);
            List<UserViewModel> resultList = new List<UserViewModel>();
            UserViewModel firstItem = new UserViewModel();
            if (AddDefault)
            {
                firstItem.UserId = -1;
                firstItem.Name = "None";
                firstItem.LoginName = "None";
                resultList.Add(firstItem);
            }
            foreach (var item in tObj)
            {
                UserViewModel newItem = new UserViewModel();
                newItem.UserId = item.UserId;
                newItem.Name = item.Name;
                resultList.Add(newItem);
            }

            return Json(resultList, JsonRequestBehavior.AllowGet);
        } // end GMSListing

        public JsonResult PlayersListing([DataSourceRequest]DataSourceRequest request, bool AddDefault = false)
        {
            IQueryable<User> tObj = null;
            tObj = db.Users.OrderBy(o => o.Name);
            List<UserViewModel> resultList = new List<UserViewModel>();
            UserViewModel firstItem = new UserViewModel();
            if (AddDefault)
            {
                firstItem.UserId = -1;
                firstItem.Name = "None";
                firstItem.LoginName = "None";
                resultList.Add(firstItem);
            }
            foreach (var item in tObj)
            {
                UserViewModel newItem = new UserViewModel();
                newItem.UserId = item.UserId;
                newItem.Name = item.Name;
                resultList.Add(newItem);
            }
            return Json(resultList, JsonRequestBehavior.AllowGet);
        } // end PlayersListing

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var secObj = db.Users;
            DataSourceResult result = secObj.ToDataSourceResult(request, so => new  UserViewModel
            {
                UserId = so.UserId,
                LoginName = so.LoginName,
                Name = so.Name,
                IsAdmin = so.IsAdmin,
                CanBeGM = so.CanBeGM,
                Password = so.Password
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, UserViewModel viewModel)
        {
            try
            {
                if (viewModel != null && ModelState.IsValid)
                {
                    User newviewModel = new User();

                    newviewModel.Name = viewModel.Name;
                    newviewModel.LoginName = viewModel.LoginName;
                    newviewModel.Password = viewModel.Password;
                    newviewModel.IsAdmin = viewModel.IsAdmin;
                    newviewModel.CanBeGM = viewModel.CanBeGM;

                    db.Users.Add(newviewModel);
                    db.SaveChanges();
                    viewModel.UserId = newviewModel.UserId;
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
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, UserViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User newviewModel = new User();

                    newviewModel.Name = viewModel.Name;
                    newviewModel.LoginName = viewModel.LoginName;
                    newviewModel.Password = viewModel.Password;
                    newviewModel.IsAdmin = viewModel.IsAdmin;
                    newviewModel.CanBeGM = viewModel.CanBeGM;
                    newviewModel.UserId = viewModel.UserId;

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
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,UserViewModel viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    ModelState.Clear();
                    User delviewModel = db.Users.Find(viewModel.UserId);
                    if (delviewModel != null)
                    {
                        db.Users.Remove(delviewModel);
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

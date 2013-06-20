using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Cumis.Filters;
using Splintered_Universe.Models.VM;
using Splintered_Universe.Models.Session;
//using UserAuthentication;


namespace Splintered_Universe
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Login()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Clear();
            Response.Cookies.Clear();
            ViewData["CanExcludeMasterPage"] = true;
            return View("Login");
        }
        /// <summary>
        /// This function verify username and redirect to landing page else it will be on the same login page.
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginUser(LoginViewModel loginViewModel)
        {
            UserViewModel target = SessionUsers.OneUser( (o => o.LoginName.ToString().Trim().ToLower() == loginViewModel.LoginName.ToString().Trim().ToLower() )   );
            if (target != null)
            {
                if (target.Password == loginViewModel.Password)
                {
                    SessionUsers.SetUserSessions(loginViewModel.LoginName.ToString().Trim());
                    // RedirectToAction??
                    return View("../Home/Index");
                }
                else
                {
                    ViewData["IsValidUser"] = false;
                    return View("Login");
                }
            }
            else
            {
                ViewData["IsValidUser"] = false;
                return View("Login");
            }


        }

        /// <summary>
        /// This function clears cache, session information and redirect to login page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {

            Session.Abandon();
            Session.Clear();
            // Invalidate the Cache on the Client Side 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();

            return RedirectToAction("Login", "Login");
        }
    }
}

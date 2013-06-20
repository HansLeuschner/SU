using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Splintered_Universe.Models.Session;
using Splintered_Universe.Models.VM;


namespace Splintered_Universe.Common
{
    public class UserRights
    {
        public static bool UserHasRights( string userRights)
        {
            if (HttpContext.Current.Session["UserSession"] != null)
            {
                UserSessionVM user = ((UserSessionVM)HttpContext.Current.Session["UserSession"]);

                if (user != null)
                {
                    if (userRights == "isAdmin" && user.IsAdmin)
                    {
                        return true;
                    }

                    if (userRights == "isGM" && user.CanBeGM)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;

        }

    }
}
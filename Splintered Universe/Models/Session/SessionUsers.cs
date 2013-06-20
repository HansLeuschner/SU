using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Splintered_Universe.Models;
using Splintered_Universe.Models.VM;

namespace Splintered_Universe.Models.Session
{
    public class SessionUsers
    {

        public static IEnumerable<UserViewModel> AllUsers()
        {
            IEnumerable<UserViewModel> result = HttpContext.Current.Session["Users"] as IEnumerable<UserViewModel>;

            if (result == null)
            {
                HttpContext.Current.Session["Users"] = result = new SUContext().Users
                    .Select(o => new UserViewModel
                    {
                         Name = o.Name,
                         LoginName = o.LoginName,
                         UserId = o.UserId,
                         CanBeGM = o.CanBeGM,
                         IsAdmin = o.IsAdmin,
                         Password = o.Password
                    }).ToList();
            }

            return result;
        }

        public static UserViewModel OneUser(Func<UserViewModel, bool> predicate)
        {
            return AllUsers().Where(predicate).FirstOrDefault();
        }

        public static void Update(UserViewModel user)
        {
            UserViewModel editable = OneUser(o => o.UserId == user.UserId);

            if (editable != null)
            {
                editable.Name = user.Name;
                editable.LoginName = user.LoginName;
                editable.IsAdmin = user.IsAdmin;
                editable.CanBeGM = user.CanBeGM;
                editable.Password = user.Password;
            }
        }

        public static void SetUserSessions(string qcb_stUsrName)
        {
            try
            {
                SUContext db = new SUContext();
                UserSessionVM userSession = new UserSessionVM();
                //For geting user id 
                var getUserId = from userId in db.Users
                             where userId.LoginName== qcb_stUsrName
                             select new {userId.LoginName, userId.UserId,userId.Name, userId.CanBeGM, userId.IsAdmin };
                foreach (var item in getUserId)
                {
                    userSession.UserId = item.UserId;
                    userSession.LoginName = item.LoginName;
                    userSession.UserName = item.Name;
                    userSession.CanBeGM = item.CanBeGM;
                    userSession.IsAdmin = item.IsAdmin;
                }
                using (SUContext context = new SUContext())
                {                    
                    //userSession.UserRights = context.sp_GetSecAttribs(qcb_stUsrName).ToList();
                    var token = new ObjectParameter("f_uidSessionToken", typeof(string));
                    //var result = context.sp_SessCreateToken(qcb_stUsrName, token);

                    if (token.Value != null)
                        userSession.UserSessionToken = (string)token.Value;

                    HttpContext.Current.Session["UserSession"] = userSession;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Splintered_Universe.Models.VM
{
    public class UserSessionVM
    {
        public int UserId { get; set; }
        public string SessionKey { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public bool CanBeGM { get; set; }
        public string UserSessionToken { get; set; }
    }
}
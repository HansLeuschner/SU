using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Splintered_Universe.Models.VM
{
    /// <summary>
    /// MODEL TO SET LOGIN DETAILS OF USER AND USED IN LOGIN VIEW.
    /// </summary>
    public class LoginViewModel
    {
        [Required, DisplayName("Login Name")]
        public string LoginName { get; set; }

        [Required, DisplayName("User Name")]
        public string UserName { get; set; }

        [Required, DisplayName("Password"), DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DisplayName("Is An Administrator")]
        public bool IsAdmin { get; set; }

        [DisplayName("Can Be A Game Master")]
        public bool CanBeGM { get; set; }
    }
}
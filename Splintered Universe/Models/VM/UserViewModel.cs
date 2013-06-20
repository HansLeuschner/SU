using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Splintered_Universe.Models.VM
{
    public class UserViewModel
    {
        [DisplayName("User ID")]
        public int UserId { get; set; }
        [Required, DisplayName("Login Name")]
        public string LoginName { get; set; }
        [Required, DisplayName("User Name")] 
        public string Name { get; set; }
        [Required, DisplayName("Password"), DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Is An Administrator")]
        public bool IsAdmin { get; set; }
        [DisplayName("Can Be A Game Master")]
        public bool CanBeGM { get; set; }
    }
}

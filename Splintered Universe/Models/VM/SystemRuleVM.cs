using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Splintered_Universe.Models.VM
{
    public class SystemRuleVM
    {
        [DisplayName("System Rules ID")]
        public int SystemRuleId { get; set; }
        [Required, DisplayName("Name")]
        public string SystemName { get; set; }
        [Required, DisplayName("Description")] 
        public string Description { get; set; }
    }
}

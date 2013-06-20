using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Splintered_Universe.Models.VM
{
    public class SystemActionCategoryVM
    {
        [Required, DisplayName("Skill Action Category ID")]
        public int SystemActionCategoryId { get; set; }

        [Required, DisplayName("Name")]
        public string Name { get; set; }

        [Required, DisplayName("System")]
        public int SystemRuleSystemRuleId { get; set; }

        [DisplayName("System")]
        public string SystemRuleSystemName { get; set; }

    }
}

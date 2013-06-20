using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Splintered_Universe.Models.VM
{
    public class SystemStatVM
    {
        [Required, DisplayName("Stat ID")]
        public int SystemStatId { get; set; }

        [Required, DisplayName("Name")]
        public string Name { get; set; }

        [Required, DisplayName("Abbreviation")]
        public string Abbreviation { get; set; }

        [Required, DisplayName("System")]
        public int SystemRuleSystemRuleId { get; set; }

        [DisplayName("System")]
        public string SystemRuleSystemName { get; set; }

    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Splintered_Universe.Models.VM
{
    public class SystemStatLevelVM
    {
        [Required, DisplayName("Stat Level ID")]
        public int SystemStatLevelId { get; set; }

        [Required, DisplayName("Level"), DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int Level { get; set; }

        [Required, DisplayName("Description")]
        public string Description { get; set; }

        [Required, DisplayName("Stat")]
        public int SystemStatSystemStatId { get; set; }

        [DisplayName("Stat")]
        public string SystemStatName { get; set; }

    }
}

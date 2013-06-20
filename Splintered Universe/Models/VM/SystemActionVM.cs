using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Splintered_Universe.Models.VM
{
    public class SystemActionVM
    {
        [Required, DisplayName("Skill Action ID")]
        public int SystemActionId { get; set; }

        [Required, DisplayName("Name")]
        public string Name { get; set; }

        [Required, DisplayName("Description")]
        public string Description { get; set; }

        [Required, DisplayName("GM Notes")]
        public string GMNotes { get; set; }

        [Required, DisplayName("Base AP Cost"), DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int BaseAPCost { get; set; }

        [Required, DisplayName("Min AP Cost"), DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int MinAPCost { get; set; }

        [Required, DisplayName("Fatigue Cost"), DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int FatigueCost { get; set; }

        [Required, DisplayName("Category")]
        public int SystemActionCategorySystemActionCategoryId { get; set; }

        [DisplayName("Category")]
        public string SystemActionCategoryName { get; set; }

    }
}

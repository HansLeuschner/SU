using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Splintered_Universe.Models.VM
{
    public class CampaignVM
    {
        [DisplayName("Campaign ID")]
        public int CampaignId { get; set; }
        [Required, DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [Required, DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy MMM dd}")]
        public DateTime? StartDate { get; set; }
        [DisplayName("End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy MMM dd}")]
        public DateTime? EndDate { get; set; }
        [DisplayName("Game Master")]
        public int GameMasterId { get; set; }
        [DisplayName("System Rules")]
        public int SystemId { get; set; }
        [DisplayName("Game Master")]
        public string GameMaster { get; set; }
        [DisplayName("System Rules")]
        public string System { get; set; }
    }
}

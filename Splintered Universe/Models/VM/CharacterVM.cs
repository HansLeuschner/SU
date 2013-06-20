using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Splintered_Universe.Models.VM
{
    public class CharacterVM
    {
        [DisplayName("Character ID")]
        public int CharacterId { get; set; }
        [Required, DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Player")]
        public int PlayerId { get; set; }
        [DisplayName("Campaign")]
        public int CampaignId { get; set; }
        [DisplayName("Game Master")]
        public string Player { get; set; }
        [DisplayName("Campaign")]
        public string Campaign { get; set; }
    }
}

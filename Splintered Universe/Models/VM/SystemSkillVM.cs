using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Splintered_Universe.Models.VM
{
    public class SystemSkillVM
    {
        [Required, DisplayName("Skill ID")]
        public int SystemSkillId { get; set; }

        [Required, DisplayName("Name")]
        public string Name { get; set; }
        
        [DisplayName("Description")]
        public string Description { get; set; }
        
        [Required, DisplayName("Difficulty")]
        public SkillDifficulty Difficulty { get; set; }
        
        [DisplayName("Game Master Notes")]
        public string GMNotes { get; set; }
                
        [Required, DisplayName("Category")]
        public int SystemSkillCategorySystemSkillCategoryId { get; set; }
        
        [DisplayName("Category")]
        public string SystemSkillCategoryName { get; set; }

        [Required, DisplayName("Relevant Stat")]
        public int SystemStatSystemStatId { get; set; }

        [DisplayName("Relevant Stat")]
        public string SystemStatName { get; set; }

        [Required, DisplayName("Related Action")]
        public int SystemActionSystemActionId { get; set; }

        [DisplayName("Related Action")]
        public string SystemActionName { get; set; }
    }
}

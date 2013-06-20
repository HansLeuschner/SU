//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Splintered_Universe
{
    using System;
    using System.Collections.Generic;
    
    public partial class Campaign
    {
        public Campaign()
        {
            this.Characters = new HashSet<Character>();
            this.CombatEncounters = new HashSet<CombatEncounter>();
        }
    
        public int CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int SystemRuleSystemRuleId { get; set; }
        public int UserUserId { get; set; }
    
        public virtual ICollection<Character> Characters { get; set; }
        public virtual SystemRule SystemRule { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CombatEncounter> CombatEncounters { get; set; }
    }
}
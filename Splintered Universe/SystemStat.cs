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
    
    public partial class SystemStat
    {
        public SystemStat()
        {
            this.SystemSkills = new HashSet<SystemSkill>();
            this.SystemStatLevels = new HashSet<SystemStatLevel>();
        }
    
        public int SystemStatId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int SystemRuleSystemRuleId { get; set; }
    
        public virtual SystemRule SystemRule { get; set; }
        public virtual ICollection<SystemSkill> SystemSkills { get; set; }
        public virtual ICollection<SystemStatLevel> SystemStatLevels { get; set; }
    }
}

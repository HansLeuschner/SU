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
    
    public partial class SystemStatLevel
    {
        public int SystemStatLevelId { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public int SystemStatSystemStatId { get; set; }
    
        public virtual SystemStat SystemStat { get; set; }
    }
}

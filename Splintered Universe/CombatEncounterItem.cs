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
    
    public partial class CombatEncounterItem
    {
        public int CombatEncounterItemId { get; set; }
        public int CombatEncounterCombatEncounterId { get; set; }
        public string StartAp { get; set; }
        public string DurationAP { get; set; }
        public int SystemActionSystemActionId { get; set; }
        public string Description { get; set; }
        public Nullable<int> CharacterCharacterId { get; set; }
        public string CombatantName { get; set; }
        public string Target { get; set; }
    
        public virtual SystemAction SystemAction { get; set; }
        public virtual CombatEncounter CombatEncounter { get; set; }
        public virtual Character Character { get; set; }
    }
}

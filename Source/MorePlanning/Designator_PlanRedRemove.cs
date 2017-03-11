using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanRedRemove : Designator_PlanBase
    {
        public Designator_PlanRedRemove() : base(DefDatabase<PlanningDesignationDef>.GetNamed("PlanRed", true), DesignateMode.Remove)
        {
            this.defaultLabel = "MorePlanning.PlanRedRemove".Translate();
            this.defaultDesc = "DesignatorPlanRemoveDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolRedRemove", true);
            this.hotKey = KeyBindingDefOf.Misc5;
        }
    }
}

using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanWhiteRemove : Designator_PlanBase
    {
        public Designator_PlanWhiteRemove() : base(DefDatabase<PlanningDesignationDef>.GetNamed("Plan", true), DesignateMode.Remove)
        {
            this.defaultLabel = "MorePlanning.PlanWhiteRemove".Translate();
            this.defaultDesc = "DesignatorPlanRemoveDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolWhiteRemove", true);
            this.hotKey = KeyBindingDefOf.Misc10;
        }
    }
}

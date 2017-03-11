using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanYellowRemove : Designator_PlanBase
    {
        public Designator_PlanYellowRemove() : base(DefDatabase<PlanningDesignationDef>.GetNamed("PlanYellow", true), DesignateMode.Remove)
        {
            this.defaultLabel = "MorePlanning.PlanYellowRemove".Translate();
            this.defaultDesc = "DesignatorPlanRemoveDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolYellowRemove", true);
            this.hotKey = KeyBindingDefOf.Misc12;
        }
    }
}

using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanWhiteAdd : Designator_PlanBase
    {
        public Designator_PlanWhiteAdd() : base(DefDatabase<PlanningDesignationDef>.GetNamed("Plan", true), DesignateMode.Add)
        {
            this.defaultLabel = "MorePlanning.PlanWhite".Translate();
            this.defaultDesc = "MorePlanning.PlanDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolWhite", true);
            this.hotKey = KeyBindingDefOf.Misc9;
        }
    }
}

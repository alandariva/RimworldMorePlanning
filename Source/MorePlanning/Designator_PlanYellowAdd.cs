using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanYellowAdd : Designator_PlanBase
    {
        public Designator_PlanYellowAdd() : base(DefDatabase<PlanningDesignationDef>.GetNamed("PlanYellow", true), DesignateMode.Add)
        {
            this.defaultLabel = "MorePlanning.PlanYellow".Translate();
            this.defaultDesc = "MorePlanning.PlanDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolYellow", true);
            this.hotKey = KeyBindingDefOf.Misc1;
        }
    }
}

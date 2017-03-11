using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanGreenAdd : Designator_PlanBase
    {
        public Designator_PlanGreenAdd() : base(DefDatabase<PlanningDesignationDef>.GetNamed("PlanGreen", true), DesignateMode.Add)
        {
            this.defaultLabel = "MorePlanning.PlanGreen".Translate();
            this.defaultDesc = "MorePlanning.PlanDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolGreen", true);
            this.hotKey = KeyBindingDefOf.Misc3;
        }
    }
}

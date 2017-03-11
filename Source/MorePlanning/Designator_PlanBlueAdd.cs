using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanBlueAdd : Designator_PlanBase
    {
        public Designator_PlanBlueAdd() : base(DefDatabase<PlanningDesignationDef>.GetNamed("PlanBlue", true), DesignateMode.Add)
        {
            this.defaultLabel = "MorePlanning.PlanBlue".Translate();
            this.defaultDesc = "MorePlanning.PlanDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolBlue", true);
            this.hotKey = KeyBindingDefOf.Misc3;
        }
    }
}

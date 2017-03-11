using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanRedAdd : Designator_PlanBase
    {
        public Designator_PlanRedAdd() : base(DefDatabase<PlanningDesignationDef>.GetNamed("PlanRed", true), DesignateMode.Add)
        {
            this.defaultLabel = "MorePlanning.PlanRed".Translate();
            this.defaultDesc = "MorePlanning.PlanDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolRed", true);
            this.hotKey = KeyBindingDefOf.Misc2;
        }
    }
}

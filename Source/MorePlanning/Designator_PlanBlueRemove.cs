using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanBlueRemove : Designator_PlanBase
    {
        public Designator_PlanBlueRemove() : base(PlanningDesignationDef.ColorBlue, DesignateMode.Remove)
        {
            this.defaultLabel = "MorePlanning.PlanBlueRemove".Translate();
            this.defaultDesc = "DesignatorPlanRemoveDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolBlueRemove", true);
            this.hotKey = KeyBindingDefOf.Misc6;
        }
    }
}

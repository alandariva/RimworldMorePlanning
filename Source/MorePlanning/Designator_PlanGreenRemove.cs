using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanGreenRemove : Designator_PlanBase
    {
        public Designator_PlanGreenRemove() : base(PlanningDesignationDef.ColorGreen, DesignateMode.Remove)
        {
            this.defaultLabel = "MorePlanning.PlanGreenRemove".Translate();
            this.defaultDesc = "DesignatorPlanRemoveDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolGreenRemove", true);
            this.hotKey = KeyBindingDefOf.Misc11;
        }
    }
}

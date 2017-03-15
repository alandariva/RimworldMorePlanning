using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanGrayRemove : Designator_PlanBase
    {
        public Designator_PlanGrayRemove() : base(PlanningDesignationDef.ColorGray, DesignateMode.Remove)
        {
            this.defaultLabel = "MorePlanning.PlanGrayRemove".Translate();
            this.defaultDesc = "DesignatorPlanRemoveDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolGrayRemove", true);
            this.hotKey = KeyBindingDefOf.Misc10;
        }
    }
}

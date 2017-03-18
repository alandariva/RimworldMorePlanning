using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanGreenAdd : Designator_PlanBase
    {
        public Designator_PlanGreenAdd() : base(PlanningDesignationDef.ColorGreen, DesignateMode.Add)
        {
            this.defaultLabel = "MorePlanning.PlanGreen".Translate();
            this.defaultDesc = "MorePlanning.PlanDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolGreen", true);
            this.hotKey = KeyBindingDefOf.Misc9;
        }
    }
}

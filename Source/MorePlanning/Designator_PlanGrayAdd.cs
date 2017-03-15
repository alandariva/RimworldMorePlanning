using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanGrayAdd : Designator_PlanBase
    {
        public Designator_PlanGrayAdd() : base(PlanningDesignationDef.ColorGray, DesignateMode.Add)
        {
            this.defaultLabel = "MorePlanning.PlanGray".Translate();
            this.defaultDesc = "MorePlanning.PlanDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolGray", true);
            this.hotKey = KeyBindingDefOf.Misc8;
        }
    }
}

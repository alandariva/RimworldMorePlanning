using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class Designator_PlanRemoveAll : Designator_PlanBase
    {
        public Designator_PlanRemoveAll() : base(-1, DesignateMode.Add)
        {
            this.defaultLabel = "DesignatorPlanRemove".Translate();
            this.defaultDesc = "DesignatorPlanRemoveDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanToolRemoveAll", true);
            this.soundSucceeded = SoundDefOf.DesignatePlanAdd;
            this.hotKey = KeyBindingDefOf.Misc7;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!c.InBounds(base.Map))
            {
                return false;
            }
            if (c.InNoBuildEdgeArea(base.Map))
            {
                return "TooCloseToMapEdge".Translate();
            }
            if (!HasAnyPlanDesignationAt(c))
            {
                return false;
            }
            return true;
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            RemoveAllPlanDesignationAt(c);
        }
    }
}

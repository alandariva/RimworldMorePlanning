using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using System;
using UnityEngine;

namespace MorePlanning
{
    public class Designator_PlanCut : Designator_PlanCopy
    {

        public Designator_PlanCut()
        {
            this.defaultLabel = "MorePlanning.PlanCut".Translate();
            this.defaultDesc = "MorePlanning.PlanCutDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanCut", true);
        }

        public override void DesignateMultiCell(IEnumerable<IntVec3> cells)
        {
            base.DesignateMultiCell(cells);

            foreach(var cell in cells)
            {
                Utils_Plan.RemoveAllPlanDesignationAt(cell, this.Map);
            }
        }

    }
}

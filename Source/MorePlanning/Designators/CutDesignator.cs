using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using System;
using MorePlanning.Utility;
using UnityEngine;

namespace MorePlanning.Designators
{
    public class CutDesignator : CopyDesignator
    {

        public CutDesignator()
        {
            this.defaultLabel = "MorePlanning.PlanCut".Translate();
            this.defaultDesc = "MorePlanning.PlanCutDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanCut", true);
        }

        public override void RenderHighlight(List<IntVec3> dragCells)
        {
            DesignatorUtility.RenderHighlightOverSelectableCells(this, dragCells);
        }

        public override void DesignateMultiCell(IEnumerable<IntVec3> cells)
        {
            base.DesignateMultiCell(cells);

            foreach(var cell in cells)
            {
                MapUtility.RemoveAllPlanDesignationAt(cell, this.Map);
            }
        }

    }
}

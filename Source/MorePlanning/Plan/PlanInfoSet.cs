using System.Collections.Generic;
using System.Linq;
using MorePlanning.Utility;
using UnityEngine;
using Verse;
using Resources = MorePlanning.Common.Resources;

namespace MorePlanning.Plan
{
    public class PlanInfoSet
    {
        protected List<PlanInfo> PlanDesignationInfo;

        public IntVec2 Size { get; private set; }

        public PlanInfoSet(List<PlanInfo> planDesignationInfo)
        {
            PlanDesignationInfo = planDesignationInfo;

            int left = planDesignationInfo.Min(plan => plan.Pos.x);
            int top = planDesignationInfo.Max(plan => plan.Pos.z);
            int right = planDesignationInfo.Max(plan => plan.Pos.x);
            int bottom = planDesignationInfo.Min(plan => plan.Pos.z);

            // Size of selection (+1 because selection on same cell will have 1 size)
            Size = new IntVec2(right - left + 1, top - bottom + 1);
        }

        public void Draw(IntVec3 intVec, Map map)
        {
            List<IntVec3> cells = new List<IntVec3>();

            foreach (var planDesInfo in PlanDesignationInfo)
            {
                IntVec3 pos = planDesInfo.Pos + intVec;

                if (pos.InNoBuildEdgeArea(map))
                {
                    continue;
                }

                Vector3 position = pos.ToVector3ShiftedWithAltitude(AltitudeLayer.MetaOverlays.AltitudeFor());
                Graphics.DrawMesh(MeshPool.plane10, position, Quaternion.identity, Resources.PlanMatColor[planDesInfo.Color], 0);
                cells.Add(pos);
            }

            GenDraw.DrawFieldEdges(cells);
        }

        public void DesignateFromOrigin(IntVec3 c, Map map)
        {
            var planDef = DefDatabase<PlanDesignationDef>.GetNamed("Plan");
            foreach (var planDesInfo in PlanDesignationInfo)
            {
                IntVec3 pos = planDesInfo.Pos + c;

                if (pos.InNoBuildEdgeArea(map))
                {
                    continue;
                }

                MapUtility.RemoveAllPlanDesignationAt(pos, map);
                map.designationManager.AddDesignation(new PlanDesignation(pos, planDef, planDesInfo.Color));
            }
        }

        public void Rotate(RotationDirection rotationDirection)
        {
            foreach (var planDesInfo in PlanDesignationInfo)
            {
                planDesInfo.Pos = planDesInfo.Pos.RotatedBy(rotationDirection == RotationDirection.Clockwise ? Rot4.East : Rot4.West);
            }
        }

    }
}

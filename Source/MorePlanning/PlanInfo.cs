using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public class PlanInfo
    {
        protected List<PlanDesignationInfo> planDesignationInfo;

        private IntVec2 size;
        public IntVec2 Size { get => size; private set => size = value; }

        public PlanInfo(List<PlanDesignationInfo> planDesignationInfo)
        {
            this.planDesignationInfo = planDesignationInfo;

            int left = planDesignationInfo.Min(plan => plan.Pos.x);
            int top = planDesignationInfo.Max(plan => plan.Pos.z);
            int right = planDesignationInfo.Max(plan => plan.Pos.x);
            int bottom = planDesignationInfo.Min(plan => plan.Pos.z);

            // Size of selection (+1 because selection on same cell will have 1 size)
            this.size = new IntVec2(right - left + 1, top - bottom + 1);
        }

        public void Draw(IntVec3 intVec)
        {
            List<IntVec3> cells = new List<IntVec3>();

            var planDef = DefDatabase<PlanningDesignationDef>.GetNamed("Plan", true);
            foreach (var planDesInfo in planDesignationInfo)
            {
                IntVec3 pos = planDesInfo.Pos + intVec;
                Vector3 position = pos.ToVector3ShiftedWithAltitude(Altitudes.AltitudeFor(AltitudeLayer.MetaOverlays));
                Graphics.DrawMesh(MeshPool.plane10, position, Quaternion.identity, planDef.iconMatColor[planDesInfo.Color], 0);
                cells.Add(pos);
            }

            GenDraw.DrawFieldEdges(cells);
        }

        public void DesignateFromOrigin(IntVec3 c, Map map)
        {
            var planDef = DefDatabase<PlanningDesignationDef>.GetNamed("Plan", true);
            foreach (var planDesInfo in planDesignationInfo)
            {
                IntVec3 pos = planDesInfo.Pos + c;
                Utils_Plan.RemoveAllPlanDesignationAt(pos, map);
                map.designationManager.AddDesignation(new PlanDesignation(pos, planDef, planDesInfo.Color));
            }
        }

        public void Rotate(RotationDirection rotationDirection)
        {
            foreach (var planDesInfo in planDesignationInfo)
            {
                if (rotationDirection == RotationDirection.Clockwise)
                {
                    planDesInfo.Pos = planDesInfo.Pos.RotatedBy(Rot4.East);
                }
                else
                {
                    planDesInfo.Pos = planDesInfo.Pos.RotatedBy(Rot4.West);
                }
            }
        }

    }
}

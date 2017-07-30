using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using UnityEngine;
using Verse.Sound;
using System;

namespace MorePlanning
{
    public class Designator_PlanAdd : Designator_PlanBase
    {
        public Designator_PlanAdd() : base()
        {
            this.defaultLabel = "DesignatorPlan".Translate();
            this.defaultDesc = "MorePlanning.PlanDesc".Translate();

            this.soundSucceeded = SoundDefOf.DesignatePlanAdd;
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
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) == false)
            {
                if (Utils_Plan.HasAnyPlanDesignationAt(c, this.Map))
                {
                    return false;
                }
            }
            return true;
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            Utils_Plan.RemoveAllPlanDesignationAt(c, this.Map);
            base.Map.designationManager.AddDesignation(new PlanDesignation(c, this.desDef, MorePlanningMod.Instance.SelectedColor));
        }

        public override void DrawMouseAttachments()
        {
            Vector2 mousePosition = Event.current.mousePosition;
            float num = mousePosition.y + 12f;

            Graphics.DrawTexture(new Rect(mousePosition.x + 12f, num, 32f, 32f), Resources.Plan, this.iconTexCoords, 0, 1, 0, 1, PlanColorManager.planColor[MorePlanningMod.Instance.SelectedColor]);
        }

        protected override void CustomGizmoOnGUI(Vector2 topLeft, Rect rect)
        {
            Graphics.DrawTexture(new Rect(rect), Resources.Plan, this.iconTexCoords, 0, 1, 0, 1, PlanColorManager.planColor[MorePlanningMod.Instance.SelectedColor]);
        }
    }
}

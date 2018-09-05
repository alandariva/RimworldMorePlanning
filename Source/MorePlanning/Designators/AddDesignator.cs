using MorePlanning.Plan;
using MorePlanning.Utility;
using RimWorld;
using UnityEngine;
using Verse;
using Resources = MorePlanning.Common.Resources;

namespace MorePlanning.Designators
{
    public class AddDesignator : PlanBaseDesignator
    {
        public AddDesignator()
        {
            defaultLabel = "DesignatorPlan".Translate();
            defaultDesc = "MorePlanning.PlanDesc".Translate();

            soundSucceeded = SoundDefOf.Designate_PlanAdd;
            hotKey = KeyBindingDefOf.Designator_Cancel;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!c.InBounds(Map))
            {
                return false;
            }
            if (c.InNoBuildEdgeArea(Map))
            {
                return "TooCloseToMapEdge".Translate();
            }
            if (!MorePlanningMod.Instance.OverrideColors)
            {
                if (MapUtility.HasAnyPlanDesignationAt(c, Map))
                {
                    return false;
                }
            }
            return true;
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            MapUtility.RemoveAllPlanDesignationAt(c, Map);
            Map.designationManager.AddDesignation(new PlanDesignation(c, DesDef, MorePlanningMod.Instance.SelectedColor));
        }

        public override void DrawMouseAttachments()
        {
            Vector2 mousePosition = Event.current.mousePosition;
            float num = mousePosition.y + 12f;

            Graphics.DrawTexture(new Rect(mousePosition.x + 12f, num, 32f, 32f), Resources.Plan, iconTexCoords, 0, 1, 0, 1, PlanColorManager.PlanColor[MorePlanningMod.Instance.SelectedColor]);
        }

        protected override void CustomGizmoOnGui(Vector2 topLeft, Rect rect)
        {
            Graphics.DrawTexture(new Rect(rect), Resources.Plan, iconTexCoords, 0, 1, 0, 1, PlanColorManager.PlanColor[MorePlanningMod.Instance.SelectedColor]);
        }
    }
}

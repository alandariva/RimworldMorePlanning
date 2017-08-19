using RimWorld;
using System;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace MorePlanning
{
    public class Designator_PlanRemove : Designator_PlanBase
    {

        public Designator_PlanRemove()
        {
            this.defaultLabel = "DesignatorPlanRemove".Translate();
            this.defaultDesc = "DesignatorPlanRemoveDesc".Translate();

            this.soundSucceeded = SoundDefOf.DesignatePlanRemove;
            this.hotKey = KeyBindingDefOf.DesignatorDeconstruct;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!c.InBounds(base.Map))
            {
                return false;
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                return Utils_Plan.HasAnyPlanDesignationAt(c, this.Map);
            }

            return Utils_Plan.HasPlanDesignationAt(c, this.Map, MorePlanningMod.Instance.SelectedColor);
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            Utils_Plan.RemoveAllPlanDesignationAt(c, this.Map);
        }

        public override void DrawMouseAttachments()
        {
            Vector2 mousePosition = Event.current.mousePosition;
            float num = mousePosition.y + 12f;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                Widgets.DrawTextureFitted(new Rect(mousePosition.x + 12f, num, 32f, 32f), Resources.PlanToolRemoveAll, this.iconDrawScale);
            }
            else
            {
                Graphics.DrawTexture(new Rect(mousePosition.x + 12f, num, 32f, 32f), Resources.Plan, this.iconTexCoords, 0, 1, 0, 1, PlanColorManager.planColor[MorePlanningMod.Instance.SelectedColor]);
                Widgets.DrawTextureFitted(new Rect(mousePosition.x + 12f, num, 32f, 32f), Resources.RemoveIcon, this.iconDrawScale);
            }
        }

        protected override void CustomGizmoOnGUI(Vector2 topLeft, Rect rect)
        {
            Rect position = new Rect(0f, 0f, this.iconProportions.x, this.iconProportions.y);
            float num;
            if (position.width / position.height < rect.width / rect.height)
            {
                num = rect.height / position.height;
            }
            else
            {
                num = rect.width / position.width;
            }
            num *= 0.85f;
            position.width *= num;
            position.height *= num;
            position.x = rect.x + rect.width / 2f - position.width / 2f;
            position.y = rect.y + rect.height / 2f - position.height / 2f;

            if (Event.current.type == EventType.Repaint)
            {
                Graphics.DrawTexture(position, Resources.Plan, this.iconTexCoords, 0, 1, 0, 1, PlanColorManager.planColor[MorePlanningMod.Instance.SelectedColor]);
                Widgets.DrawTextureFitted(new Rect(rect), Resources.RemoveIcon, this.iconDrawScale * 0.85f, this.iconProportions, this.iconTexCoords);
            }
        }

    }
}

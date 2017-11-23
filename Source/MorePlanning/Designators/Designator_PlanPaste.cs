using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using UnityEngine;
using Verse.Sound;

namespace MorePlanning
{
    public class Designator_PlanPaste : Designator_Base
    {
        protected static float middleMouseDownTime;

        public static PlanInfo CurrentPlanCopy { get; set; }

        public override int DraggableDimensions
        {
            get
            {
                return 0;
            }
        }

        public override bool DragDrawMeasurements
        {
            get
            {
                return false;
            }
        }

        public Designator_PlanPaste()
        {
            this.defaultLabel = "MorePlanning.PlanPaste".Translate();
            this.defaultDesc = "MorePlanning.PlanPasteDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/PlanPaste", true);
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            return true;
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            //bool somethingSucceeded = false;

            if (CurrentPlanCopy != null)
            {
                CurrentPlanCopy.DesignateFromOrigin(c, this.Map);
            }
        }

        public override void SelectedUpdate()
        {
            GenDraw.DrawNoBuildEdgeLines();
            if (!ArchitectCategoryTab.InfoRect.Contains(UI.MousePositionOnUIInverted))
            {
                if (CurrentPlanCopy != null)
                {
                    IntVec3 intVec = UI.MouseCell();
                    CurrentPlanCopy.Draw(intVec, this.Map);
                }
            }
        }

        public override void DoExtraGuiControls(float leftX, float bottomY)
        {
            Rect winRect = new Rect(leftX, bottomY - 90f, 200f, 90f);
            this.HandleRotationShortcuts();
            Find.WindowStack.ImmediateWindow(73095, winRect, WindowLayer.GameUI, delegate
            {
                RotationDirection rotationDirection = RotationDirection.None;
                Text.Anchor = TextAnchor.MiddleCenter;
                Text.Font = GameFont.Medium;
                Rect rect = new Rect(winRect.width / 2f - 64f - 5f, 15f, 64f, 64f);
                if (Widgets.ButtonImage(rect, TexUI.RotLeftTex))
                {
                    SoundDefOf.AmountDecrement.PlayOneShotOnCamera();
                    rotationDirection = RotationDirection.Counterclockwise;
                    Event.current.Use();
                }
                Widgets.Label(rect, KeyBindingDefOf.DesignatorRotateLeft.MainKeyLabel);
                Rect rect2 = new Rect(winRect.width / 2f + 5f, 15f, 64f, 64f);
                if (Widgets.ButtonImage(rect2, TexUI.RotRightTex))
                {
                    SoundDefOf.AmountIncrement.PlayOneShotOnCamera();
                    rotationDirection = RotationDirection.Clockwise;
                    Event.current.Use();
                }
                Widgets.Label(rect2, KeyBindingDefOf.DesignatorRotateRight.MainKeyLabel);
                if (rotationDirection != RotationDirection.None)
                {
                    CurrentPlanCopy.Rotate(rotationDirection);
                }
                Text.Anchor = TextAnchor.UpperLeft;
                Text.Font = GameFont.Small;
            }, true, false, 1f);
        }

        private void HandleRotationShortcuts()
        {
            RotationDirection rotationDirection = RotationDirection.None;
            if (Event.current.button == 2)
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    Event.current.Use();
                    middleMouseDownTime = Time.realtimeSinceStartup;
                }
                if (Event.current.type == EventType.MouseUp && Time.realtimeSinceStartup - middleMouseDownTime < 0.15f)
                {
                    rotationDirection = RotationDirection.Clockwise;
                }
            }
            if (KeyBindingDefOf.DesignatorRotateRight.KeyDownEvent)
            {
                rotationDirection = RotationDirection.Clockwise;
            }
            if (KeyBindingDefOf.DesignatorRotateLeft.KeyDownEvent)
            {
                rotationDirection = RotationDirection.Counterclockwise;
            }
            if (rotationDirection == RotationDirection.Clockwise)
            {
                SoundDefOf.AmountIncrement.PlayOneShotOnCamera();
                CurrentPlanCopy.Rotate(RotationDirection.Clockwise);
            }
            if (rotationDirection == RotationDirection.Counterclockwise)
            {
                SoundDefOf.AmountDecrement.PlayOneShotOnCamera();
                CurrentPlanCopy.Rotate(RotationDirection.Counterclockwise);
            }
        }

        public override void ProcessInput(Event ev)
        {
            if (CurrentPlanCopy == null)
            {
                Messages.Message("MorePlanning.NoCutCopiedPlan".Translate(), MessageTypeDefOf.RejectInput);
                return;
            }

            base.ProcessInput(ev);
        }

    }
}

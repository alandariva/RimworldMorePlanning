using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using System;
using System.Reflection;
using Verse.Sound;

namespace MorePlanning
{
    public abstract class Designator_PlanBase : Designator_Base
    {
        protected PlanningDesignationDef desDef = null;

        protected int draggableDimensions = 2;

        public override int DraggableDimensions
        {
            get
            {
                return draggableDimensions;
            }
        }

        public override bool DragDrawMeasurements
        {
            get
            {
                return true;
            }
        }

        public Designator_PlanBase()
        {
            this.desDef = DefDatabase<PlanningDesignationDef>.GetNamed("Plan", true);

            /*
            if (DesignateMode.Add == mode)
            {
                this.soundSucceeded = SoundDefOf.DesignatePlanAdd;
            }
            else
            {
                this.soundSucceeded = SoundDefOf.DesignatePlanRemove;
            }
            */
        }

        /*
        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!c.InBounds(base.Map))
            {
                return false;
            }
            if (this.mode == DesignateMode.Add)
            {
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
            }
            else if (this.mode == DesignateMode.Remove && HasThisPlanAt(c, this.desDef) == false)
            {
                return false;
            }
            return true;
        }
        */

            /*
        private bool HasThisPlanAt(IntVec3 c, DesignationDef desDef)
        {
            var desig = base.Map.designationManager.DesignationAt(c, this.desDef);
            if (desig is PlanDesignation)
            {
                return (desig as PlanDesignation).color == this.color;
            }
            return desig != null && this.color == 0;
        }
        */

        /*
        public override void DesignateSingleCell(IntVec3 c)
        {
            if (this.mode == DesignateMode.Add)
            {
                Utils_Plan.RemoveAllPlanDesignationAt(c, this.Map);
                base.Map.designationManager.AddDesignation(new PlanDesignation(c, this.desDef, this.color));
            }
            else if (this.mode == DesignateMode.Remove)
            {
                base.Map.designationManager.DesignationAt(c, this.desDef).Delete();
            }
        }
        */

        public override void SelectedUpdate()
        {
            GenUI.RenderMouseoverBracket();
            GenDraw.DrawNoBuildEdgeLines();
            Designator_PlanningVisibility.PlanningVisibility = true;
        }

        protected abstract void CustomGizmoOnGUI(Vector2 topLeft, Rect rect);

        // copy paste from Command.GizmoOnGUI
        public override GizmoResult GizmoOnGUI(Vector2 topLeft)
        {
            Rect rect = new Rect(topLeft.x, topLeft.y, this.Width, 75f);
            bool flag = false;
            if (Mouse.IsOver(rect))
            {
                flag = true;
                GUI.color = GenUI.MouseoverColor;
            }
            Texture2D badTex = this.icon;
            if (badTex == null)
            {
                badTex = BaseContent.BadTex;
            }
            GUI.DrawTexture(rect, Command.BGTex);
            MouseoverSounds.DoRegion(rect, SoundDefOf.MouseoverCommand);
            GUI.color = this.IconDrawColor;
            // BEGIN EDIT
            //Widgets.DrawTextureFitted(rect, badTex, this.iconDrawScale * 0.85f, this.iconProportions, this.iconTexCoords, this.iconAngle);

            this.CustomGizmoOnGUI(topLeft, rect);

            // END EDIT
            GUI.color = Color.white;
            bool flag2 = false;
            KeyCode keyCode = (this.hotKey != null) ? this.hotKey.MainKey : KeyCode.None;
            if (keyCode != KeyCode.None && !GizmoGridDrawer.drawnHotKeys.Contains(keyCode))
            {
                Rect rect2 = new Rect(rect.x + 5f, rect.y + 5f, rect.width - 10f, 18f);
                Widgets.Label(rect2, keyCode.ToStringReadable());
                GizmoGridDrawer.drawnHotKeys.Add(keyCode);
                if (this.hotKey.KeyDownEvent)
                {
                    flag2 = true;
                    Event.current.Use();
                }
            }
            if (Widgets.ButtonInvisible(rect, false))
            {
                flag2 = true;
            }
            string labelCap = this.LabelCap;
            if (!labelCap.NullOrEmpty())
            {
                float num = Text.CalcHeight(labelCap, rect.width);
                Rect rect3 = new Rect(rect.x, rect.yMax - num + 12f, rect.width, num);
                GUI.DrawTexture(rect3, TexUI.GrayTextBG);
                GUI.color = Color.white;
                Text.Anchor = TextAnchor.UpperCenter;
                Widgets.Label(rect3, labelCap);
                Text.Anchor = TextAnchor.UpperLeft;
                GUI.color = Color.white;
            }
            GUI.color = Color.white;
            if (this.DoTooltip)
            {
                TipSignal tip = this.Desc;
                if (this.disabled && !this.disabledReason.NullOrEmpty())
                {
                    string text = tip.text;
                    tip.text = string.Concat(new string[]
                    {
                        text,
                        "\n\n",
                        "DisabledCommand".Translate(),
                        ": ",
                        this.disabledReason
                    });
                }
                TooltipHandler.TipRegion(rect, tip);
            }
            if (!this.HighlightTag.NullOrEmpty() && (Find.WindowStack.FloatMenu == null || !Find.WindowStack.FloatMenu.windowRect.Overlaps(rect)))
            {
                UIHighlighter.HighlightOpportunity(rect, this.HighlightTag);
            }
            if (flag2)
            {
                if (this.disabled)
                {
                    if (!this.disabledReason.NullOrEmpty())
                    {
                        Messages.Message(this.disabledReason, MessageTypeDefOf.RejectInput);
                    }
                    return new GizmoResult(GizmoState.Mouseover, null);
                }
                if (!TutorSystem.AllowAction(this.TutorTagSelect))
                {
                    return new GizmoResult(GizmoState.Mouseover, null);
                }
                GizmoResult result = new GizmoResult(GizmoState.Interacted, Event.current);
                TutorSystem.Notify_Event(this.TutorTagSelect);
                return result;
            }
            else
            {
                if (flag)
                {
                    return new GizmoResult(GizmoState.Mouseover, null);
                }
                return new GizmoResult(GizmoState.Clear, null);
            }
        }
    }
}

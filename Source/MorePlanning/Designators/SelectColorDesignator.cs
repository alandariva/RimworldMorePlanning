using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using UnityEngine;
using Verse.Sound;
using System;
using MorePlanning.Designators;
using MorePlanning.Plan;
using MorePlanning.Utility;
using Resources = MorePlanning.Common.Resources;

namespace MorePlanning.Designators
{
    public class SelectColorDesignator : BaseDesignator
    {
        protected int color = 0;

        public SelectColorDesignator(int color)
        {
            this.color = color;
            this.defaultLabel = "" + color;
            this.defaultDesc = "MorePlanning.PlanDesc".Translate();
        }

        public override void ProcessInput(Event ev)
        {
            // Show change color option
            List<FloatMenuOption> list = new List<FloatMenuOption>();

            list.Add(new FloatMenuOption("MorePlanning.ChangeColor".Translate(), delegate
            {
                Find.WindowStack.Add(new Dialogs.ColorSelectorDialog(color));
            }, MenuOptionPriority.Default, null, null, 0, null, null));

            Find.WindowStack.Add(new FloatMenu(list));

            // Select color
            MorePlanningMod.Instance.SelectedColor = this.color;

            if (Find.DesignatorManager.SelectedDesignator == null)
            {
                var designatorPlanPaste = MenuUtility.GetPlanningDesignator<Designator_PlanAdd>();
                Find.DesignatorManager.Select(designatorPlanPaste);
            }
        }

        // copy paste from Command.GizmoOnGUI
        public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth)
        {
            Text.Font = GameFont.Tiny;
            Rect rect = new Rect(topLeft.x, topLeft.y, GetWidth(maxWidth), 75f);
            bool flag = false;
            if (Mouse.IsOver(rect))
            {
                flag = true;
                if (!disabled)
                {
                    GUI.color = GenUI.MouseoverColor;
                }
            }
            Texture2D badTex = icon;
            if ((UnityEngine.Object)badTex == (UnityEngine.Object)null)
            {
                badTex = BaseContent.BadTex;
            }
            Material material = (!disabled) ? null : TexUI.GrayscaleGUI;
            GenUI.DrawTextureWithMaterial(rect, BGTex, material, default(Rect));
            MouseoverSounds.DoRegion(rect, SoundDefOf.Mouseover_Command);
            Rect outerRect = rect;
            Vector2 position = outerRect.position;
            float x = iconOffset.x;
            Vector2 size = outerRect.size;
            float x2 = x * size.x;
            float y = iconOffset.y;
            Vector2 size2 = outerRect.size;
            outerRect.position = position + new Vector2(x2, y * size2.y);
            GUI.color = IconDrawColor;
            // BEGIN EDIT
            //Widgets.DrawTextureFitted(outerRect, badTex, iconDrawScale * 0.85f, iconProportions, iconTexCoords, iconAngle, material);
            {
                Rect positionColor = new Rect(0f, 0f, this.iconProportions.x, this.iconProportions.y);
                float num;
                if (positionColor.width / positionColor.height < rect.width / rect.height)
                {
                    num = rect.height / positionColor.height;
                }
                else
                {
                    num = rect.width / positionColor.width;
                }
                num *= this.iconDrawScale * 0.85f;
                positionColor.width *= num;
                positionColor.height *= num;
                positionColor.x = rect.x + rect.width / 2f - positionColor.width / 2f;
                positionColor.y = rect.y + rect.height / 2f - positionColor.height / 2f;

                Widgets.DrawBoxSolid(positionColor, PlanColorManager.planColor[this.color]);

                if (MorePlanningMod.Instance.SelectedColor == this.color)
                {
                    Widgets.DrawTextureFitted(outerRect, Resources.ToolBoxColorSelected, this.iconDrawScale * 0.85f, this.iconProportions, this.iconTexCoords);
                }
                else
                {
                    Widgets.DrawTextureFitted(outerRect, Resources.ToolBoxColor, this.iconDrawScale * 0.85f, this.iconProportions, this.iconTexCoords);
                }
            }
            // END EDIT
            GUI.color = Color.white;
            bool flag2 = false;
            KeyCode keyCode = (hotKey != null) ? hotKey.MainKey : KeyCode.None;
            if (keyCode != 0 && !GizmoGridDrawer.drawnHotKeys.Contains(keyCode))
            {
                Rect rect2 = new Rect(rect.x + 5f, rect.y + 5f, rect.width - 10f, 18f);
                Widgets.Label(rect2, keyCode.ToStringReadable());
                GizmoGridDrawer.drawnHotKeys.Add(keyCode);
                if (hotKey.KeyDownEvent)
                {
                    flag2 = true;
                    Event.current.Use();
                }
            }
            if (Widgets.ButtonInvisible(rect, false))
            {
                flag2 = true;
            }
            string labelCap = LabelCap;
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
            if (DoTooltip)
            {
                TipSignal tip = Desc;
                if (disabled && !disabledReason.NullOrEmpty())
                {
                    string text = tip.text;
                    tip.text = text + "\n\n" + "DisabledCommand".Translate() + ": " + disabledReason;
                }
                TooltipHandler.TipRegion(rect, tip);
            }
            if (!HighlightTag.NullOrEmpty() && (Find.WindowStack.FloatMenu == null || !Find.WindowStack.FloatMenu.windowRect.Overlaps(rect)))
            {
                UIHighlighter.HighlightOpportunity(rect, HighlightTag);
            }
            Text.Font = GameFont.Small;
            if (flag2)
            {
                if (disabled)
                {
                    if (!disabledReason.NullOrEmpty())
                    {
                        Messages.Message(disabledReason, MessageTypeDefOf.RejectInput, false);
                    }
                    return new GizmoResult(GizmoState.Mouseover, null);
                }
                GizmoResult result;
                if (Event.current.button == 1)
                {
                    result = new GizmoResult(GizmoState.OpenedFloatMenu, Event.current);
                }
                else
                {
                    if (!TutorSystem.AllowAction(TutorTagSelect))
                    {
                        return new GizmoResult(GizmoState.Mouseover, null);
                    }
                    result = new GizmoResult(GizmoState.Interacted, Event.current);
                    TutorSystem.Notify_Event(TutorTagSelect);
                }
                return result;
            }
            if (flag)
            {
                return new GizmoResult(GizmoState.Mouseover, null);
            }
            return new GizmoResult(GizmoState.Clear, null);
        }
        
    }

}

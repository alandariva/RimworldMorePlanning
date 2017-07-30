using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using UnityEngine;
using Verse.Sound;
using System;

namespace MorePlanning
{
    public class Designator_SelectColor : Designator_Base
    {
        protected int color = 0;

        public Designator_SelectColor(int color)
        {
            this.color = color;
            this.defaultLabel = "" + color;
            this.defaultDesc = "MorePlanning.PlanDesc".Translate();
        }

        public override void ProcessInput(Event ev)
        {
            if (ev.button == 1)
            {
                List<FloatMenuOption> list = new List<FloatMenuOption>();

                list.Add(new FloatMenuOption("MorePlanning.ChangeColor".Translate(), delegate
                {
                    Find.WindowStack.Add(new Dialog.ColorSelectorDialog());
                }, MenuOptionPriority.Default, null, null, 0, null, null));

                Find.WindowStack.Add(new FloatMenu(list));
            }
            else
            {
                MorePlanningMod.Instance.SelectedColor = this.color;

                if (Find.DesignatorManager.SelectedDesignator == null)
                {
                    var designatorPlanPaste = Utils_Menu.GetPlanningDesignator<Designator_PlanAdd>();
                    Find.DesignatorManager.Select(designatorPlanPaste);
                }
            }
        }

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
            //Widgets.DrawTextureFitted(new Rect(rect), badTex, this.iconDrawScale * 0.85f, this.iconProportions, this.iconTexCoords);

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
                num *= this.iconDrawScale * 0.85f;
                position.width *= num;
                position.height *= num;
                position.x = rect.x + rect.width / 2f - position.width / 2f;
                position.y = rect.y + rect.height / 2f - position.height / 2f;

                Widgets.DrawBoxSolid(position, PlanColorManager.planColor[this.color]);

                if (MorePlanningMod.Instance.SelectedColor == this.color)
                {
                    Widgets.DrawTextureFitted(new Rect(rect), Resources.ToolBoxColorSelected, this.iconDrawScale * 0.85f, this.iconProportions, this.iconTexCoords);
                }
                else
                {
                    Widgets.DrawTextureFitted(new Rect(rect), Resources.ToolBoxColor, this.iconDrawScale * 0.85f, this.iconProportions, this.iconTexCoords);
                }
            }

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
                        Messages.Message(this.disabledReason, MessageSound.RejectInput);
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

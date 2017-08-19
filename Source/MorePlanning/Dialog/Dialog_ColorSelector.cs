using System;
using HugsLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Reflection;
using HugsLib.Settings;
using UnityEngine;

namespace MorePlanning.Dialog
{
    public class Dialog_ColorSelector : Window
    {
        protected PlanColor color;

        protected string inputColorHex;

        protected string hexColorBefore;

        protected float slider;

        protected float S;
        protected float V;

        protected int numColor;

        protected bool acceptColor = false;

        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(416f, 292f);
            }
        }

        public Dialog_ColorSelector(int numColor)
        {
            this.numColor = numColor;
            this.color = new PlanColor(PlanColorManager.planColor[numColor]);

            this.inputColorHex = this.color.HexColor;
            this.slider = this.color.H;
            this.S = this.color.S;
            this.V = this.color.V;

            this.hexColorBefore = this.inputColorHex;
            this.forcePause = true;
            this.doCloseX = true;
            this.closeOnEscapeKey = true;
            this.absorbInputAroundWindow = true;
            this.closeOnClickedOutside = true;
        }

        public override void PreClose()
        {
            if (acceptColor == false)
            {
                PlanColorManager.ChangeColor(numColor, this.hexColorBefore);
            }
        }

        public override void DoWindowContents(Rect inRect)
        {
            Color beforeColor = this.color.ObjColor;

            Rect colorSB = new Rect(0, 0, 10, 10);
            colorSB.center = new Vector2(256 * S, 256 - 256 * V);

            if (GUI.RepeatButton(new Rect(0, 0, 256, 256), ""))
            {
                this.color.S = (Event.current.mousePosition.x - inRect.x) / 256f;
                this.color.V = 1f - (Event.current.mousePosition.y - inRect.y) / 256f;
            }
            Widgets.DrawBoxSolid(new Rect(0, 0, 256, 256), this.color.ObjColorH);
            GUI.DrawTexture(new Rect(0, 0, 256, 256), Resources.ColorPickerOverlay);
            GUI.DrawTexture(colorSB, Resources.ColorPickerSelect);

            GUI.DrawTexture(new Rect(275, 0, 19, 256), Resources.HsvSlider);

            float newSlider = GUI.VerticalSlider(new Rect(264f, 0f, 11, 256f), slider, 1, 0);

            Widgets.DrawBoxSolid(new Rect(305, 0, 76, 76), this.color.ObjColor);

            string textHex = Widgets.TextField(new Rect(305f, 91f, 76f, 23f), this.inputColorHex);

            bool defaultColorClicked = Widgets.ButtonText(new Rect(305f, 128f, 76f, 50f), "MorePlanning.DefaultColor".Translate());
            bool okClicked = Widgets.ButtonText(new Rect(305f, 234f, 76f, 23f), "MorePlanning.Ok".Translate());

            if (newSlider != slider)
            {
                this.color.H = newSlider;
                this.slider = newSlider;
            }

            bool colorHexChanged = false;
            if (textHex != this.inputColorHex)
            {
                this.color.HexColor = "#" + textHex;
                this.inputColorHex = textHex;
                colorHexChanged = true;
            }

            if (defaultColorClicked)
            {
                this.color.HexColor = "#" + PlanColorManager.defaultColors[numColor];
            }

            if (okClicked)
            {
                this.acceptColor = true;
                this.Close();
            }

            if (!beforeColor.Equals(this.color.ObjColor))
            {
                slider = this.color.H;
                this.S = this.color.S;
                this.V = this.color.V;
                if (colorHexChanged == false)
                {
                    this.inputColorHex = this.color.HexColor;
                }
                PlanColorManager.ChangeColor(numColor, this.color.HexColor);
            }
        }
    }
}

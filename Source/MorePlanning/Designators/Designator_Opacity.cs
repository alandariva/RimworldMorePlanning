using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using System;

namespace MorePlanning
{
    public class Designator_Opacity : Designator
    {
        private static int opacity = 0;

        public static int Opacity
        {
            get
            {
                return opacity;
            }
            set
            {
                opacity = value;
                UpdateLabel();
                MorePlanningMod.Instance.PlanOpacity = opacity;
            }
        }

        public Designator_Opacity()
        {
            this.defaultLabel = "MorePlanning.Opacity.label".Translate(0);
            this.defaultDesc = "MorePlanning.Opacity.desc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/Opacity", true);
        }

        public static void UpdateLabel()
        {
            var desOpacity = Utils_Menu.GetPlanningDesignator<Designator_Opacity>();
            desOpacity.defaultLabel = "MorePlanning.Opacity.label".Translate(opacity);
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            return false;
        }

        public override void ProcessInput(Event ev)
        {
            int[] opacityOptions = new int[] { 10, 15, 20, 25, 30, 40, 50, 60, 70, 80 };
            List<FloatMenuOption> options = new List<FloatMenuOption>();

            for (int i = 0; i < opacityOptions.Length; i++)
            {
                int value = opacityOptions[i];
                string label = value + "%";
                if (value == MorePlanningMod.Instance.DefaultPlanOpacity)
                {
                    label += " " + "MorePlanning.DefaultOpacity".Translate();
                }
                options.Add(new FloatMenuOption(label, delegate {
                    MorePlanningMod.Instance.PlanOpacity = value;
                    this.defaultLabel = "MorePlanning.Opacity.label".Translate(value);
                }));
            }

            Find.WindowStack.Add(new FloatMenu(options));
        }

    }
}

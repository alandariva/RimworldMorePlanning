using System;
using System.Collections.Generic;
using MorePlanning.Utility;
using UnityEngine;
using Verse;

namespace MorePlanning.Designators
{
    public class OpacityDesignator : Designator
    {
        private static int _opacity;

        public static int Opacity
        {
            get => _opacity;
            set
            {
                _opacity = value;
                UpdateLabel();
                MorePlanningMod.Instance.PlanOpacity = _opacity;
            }
        }

        public OpacityDesignator()
        {
            defaultLabel = "MorePlanning.Opacity.label".Translate(0);
            defaultDesc = "MorePlanning.Opacity.desc".Translate();
            icon = ContentFinder<Texture2D>.Get("UI/Opacity");
        }

        public static void UpdateLabel()
        {
            var desOpacity = MenuUtility.GetPlanningDesignator<OpacityDesignator>();
            desOpacity.defaultLabel = "MorePlanning.Opacity.label".Translate(_opacity);
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            return false;
        }

        public override void ProcessInput(Event ev)
        {
            int[] opacityOptions = { 10, 15, 20, 25, 30, 40, 50, 60, 70, 80 };
            List<FloatMenuOption> options = new List<FloatMenuOption>();

            foreach (var value in opacityOptions)
            {
                string label = value + "%";
                if (Math.Abs(value - MorePlanningMod.Instance.DefaultPlanOpacity) < 1)
                {
                    label += " " + "MorePlanning.DefaultOpacity".Translate();
                }

                var value1 = value;
                options.Add(new FloatMenuOption(label, delegate {
                    MorePlanningMod.Instance.PlanOpacity = value1;
                    defaultLabel = "MorePlanning.Opacity.label".Translate(value1);
                }));
            }

            Find.WindowStack.Add(new FloatMenu(options));
        }

    }
}

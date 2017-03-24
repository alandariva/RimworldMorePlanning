using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace MorePlanning
{
    public class Designator_PlanningVisibility : Designator
    {
        private static bool planningVisibility = true;

        public static bool PlanningVisibility
        {
            get
            {
                return planningVisibility;
            }
            set
            {
                planningVisibility = value;
                UpdateIconTool();
                MorePlanningMod.Instance.SetPlanningVisibility(value);
            }
        }

        public Designator_PlanningVisibility()
        {
            this.defaultLabel = "MorePlanning.PlanVisibility".Translate();
            this.defaultDesc = "MorePlanning.PlanVisibilityDesc".Translate();
            this.soundSucceeded = SoundDefOf.DesignatePlanAdd;
            this.hotKey = KeyBindingDefOf.Misc12;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            MorePlanningMod.LogError(GetType().Name + " can't designate cells");
            return AcceptanceReport.WasRejected;
        }

        public static void UpdateIconTool()
        {
            var desPlanningVisibility = Utils_Menu.GetPlanningDesignator<Designator_PlanningVisibility>();

            if (planningVisibility)
            {
                desPlanningVisibility.icon = ContentFinder<Texture2D>.Get("UI/PlanVisible", true);
            }
            else
            {
                desPlanningVisibility.icon = ContentFinder<Texture2D>.Get("UI/PlanInvisible", true);
            }
        }

        public override void ProcessInput(Event ev)
        {
            if (this.CurActivateSound != null)
            {
                this.CurActivateSound.PlayOneShotOnCamera();
            }
            Find.DesignatorManager.Deselect();
            PlanningVisibility = !planningVisibility;
        }
    }
}

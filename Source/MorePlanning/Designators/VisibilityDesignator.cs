using MorePlanning.Utility;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;
using Resources = MorePlanning.Common.Resources;

namespace MorePlanning.Designators
{
    public class VisibilityDesignator : Designator
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

        public VisibilityDesignator()
        {
            this.defaultLabel = "MorePlanning.PlanVisibility".Translate();
            this.defaultDesc = "MorePlanning.PlanVisibilityDesc".Translate();
            this.soundSucceeded = SoundDefOf.Designate_PlanAdd;
            this.hotKey = KeyBindingDefOf.Misc12;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            MorePlanningMod.LogError(GetType().Name + " can't designate cells");
            return AcceptanceReport.WasRejected;
        }

        public static void UpdateIconTool()
        {
            var desPlanningVisibility = MenuUtility.GetPlanningDesignator<VisibilityDesignator>();

            if (planningVisibility)
            {
                desPlanningVisibility.icon = Resources.IconVisible;
            }
            else
            {
                desPlanningVisibility.icon = Resources.IconInvisible;
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

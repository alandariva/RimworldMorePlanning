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

        private static FieldInfo resolvedDesignatorsInfo = null;

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
            this.hotKey = KeyBindingDefOf.Misc5;
            InitReflection();
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            return false;
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            return;
        }

        public static void UpdateIconTool()
        {
            var planningCategory = DefDatabase<DesignationCategoryDef>.GetNamed("Planning");

            if (planningCategory == null)
            {
                Log.Message("Não deu de primeira");
                return;
            }

            List<Designator> list = (List<Designator>) resolvedDesignatorsInfo.GetValue(planningCategory);

            foreach (Designator des in list)
            {
                if (des is Designator_PlanningVisibility)
                {
                    if (planningVisibility)
                    {
                        des.icon = ContentFinder<Texture2D>.Get("UI/PlanVisible", true);
                    }
                    else
                    {
                        des.icon = ContentFinder<Texture2D>.Get("UI/PlanInvisible", true);
                    }
                }
            }
        }

        private static void InitReflection()
        {
            FieldInfo field = typeof(DesignationCategoryDef).GetField("resolvedDesignators", BindingFlags.Instance | BindingFlags.NonPublic);
            resolvedDesignatorsInfo = field;
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

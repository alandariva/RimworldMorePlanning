using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using System.Reflection;

namespace MorePlanning
{
    class Utils_Menu
    {

        private static FieldInfo resolvedDesignatorsInfo = null;

        public static void InitReflection()
        {
            FieldInfo field = typeof(DesignationCategoryDef).GetField("resolvedDesignators", BindingFlags.Instance | BindingFlags.NonPublic);
            resolvedDesignatorsInfo = field;

            if (resolvedDesignatorsInfo == null)
            {
                MorePlanningMod.LogError("Reflection failed (Utils_Menu::InitReflection, DesignationCategoryDef.resolvedDesignators)");
            }
        }

        public static List<Designator> GetPlanningDesignators()
        {
            if (resolvedDesignatorsInfo == null)
            {
                InitReflection();
            }

            var planningCategory = DefDatabase<DesignationCategoryDef>.GetNamed("Planning");

            if (planningCategory == null)
            {
                MorePlanningMod.LogError("Menu planning not found");
                return null;
            }

            return (List<Designator>)resolvedDesignatorsInfo.GetValue(planningCategory);
        }

        public static T GetPlanningDesignator<T>() where T: class
        {
            var designators = GetPlanningDesignators();
            foreach (var designator in designators)
            {
                if (designator is T)
                {
                    return designator as T;
                }
            }

            return null;
        }

    }
}

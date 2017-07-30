using Verse;

namespace MorePlanning
{
    public static class Utils_Plan
    {
        public static void RemoveAllPlanDesignationAt(IntVec3 c, Map map)
        {
            foreach (DesignationDef def in MorePlanningMod.PlanDesDefs)
            {
                Designation desAt = map.designationManager.DesignationAt(c, def);
                if (desAt != null)
                {
                    desAt.Delete();
                }
            }
        }

        /// <summary>
        /// Returns a plan designation if it exists at the position.
        /// </summary>
        public static PlanDesignation GetPlanDesignationAt(IntVec3 c, Map map)
        {
            foreach (DesignationDef def in MorePlanningMod.PlanDesDefs)
            {
                var des = map.designationManager.DesignationAt(c, def);
                if (des != null)
                {
                    return des as PlanDesignation;
                }
            }

            return null;
        }

        public static bool HasAnyPlanDesignationAt(IntVec3 c, Map map)
        {
            foreach (DesignationDef def in MorePlanningMod.PlanDesDefs)
            {
                if (map.designationManager.DesignationAt(c, def) != null)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasPlanDesignationAt(IntVec3 c, Map map, int color)
        {
            foreach (DesignationDef def in MorePlanningMod.PlanDesDefs)
            {
                var designation = GetPlanDesignationAt(c, map);
                if ((designation != null) && (designation.color == color))
                {
                    return true;
                }
            }

            return false;
        }

    }
}

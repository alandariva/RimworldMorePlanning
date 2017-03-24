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
        /// It can return a PlanDesignation or a Designation, for compatibility with vanilla.
        /// </summary>
        public static Designation GetPlanDesignationAt(IntVec3 c, Map map)
        {
            foreach (DesignationDef def in MorePlanningMod.PlanDesDefs)
            {
                var des = map.designationManager.DesignationAt(c, def);
                if (des != null)
                {
                    return des;
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

    }
}

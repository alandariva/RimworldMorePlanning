using Verse;
using RimWorld;

namespace MorePlanning
{
    public static class Utils_Migrate
    {

        /// <summary>
        /// Migrate old designations from older versions of this mod.
        /// </summary>
        /// <param name="map"></param>
        public static void MigrateOldDesignation(Map map)
        {
            var oldDesignations = map.designationManager.allDesignations.FindAll(d => d.def is OLDPlanningDesignationDef);

            foreach (var oldDesignation in oldDesignations)
            {
                int color = 0;
                switch (oldDesignation.def.defName)
                {
                    case "PlanBlue":
                        color = PlanningDesignationDef.ColorBlue;
                        break;
                    case "PlanRed":
                        color = PlanningDesignationDef.ColorRed;
                        break;
                    case "PlanGreen":
                        color = PlanningDesignationDef.ColorGreen;
                        break;
                    case "PlanYellow":
                        color = PlanningDesignationDef.ColorYellow;
                        break;
                }
                var newDesignation = new PlanDesignation(oldDesignation.target, DesignationDefOf.Plan, color);
                map.designationManager.allDesignations.Add(newDesignation);
                newDesignation.designationManager = map.designationManager;
                oldDesignation.Delete();
            }
        }

    }
}

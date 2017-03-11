using Harmony;
using System.Reflection;
using Verse;

namespace MorePlanning
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = HarmonyInstance.Create(MorePlanningMod.Identifier);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(Designation))]
    [HarmonyPatch("DesignationDraw")]
    class DesignatioPlanningDraw
    {
        static bool Prefix(Designation __instance)
        {
            return Designator_PlanningVisibility.PlanningVisibility || (__instance.def is PlanningDesignationDef == false);
        }
    }
}

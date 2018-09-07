using System.Reflection;
using Harmony;
using Verse;

namespace MorePlanning.Patches
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
}

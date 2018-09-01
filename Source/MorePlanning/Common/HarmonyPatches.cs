using System.Reflection;
using Harmony;
using MorePlanning.Designators;
using MorePlanning.Plan;
using UnityEngine;
using Verse;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable ArrangeTypeModifiers

namespace MorePlanning.Common
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
    class DesignationPlanningDraw
    {
        static bool Prefix(Designation __instance)
        {
            if (__instance.def is PlanDesignationDef == false)
            {
                return true;
            }

            if (VisibilityDesignator.PlanningVisibility == false)
            {
                return false;
            }

            int colorId = 0;

            if (__instance is PlanDesignation designation)
            {
                colorId = designation.Color;
            }

            Vector3 position = __instance.target.Cell.ToVector3ShiftedWithAltitude(__instance.DesignationDrawAltitude);
            Graphics.DrawMesh(MeshPool.plane10, position, Quaternion.identity, Resources.PlanMatColor[colorId], 0);

            return false;
        }
    }

    [HarmonyPatch(typeof(Designation))]
    [HarmonyPatch("ExposeData")]
    class DesignationPlanningExposeData
    {
        static bool Prefix(Designation __instance)
        {
            if (__instance is PlanDesignation designation)
            {
                Scribe_Values.Look(ref designation.Color, "Color", 0, true);
            }
            
            return true;
        }
    }
}

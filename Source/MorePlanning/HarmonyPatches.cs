﻿using Harmony;
using System.Reflection;
using UnityEngine;
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
    class DesignationPlanningDraw
    {
        static bool Prefix(Designation __instance)
        {
            if (__instance.def is PlanningDesignationDef == false)
            {
                return true;
            }

            if (Designator_PlanningVisibility.PlanningVisibility == false)
            {
                return false;
            }

            int colorId = 0;

            if (__instance is PlanDesignation)
            {
                colorId = (__instance as PlanDesignation).color;
            }

            Vector3 position = position = __instance.target.Cell.ToVector3ShiftedWithAltitude(__instance.DesignationDrawAltitude);
            Graphics.DrawMesh(MeshPool.plane10, position, Quaternion.identity, (__instance.def as PlanningDesignationDef).iconMatColor[colorId], 0);

            return false;
        }
    }

    [HarmonyPatch(typeof(Designation))]
    [HarmonyPatch("ExposeData")]
    class DesignatioPlanningExposeData
    {
        static bool Prefix(Designation __instance)
        {
            if (__instance is PlanDesignation)
            {
                Scribe_Values.LookValue<int>(ref (__instance as PlanDesignation).color, "Color", 0, true);
            }
            
            return true;
        }
    }
}

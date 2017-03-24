using System;
using HugsLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Reflection;
using HugsLib.Settings;
using UnityEngine;

namespace MorePlanning
{

    public class MorePlanningMod : ModBase
    {
        private MorePlanningMod()
        {
            instance = this;
        }

        private static MorePlanningMod instance = null;
        public static MorePlanningMod Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MorePlanningMod();
                }
                return instance;
            }
        }

        public const string Identifier = "com.github.alandariva.moreplanning";

        private static List<PlanningDesignationDef> planDesDefs = new List<PlanningDesignationDef>();

        private PlanningDataStore dataStore = null;

        private SettingHandle<bool> removeIfBuildingDespawned;

        private SettingHandle<int> planOpacity;
        public int PlanOpacity
        {
            get
            {
                return planOpacity.Value;
            }
            set
            {
                planOpacity.Value = value;
                HugsLibController.SettingsManager.SaveChanges();
            }
        }

        public float DefaultPlanOpacity
        {
            get
            {
                return planOpacity.DefaultValue;
            }
        }

        public static List<PlanningDesignationDef> PlanDesDefs
        {
            get
            {
                if (planDesDefs.Count == 0)
                {
                    LoadPlanDesDefs();
                }
                return planDesDefs;
            }
        }

        public override string ModIdentifier
        {
            get { return MorePlanningMod.Identifier; }
        }

        public override void DefsLoaded()
        {
            LoadPlanDesDefs();
            removeIfBuildingDespawned = Settings.GetHandle<bool>("removeIfBuildingDespawned", "MorePlanning.SettingRemoveIfBuildingDespawned.label".Translate(), "MorePlanning.SettingRemoveIfBuildingDespawned.desc".Translate(), false);
            planOpacity = Settings.GetHandle<int>("opacity", "MorePlanning.SettingPlanOpacity.label".Translate(), "MorePlanning.SettingPlanOpacity.desc".Translate(), 25);
            planOpacity.NeverVisible = true;
            SettingsChanged();
        }

        public static void LogError(string text)
        {
            Instance.Logger.Error(text);
        }

        private void UpdatePlanningDefsSetting()
        {
            var planningDefs = DefDatabase<PlanningDesignationDef>.AllDefs;
            foreach (var planningDef in planningDefs)
            {
                planningDef.removeIfBuildingDespawned = removeIfBuildingDespawned;
            }
        }

        public override void SettingsChanged()
        {
            UpdatePlanningDefsSetting();
            UpdatePlanOpacity();
        }

        private void UpdatePlanOpacity()
        {
            var planDef = DefDatabase<PlanningDesignationDef>.GetNamed("Plan", true);

            foreach (var mat in planDef.iconMatColor)
            {
                Color color = mat.color;
                color.a = planOpacity / 100f;
                mat.color = color;
            }
        }

        public override void WorldLoaded()
        {
            dataStore = HugsLib.Utils.UtilityWorldObjectManager.GetUtilityWorldObject<PlanningDataStore>();
            Designator_PlanningVisibility.PlanningVisibility = dataStore.planningVisibility;
            Designator_Opacity.Opacity = PlanOpacity;
        }

        private static void LoadPlanDesDefs()
        {
            planDesDefs.Clear();
            planDesDefs.AddRange(DefDatabase<PlanningDesignationDef>.AllDefsListForReading);
        }

        public void SetPlanningVisibility(bool value)
        {
            if (this.dataStore != null)
            {
                this.dataStore.planningVisibility = value;
            }
        }

        public override void MapLoaded(Map map)
        {
            Utils_Migrate.MigrateOldDesignation(map);
        }

        private class PlanningDataStore : HugsLib.Utils.UtilityWorldObject
        {
            public bool planningVisibility;

            public override void PostAdd()
            {
                base.PostAdd();
                planningVisibility = true;
            }

            public override void ExposeData()
            {
                base.ExposeData();
                Scribe_Values.LookValue<bool>(ref planningVisibility, "planningVisibility", true);
            }
        }

    }
}

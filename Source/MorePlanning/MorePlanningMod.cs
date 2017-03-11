using System;
using HugsLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Reflection;
using HugsLib.Settings;

namespace MorePlanning
{
    class MorePlanningMod : ModBase
    {
        public static MorePlanningMod Instance
        {
            get;
            private set;
        }

        public const string Identifier = "com.github.alandariva.moreplanning";

        private static List<PlanningDesignationDef> planDesDefs = new List<PlanningDesignationDef>();

        private PlanningDataStore dataStore = null;

        private SettingHandle<bool> removeIfBuildingDespawned;

        private MorePlanningMod() : base()
        {
            MorePlanningMod.Instance = this;
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
            UpdatePlanningDefsSetting();
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
            base.SettingsChanged();

            UpdatePlanningDefsSetting();
        }

        public override void WorldLoaded()
        {
            dataStore = HugsLib.Utils.UtilityWorldObjectManager.GetUtilityWorldObject<PlanningDataStore>();
            Designator_PlanningVisibility.PlanningVisibility = dataStore.planningVisibility;
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

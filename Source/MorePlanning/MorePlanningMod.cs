using System;
using HugsLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Reflection;
using HugsLib.Settings;
using MorePlanning.Designators;
using MorePlanning.Plan;
using UnityEngine;
using Resources = MorePlanning.Common.Resources;

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

        public int SelectedColor = 0;

        private static List<PlanDesignationDef> planDesDefs = new List<PlanDesignationDef>();

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

        public static List<PlanDesignationDef> PlanDesDefs
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
            PlanColorManager.Load(Settings);

            SettingsChanged();

            DesignationCategoryDef desCatDef = DefDatabase<DesignationCategoryDef>.GetNamed("Planning");

            if (desCatDef == null)
                throw new Exception("Planning designation category not found");

            FieldInfo _designatorsFI = typeof(DesignationCategoryDef).GetField("resolvedDesignators", BindingFlags.NonPublic | BindingFlags.Instance);
            var _designators = _designatorsFI.GetValue(desCatDef) as List<Designator>;

            for (int i = 0; i < PlanColorManager.NumPlans; i++)
            {
                _designators.Add(new SelectColorDesignator(i));
            }
        }

        public static void LogError(string text)
        {
            Instance.Logger.Error(text);
        }

        private void UpdatePlanningDefsSetting()
        {
            var planningDefs = DefDatabase<PlanDesignationDef>.AllDefs;
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
            var planDef = DefDatabase<PlanDesignationDef>.GetNamed("Plan", true);

            foreach (var mat in Resources.planMatColor)
            {
                Color color = mat.color;
                color.a = planOpacity / 100f;
                mat.color = color;
            }
        }

        public override void WorldLoaded()
        {
            dataStore = HugsLib.Utils.UtilityWorldObjectManager.GetUtilityWorldObject<PlanningDataStore>();
            VisibilityDesignator.PlanningVisibility = dataStore.planningVisibility;
            OpacityDesignator.Opacity = PlanOpacity;
        }

        private static void LoadPlanDesDefs()
        {
            planDesDefs.Clear();
            planDesDefs.AddRange(DefDatabase<PlanDesignationDef>.AllDefsListForReading);
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
                Scribe_Values.Look<bool>(ref planningVisibility, "planningVisibility", true);
            }
        }

    }
}

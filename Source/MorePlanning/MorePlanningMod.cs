using System;
using System.Collections.Generic;
using System.Reflection;
using HugsLib;
using HugsLib.Settings;
using HugsLib.Utils;
using MorePlanning.Designators;
using MorePlanning.Plan;
using UnityEngine;
using Verse;
using Resources = MorePlanning.Common.Resources;

namespace MorePlanning
{

    public class MorePlanningMod : ModBase
    {
        private MorePlanningMod()
        {
            _instance = this;
        }

        private static MorePlanningMod _instance;
        public static MorePlanningMod Instance
        {
            get { return _instance ?? (_instance = new MorePlanningMod()); }
        }

        public const string Identifier = "com.github.alandariva.moreplanning";

        public int SelectedColor = 0;

        private static List<PlanDesignationDef> _planDesDefs = new List<PlanDesignationDef>();

        private PlanningDataStore _dataStore;

        private SettingHandle<bool> _removeIfBuildingDespawned;

        private SettingHandle<int> _planOpacity;
        public int PlanOpacity
        {
            get => _planOpacity.Value;
            set
            {
                _planOpacity.Value = value;
                HugsLibController.SettingsManager.SaveChanges();
            }
        }

        public float DefaultPlanOpacity => _planOpacity.DefaultValue;

        public static List<PlanDesignationDef> PlanDesDefs
        {
            get
            {
                if (_planDesDefs.Count == 0)
                {
                    LoadPlanDesDefs();
                }
                return _planDesDefs;
            }
        }

        public override string ModIdentifier => Identifier;

        public override void DefsLoaded()
        {
            LoadPlanDesDefs();
            _removeIfBuildingDespawned = Settings.GetHandle("removeIfBuildingDespawned", "MorePlanning.SettingRemoveIfBuildingDespawned.label".Translate(), "MorePlanning.SettingRemoveIfBuildingDespawned.desc".Translate(), false);
            _planOpacity = Settings.GetHandle("opacity", "MorePlanning.SettingPlanOpacity.label".Translate(), "MorePlanning.SettingPlanOpacity.desc".Translate(), 25);
            _planOpacity.NeverVisible = true;
            PlanColorManager.Load(Settings);

            SettingsChanged();

            DesignationCategoryDef desCatDef = DefDatabase<DesignationCategoryDef>.GetNamed("Planning");

            if (desCatDef == null)
                throw new Exception("Planning designation category not found");

            FieldInfo designatorsFi = typeof(DesignationCategoryDef).GetField("resolvedDesignators", BindingFlags.NonPublic | BindingFlags.Instance);
            var designators = designatorsFi.GetValue(desCatDef) as List<Designator>;

            for (int i = 0; i < PlanColorManager.NumPlans; i++)
            {
                designators.Add(new SelectColorDesignator(i));
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
                planningDef.removeIfBuildingDespawned = _removeIfBuildingDespawned;
            }
        }

        public override void SettingsChanged()
        {
            UpdatePlanningDefsSetting();
            UpdatePlanOpacity();
        }

        private void UpdatePlanOpacity()
        {
            foreach (var mat in Resources.PlanMatColor)
            {
                Color color = mat.color;
                color.a = _planOpacity / 100f;
                mat.color = color;
            }
        }

        public override void WorldLoaded()
        {
            _dataStore = UtilityWorldObjectManager.GetUtilityWorldObject<PlanningDataStore>();
            VisibilityDesignator.PlanningVisibility = _dataStore.PlanningVisibility;
            OpacityDesignator.Opacity = PlanOpacity;
        }

        private static void LoadPlanDesDefs()
        {
            _planDesDefs.Clear();
            _planDesDefs.AddRange(DefDatabase<PlanDesignationDef>.AllDefsListForReading);
        }

        public void SetPlanningVisibility(bool value)
        {
            if (_dataStore != null)
            {
                _dataStore.PlanningVisibility = value;
            }
        }

        public override void MapLoaded(Map map)
        {
            
        }

        private class PlanningDataStore : UtilityWorldObject
        {
            public bool PlanningVisibility;

            public override void PostAdd()
            {
                base.PostAdd();
                PlanningVisibility = true;
            }

            public override void ExposeData()
            {
                base.ExposeData();
                Scribe_Values.Look(ref PlanningVisibility, "planningVisibility", true);
            }
        }

    }
}

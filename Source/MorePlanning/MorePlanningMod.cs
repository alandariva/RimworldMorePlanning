using System;
using HugsLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Reflection;

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
        }

        public override void WorldLoaded()
        {
            dataStore = HugsLib.Utils.UtilityWorldObjectManager.GetUtilityWorldObject<PlanningDataStore>();
            Designator_PlanningVisibility.PlanningVisibility = dataStore.planningVisibility;
        }

        private static void LoadPlanDesDefs()
        {
            planDesDefs.Clear();
            planDesDefs.Add(DefDatabase<PlanningDesignationDef>.GetNamed("Plan", true));
            planDesDefs.Add(DefDatabase<PlanningDesignationDef>.GetNamed("PlanBlue", true));
            planDesDefs.Add(DefDatabase<PlanningDesignationDef>.GetNamed("PlanGreen", true));
            planDesDefs.Add(DefDatabase<PlanningDesignationDef>.GetNamed("PlanRed", true));
            planDesDefs.Add(DefDatabase<PlanningDesignationDef>.GetNamed("PlanYellow", true));
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

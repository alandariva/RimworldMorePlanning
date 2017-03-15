using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace MorePlanning
{
    public class Designator_PlanShape : Designator
    {
        public const string ShapeFilledRectangle = "__filledrectangle";
        public const string ShapeEmptyRectangle = "__emptyrectangle";

        private string shape = ShapeFilledRectangle;

        public Designator_PlanShape()
        {
            this.defaultLabel = "MorePlanning.PlanVisibility".Translate();
            this.defaultDesc = "MorePlanning.PlanVisibilityDesc".Translate();
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            MorePlanningMod.LogError(GetType().Name + " can't designate cells");
            return AcceptanceReport.WasRejected;
        }

        public override void ProcessInput(Event ev)
        {
            var currentDesignator = Find.DesignatorManager.SelectedDesignator;
            if (this.CurActivateSound != null)
            {
                this.CurActivateSound.PlayOneShotOnCamera();
            }

            Find.WindowStack.Add(new DialogSelectThing());
        }


    }
}

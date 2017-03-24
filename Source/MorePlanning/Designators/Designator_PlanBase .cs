using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using System;
using System.Reflection;

namespace MorePlanning
{
    public abstract class Designator_PlanBase : Designator_Base
    {
        protected int color;
        protected DesignateMode mode;

        protected PlanningDesignationDef desDef = null;

        protected int draggableDimensions = 2;

        public override int DraggableDimensions
        {
            get
            {
                return draggableDimensions;
            }
        }

        public override bool DragDrawMeasurements
        {
            get
            {
                return true;
            }
        }

        public Designator_PlanBase(int color, DesignateMode mode)
        {
            this.mode = mode;
            this.desDef = DefDatabase<PlanningDesignationDef>.GetNamed("Plan", true);
            this.color = color;

            if (DesignateMode.Add == mode)
            {
                this.soundSucceeded = SoundDefOf.DesignatePlanAdd;
            }
            else
            {
                this.soundSucceeded = SoundDefOf.DesignatePlanRemove;
            }
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!c.InBounds(base.Map))
            {
                return false;
            }
            if (c.InNoBuildEdgeArea(base.Map))
            {
                return "TooCloseToMapEdge".Translate();
            }
            if (this.mode == DesignateMode.Add)
            {
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) == false)
                {
                    if (Utils_Plan.HasAnyPlanDesignationAt(c, this.Map))
                    {
                        return false;
                    }
                }
            }
            else if (this.mode == DesignateMode.Remove && HasThisPlanAt(c, this.desDef) == false)
            {
                return false;
            }
            return true;
        }

        private bool HasThisPlanAt(IntVec3 c, DesignationDef desDef)
        {
            var desig = base.Map.designationManager.DesignationAt(c, this.desDef);
            if (desig is PlanDesignation)
            {
                return (desig as PlanDesignation).color == this.color;
            }
            return desig != null && this.color == PlanningDesignationDef.ColorGray;
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            if (this.mode == DesignateMode.Add)
            {
                Utils_Plan.RemoveAllPlanDesignationAt(c, this.Map);
                base.Map.designationManager.AddDesignation(new PlanDesignation(c, this.desDef, this.color));
            }
            else if (this.mode == DesignateMode.Remove)
            {
                base.Map.designationManager.DesignationAt(c, this.desDef).Delete();
            }
        }

        public override void SelectedUpdate()
        {
            GenUI.RenderMouseoverBracket();
            GenDraw.DrawNoBuildEdgeLines();
            Designator_PlanningVisibility.PlanningVisibility = true;
        }

    }
}

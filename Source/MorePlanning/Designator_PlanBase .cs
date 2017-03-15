using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using System;
using System.Reflection;

namespace MorePlanning
{
    public abstract class Designator_PlanBase : Designator
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
            this.soundDragSustain = SoundDefOf.DesignateDragStandard;
            this.soundDragChanged = SoundDefOf.DesignateDragStandardChanged;
            this.useMouseIcon = true;
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

        protected void RemoveAllPlanDesignationAt(IntVec3 c)
        {
            foreach (DesignationDef def in MorePlanningMod.PlanDesDefs)
            {
                Designation desAt = base.Map.designationManager.DesignationAt(c, def);
                if (desAt != null)
                {
                    desAt.Delete();
                }
            }
        }

        protected bool HasAnyPlanDesignationAt(IntVec3 c)
        {
            foreach (DesignationDef def in MorePlanningMod.PlanDesDefs)
            {
                if (base.Map.designationManager.DesignationAt(c, def) != null)
                {
                    return true;
                }
            }

            return false;
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
                    if (HasAnyPlanDesignationAt(c))
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
            var desig = base.Map.designationManager.DesignationAt(c, this.desDef) as PlanDesignation;
            return desig != null && desig.color == this.color;
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            if (this.mode == DesignateMode.Add)
            {
                RemoveAllPlanDesignationAt(c);
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

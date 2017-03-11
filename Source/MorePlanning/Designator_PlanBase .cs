using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    public abstract class Designator_PlanBase : Designator
    {
        protected DesignateMode mode;

        protected DesignationDef desDef = null;

        public override int DraggableDimensions
        {
            get
            {
                return 2;
            }
        }

        public override bool DragDrawMeasurements
        {
            get
            {
                return true;
            }
        }

        public Designator_PlanBase(DesignationDef desDef, DesignateMode mode)
        {
            this.mode = mode;
            this.soundDragSustain = SoundDefOf.DesignateDragStandard;
            this.soundDragChanged = SoundDefOf.DesignateDragStandardChanged;
            this.useMouseIcon = true;
            this.desDef = desDef;

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
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    && base.Map.designationManager.DesignationAt(c, this.desDef) == null)
                {
                    return true;
                }
                else
                {
                    if (HasAnyPlanDesignationAt(c))
                    {
                        return false;
                    }
                }
            }
            else if (this.mode == DesignateMode.Remove && base.Map.designationManager.DesignationAt(c, this.desDef) == null)
            {
                return false;
            }
            return true;
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            if (this.mode == DesignateMode.Add)
            {
                RemoveAllPlanDesignationAt(c);
                base.Map.designationManager.AddDesignation(new Designation(c, this.desDef));
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

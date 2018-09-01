using System;
using RimWorld;
using Verse;

namespace MorePlanning.Designators
{
    public abstract class BaseDesignator : Designator
    {
        protected BaseDesignator()
        {
            soundDragSustain = SoundDefOf.Designate_DragStandard;
            soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            useMouseIcon = true;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            throw new NotImplementedException();
        }
    }
}

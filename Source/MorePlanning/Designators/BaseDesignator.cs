using RimWorld;
using Verse;
using System;

namespace MorePlanning.Designators
{
    public abstract class BaseDesignator : Designator
    {
        public BaseDesignator()
        {
            this.soundDragSustain = SoundDefOf.Designate_DragStandard;
            this.soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            this.useMouseIcon = true;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            throw new NotImplementedException();
        }
    }
}

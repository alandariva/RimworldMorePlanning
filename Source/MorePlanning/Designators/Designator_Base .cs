using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using System;
using System.Reflection;
using Verse.Sound;

namespace MorePlanning
{
    public abstract class Designator_Base : Designator
    {
        public Designator_Base()
        {
            this.soundDragSustain = SoundDefOf.DesignateDragStandard;
            this.soundDragChanged = SoundDefOf.DesignateDragStandardChanged;
            this.useMouseIcon = true;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            throw new NotImplementedException();
        }
    }
}

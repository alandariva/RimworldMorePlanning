using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using System;
using System.Reflection;

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
    }
}

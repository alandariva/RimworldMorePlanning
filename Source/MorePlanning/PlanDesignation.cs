using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace MorePlanning
{
    public class PlanDesignation : Designation
    {
        public int color = 0;

        public PlanDesignation(LocalTargetInfo target, DesignationDef def, int color) : base(target, def)
        {
            this.color = color;
        }

        public PlanDesignation()
        {
        }
        
    }
}

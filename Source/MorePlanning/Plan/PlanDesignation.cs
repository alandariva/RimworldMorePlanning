using Verse;

namespace MorePlanning.Plan
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

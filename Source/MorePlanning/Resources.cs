using UnityEngine;
using Verse;

namespace MorePlanning
{
    [StaticConstructorOnStartup]
    class Resources
    {
        public static readonly Texture2D IconVisible = ContentFinder<Texture2D>.Get("UI/PlanVisible", true);
        public static readonly Texture2D IconInvisible = ContentFinder<Texture2D>.Get("UI/PlanInvisible", true);
    }
}

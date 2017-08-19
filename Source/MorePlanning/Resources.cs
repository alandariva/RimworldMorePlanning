using System.IO;
using UnityEngine;
using Verse;

namespace MorePlanning
{
    [StaticConstructorOnStartup]
    class Resources
    {
        public static readonly Texture2D IconVisible = ContentFinder<Texture2D>.Get("UI/PlanVisible", true);
        public static readonly Texture2D IconInvisible = ContentFinder<Texture2D>.Get("UI/PlanInvisible", true);

        public static readonly Texture2D ToolBoxColor = ContentFinder<Texture2D>.Get("UI/ToolBoxColor", true);
        public static readonly Texture2D ToolBoxColorSelected = ContentFinder<Texture2D>.Get("UI/ToolBoxColorSelected", true);

        public static readonly Texture2D Plan = ContentFinder<Texture2D>.Get("UI/Plan", true);

        public static readonly Texture2D RemoveIcon = ContentFinder<Texture2D>.Get("UI/RemoveIcon", true);
        public static readonly Texture2D PlanToolRemoveAll = ContentFinder<Texture2D>.Get("UI/PlanToolRemoveAll", true);

        public static readonly Texture2D ColorPickerSelect = ContentFinder<Texture2D>.Get("UI/ColorPickerSelect", true);

        public static readonly Texture2D ColorPickerOverlay = ContentFinder<Texture2D>.Get("UI/ColorPickerOverlay", true);
        public static readonly Texture2D HsvSlider = ContentFinder<Texture2D>.Get("UI/HsvSlider", true);

        public static Material[] planMatColor = new Material[PlanColorManager.NumPlans];

        static Resources()
        {
            for (int i = 0; i < planMatColor.Length; i++)
            {
                Color c = new Color(0, 0, i);
                planMatColor[i] = MaterialPool.MatFrom("UI/PlanBase", ShaderDatabase.MetaOverlay, c);
            }
        }
    }
}

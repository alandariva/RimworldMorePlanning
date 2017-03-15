using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Harmony;
using System.Reflection;

namespace MorePlanning
{
    public class PlanningDesignationDef : DesignationDef
    {
        public const int ColorGray = 0;
        public const int ColorBlue = 1;
        public const int ColorRed = 3;
        public const int ColorGreen = 2;
        public const int ColorYellow = 4;

        [Unsaved]
        public Material[] iconMatColor = new Material[5];

        public override void ResolveReferences()
        {
            base.ResolveReferences();

            LongEventHandler.ExecuteWhenFinished(delegate
            {
                // blue
                {
                    Color color = new Color();
                    ColorUtility.TryParseHtmlString("#2095f2", out color);
                    this.iconMatColor[ColorBlue] = MaterialPool.MatFrom(this.texturePath, ShaderDatabase.MetaOverlay, color);
                }


                // gray
                {
                    Color color = new Color();
                    ColorUtility.TryParseHtmlString("#a9a9a9", out color);
                    this.iconMatColor[ColorGray] = MaterialPool.MatFrom(this.texturePath, ShaderDatabase.MetaOverlay, color);
                }

                // red
                {
                    Color color = new Color();
                    ColorUtility.TryParseHtmlString("#f34235", out color);
                    this.iconMatColor[ColorRed] = MaterialPool.MatFrom(this.texturePath, ShaderDatabase.MetaOverlay, color);
                }

                // green
                {
                    Color color = new Color();
                    ColorUtility.TryParseHtmlString("#4bae4f", out color);
                    this.iconMatColor[ColorGreen] = MaterialPool.MatFrom(this.texturePath, ShaderDatabase.MetaOverlay, color);
                }

                // yellow
                {
                    Color color = new Color();
                    ColorUtility.TryParseHtmlString("#feea3a", out color);
                    this.iconMatColor[ColorYellow] = MaterialPool.MatFrom(this.texturePath, ShaderDatabase.MetaOverlay, color);
                }
            });
        }
    }
}

using HugsLib.Settings;
using UnityEngine;
using Resources = MorePlanning.Common.Resources;
using Multiplayer.API;
using System.Text.RegularExpressions;
using System.Globalization;
using MorePlanning.Utility;

namespace MorePlanning.Plan
{

    public class PlanColorManager
    {
        public const int NumPlans = 10;

        private static Color[] PlanColor = new Color[NumPlans];
        private static bool[] PlanColorChanged = new bool[NumPlans];

        private static SettingHandle<string>[] _planColorSetting = new SettingHandle<string>[NumPlans];

        public static readonly string[] DefaultColors = new string[] {
            "a9a9a9",
            "2095f2",
            "4bae4f",
            "f34235",
            "feea3a",
            "ff00f0",
            "00fffc",
            "8400ff",
            "ffa200",
            "000000"
        };

        private static string GetDefaultColor(int i)
        {
            return DefaultColors[i];
        }

        public static void Load(ModSettingsPack settings)
        {
            for (int i = 0; i < NumPlans; i++)
            {
                _planColorSetting[i] = settings.GetHandle("planColor" + i, "planColor" + i, "planColor" + i, GetDefaultColor(i));
                _planColorSetting[i].NeverVisible = true;
            }

            for (int i = 0; i < NumPlans; i++)
            {
                OnColorChanged(i);
            }
        }

        [SyncMethod]
        public static void ChangeColor(int colorNum, string hexColor)
        {
            _planColorSetting[colorNum].Value = hexColor;
            OnColorChanged(colorNum);
        }

        private static void OnColorChanged(int numColor = -1)
        {
            PlanColor[numColor] = _planColorSetting[numColor].Value.HexToColor();
            PlanColorChanged[numColor] = true;
        }

        public static void InvalidateColors()
        {
            for (int i = 0; i < PlanColorChanged.Length; i++)
            {
                PlanColorChanged[i] = true;
            }
        }

        public static Color GetColor(int col = -1)
        {
            if (col < 0)
                col = MorePlanningMod.Instance.SelectedColor;
            return PlanColor[col];
        }

        public static Material GetMaterial(int numColor)
        {
            if(PlanColorChanged[numColor])
            {
                PlanColorChanged[numColor] = false;
                Color c = PlanColor[numColor];
                c.a = MorePlanningMod.Instance.ModSettings.PlanOpacity / 100f;
                Resources.PlanMatColor[numColor].SetColor("_Color", c);
            }
            return Resources.PlanMatColor[numColor];
        }

    }
}

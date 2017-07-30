using System;
using HugsLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Reflection;
using HugsLib.Settings;
using UnityEngine;

namespace MorePlanning.Dialog
{
    public class ColorSelectorDialog : Window
    {
        public override void DoWindowContents(Rect inRect)
        {
            Widgets.ButtonText(new Rect(0, 0, 100f, 10f), "Legal", true, false, true);
        }
    }
}

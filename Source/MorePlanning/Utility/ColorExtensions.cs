using UnityEngine;
using System.Globalization;

namespace MorePlanning.Utility
{
    public static class ColorExtensions
    {
        public static string ColorToHex(this Color c)
        {
            int r = (int)(Mathf.Clamp(c.r, 0, 1) * 255);
            int g = (int)(Mathf.Clamp(c.r, 0, 1) * 255);
            int b = (int)(Mathf.Clamp(c.r, 0, 1) * 255);

            return $"{r:x2}{g:x2}{b:x2}";
        }

        public static Color HexToColor(this string cstr)
        {
            Color color = Color.black;

            color.r = int.Parse(cstr.Substring(0, 2), NumberStyles.AllowHexSpecifier) / 255.0f;
            color.g = int.Parse(cstr.Substring(2, 2), NumberStyles.AllowHexSpecifier) / 255.0f;
            color.b = int.Parse(cstr.Substring(4, 2), NumberStyles.AllowHexSpecifier) / 255.0f;

            return color;
        }

        public static bool TryHexToColor(this string cstr, out Color color)
        {
            color = Color.black;

            if(!int.TryParse(cstr.Substring(0, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int r))
                return false;
            if (!int.TryParse(cstr.Substring(2, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int g))
                return false;
            if (!int.TryParse(cstr.Substring(4, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int b))
                return false;

            color.r = r / 255.0f;
            color.g = g / 255.0f;
            color.b = b / 255.0f;

            return true;
        }
    }
}

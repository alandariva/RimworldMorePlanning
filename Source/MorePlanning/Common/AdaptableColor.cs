using UnityEngine;

namespace MorePlanning.Common
{
    public class AdaptableColor
    {
        protected string hexColor;
        protected float h, s, v;
        protected float r, g, b, a;

        public AdaptableColor()
        {
            this.ObjColor = new Color();
        }

        public AdaptableColor(Color c)
        {
            this.ObjColor = c;
        }

        public Color ObjColorH
        {
            get
            {
                return Color.HSVToRGB(h, 1, 1);
            }
        }

        public Color ObjColor
        {
            get
            {
                return new Color(this.r, this.g, this.b, this.a);
            }

            set
            {
                this.r = value.r;
                this.g = value.g;
                this.b = value.b;
                this.a = value.a;
                Color.RGBToHSV(this.ObjColor, out h, out s, out v);
                this.hexColor = ColorUtility.ToHtmlStringRGB(this.ObjColor);
            }
        }

        public string HexColor
        {
            get
            {
                return hexColor;
            }

            set
            {
                Color color = new Color();
                if (ColorUtility.TryParseHtmlString(value, out color) == false)
                {
                    return;
                }

                color.a = this.a;

                this.ObjColor = color;
            }
        }

        public float H
        {
            get
            {
                return h;
            }

            set
            {
                this.h = value;
                Color c = Color.HSVToRGB(h, s, v);
                r = c.r;
                g = c.g;
                b = c.b;
                this.hexColor = ColorUtility.ToHtmlStringRGB(this.ObjColor);
            }
        }

        public float S
        {
            get
            {
                return s;
            }

            set
            {
                this.s = value;
                Color c = Color.HSVToRGB(h, s, v);
                r = c.r;
                g = c.g;
                b = c.b;
                this.hexColor = ColorUtility.ToHtmlStringRGB(this.ObjColor);
            }
        }

        public float V
        {
            get
            {
                return v;
            }

            set
            {
                this.v = value;
                Color c = Color.HSVToRGB(h, s, v);
                r = c.r;
                g = c.g;
                b = c.b;
                this.hexColor = ColorUtility.ToHtmlStringRGB(this.ObjColor);
            }
        }
    }
}

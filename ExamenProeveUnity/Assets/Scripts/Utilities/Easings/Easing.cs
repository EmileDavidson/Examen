using System;
using UnityEngine;

namespace Toolbox.Easings
{
    /// <summary>
    /// Class filled with easing formula's using percentage 0-1 and gives back an value around
    /// this value can be used to create easing methods.
    /// this class also works with <see cref="CurveGenerator"/> and <see cref="EasingTools"/>
    /// </summary>
    public static class Easing
    {
        public static float Linear(float t) => t;
        public static float InSine(float percentage) 
        {
            return (float) (1 - Math.Cos((percentage * Math.PI) / 2));
        }
 
        public static float OutSine(float percentage)
        {
            return (float)(Math.Sin((percentage * Math.PI) / 2));
        }
 
        public static float InOutSine(float percentage)
        {
            return (float) (-(Math.Cos(Math.PI * percentage) - 1) / 2);
        }
 
 
        public static float InCubic(float percentage)
        {
            return (percentage * percentage * percentage);
        }
 
 
        public static float OutCubic(float percentage)
        {
            return (float) (1 - Math.Pow(1 - percentage, 3));
        }
 
        public static float InOutCubic(float percentage)
        {
            return (float) (percentage < 0.5 ? 4 * percentage * percentage * percentage : 1 - Math.Pow(-2 * percentage + 2, 3) / 2);
        }
 
        public static float InQuint(float percentage)
        {
            return (percentage * percentage * percentage * percentage * percentage);
        }
 
        public static float OutQuint(float percentage)
        {
            return (float) (1 - Math.Pow(1 - percentage, 5));
        }
 
        public static float InOutQuint(float percentage)
        {
            return (float) (percentage < 0.5 ? 16 * percentage * percentage * percentage * percentage * percentage : 1 - Math.Pow(-2 * percentage + 2, 5) / 2);
        }
 
        public static float InCirc(float percentage)
        {
            return (float) (1 - Math.Sqrt(1 - Math.Pow(percentage, 2)));
        }
 
        public static float OutCirc(float percentage)
        {
            return (float) (Math.Sqrt(1 - Math.Pow(percentage - 1, 2)));
        }
 
        public static float InOutCirc(float percentage)
        {
            return (float) (percentage < 0.5 ? (1 - Math.Sqrt(1 - Math.Pow(2 * percentage, 2))) / 2 : (Math.Sqrt(1 - Math.Pow(-2 * percentage + 2, 2)) + 1) / 2);
        }
 
        public static float InElastic(float percentage)
        {
            float c4 = (float) ((2 * Math.PI) / 3);
 
            return (float) (percentage == 0 ? 0 : percentage >= 1 ? 1 : -Math.Pow(2, 10 * percentage - 10) * Math.Sin((percentage * 10 - 10.75) * c4));
        }
 
        public static float OutElastic(float percentage)
        {
            float c4 = (float) ((2 * Math.PI) / 3);
 
            return (float) (percentage == 0 ? 0 : percentage >= 1 ? 1 : Math.Pow(2, -10 * percentage) * Math.Sin((percentage * 10 - 0.75) * c4) + 1);
        }
 
        public static float InOutElastic(float percentage)
        {
            float c5 = (float) ((2 * Math.PI) / 4.5);
 
            return (float) (percentage == 0 ? 0 : percentage >= 1 ? 1 : percentage < 0.5 ? -(Math.Pow(2, 20 * percentage - 10) * Math.Sin((20 * percentage - 11.125) * c5)) / 2 : (Math.Pow(2, -20 * percentage + 10) * Math.Sin((20 * percentage - 11.125) * c5)) / 2 + 1);
        }
 
 
        // row 2
        public static float InQuad(float percentage)
        {
            return (percentage * percentage);
        }
 
        public static float OutQuad(float percentage)
        {
            return (1 - (1 - percentage) * (1 - percentage));
        }
 
        public static float InOutQuad(float percentage)
        {
            return (float) (percentage < 0.5 ? 2 * percentage * percentage : 1 - Math.Pow(-2 * percentage + 2, 2) / 2);
        }
 
        public static float InQuart(float percentage)
        {
            return (percentage * percentage * percentage * percentage);
        }
 
        public static float OutQuart(float percentage)
        {
            return (float) (1 - Math.Pow(1 - percentage, 4));
        }
 
        public static float InOutQuart(float percentage)
        {
            return (float) (percentage < 0.5 ? 8 * percentage * percentage * percentage * percentage : 1 - Math.Pow(-2 * percentage + 2, 4) / 2);
        }
 
        public static float InExpo(float percentage)
        {
            return (float) (percentage == 0 ? 0 : Math.Pow(2, 10 * percentage - 10));
        }
 
        public static float OutExpo(float percentage)
        {
            return Math.Abs(percentage - 1f) < 0 ? 1f : 1 - Mathf.Pow(2, -10 * percentage);
        }
 
        public static float InOutExpo(float percentage)
        {
            return (float) (percentage == 0 ? 0 : percentage >= 1 ? 1 : percentage < 0.5 ? Math.Pow(2, 20 * percentage - 10) / 2 : (2 - Math.Pow(2, -20 * percentage + 10)) / 2);
        }
 
        public static float InBack(float percentage)
        {
            float c1 = 1.70158f;
            float c3 = c1 + 1;
        
            return (c3 * percentage * percentage * percentage - c1 * percentage * percentage);
        }
 
        public static float OutBack(float percentage)
        {
            float c1 = 1.70158f;
            float c3 = c1 + 1;
 
            return (float) (1 + c3 * Math.Pow(percentage - 1, 3) + c1 * Math.Pow(percentage - 1, 2));
        }
 
        public static float InOutBack(float percentage)
        {
            float c1 = 1.70158f;
            float c2 = c1 * 1.525f;
 
            return (float)(percentage < 0.5 ? (Math.Pow(2 * percentage, 2) * ((c2 + 1) * 2 * percentage - c2)) / 2 : (Math.Pow(2 * percentage - 2, 2) * ((c2 + 1) * (percentage * 2 - 2) + c2) + 2) / 2);
        }
 
        public static float InBounce(float percentage)
        {
            return (1 - OutBounce(1 - percentage));
        }
 
        public static float OutBounce(float percentage)
        {
            float n1 = 7.5625f;
            float d1 = 2.75f;
 
            if (percentage < 1 / d1) return (n1 * percentage * percentage);
            if (percentage < 2 / d1) return (float) (n1 * (percentage -= 1.5f / d1) * percentage + 0.75);
            if (percentage < 2.5 / d1) return (n1 * (percentage -= 2.25f / d1) * percentage + 0.9375f);
            return (n1 * (percentage -= 2.625f / d1) * percentage + 0.984375f);
        }
 
        public static float InOutBounce(float percentage)
        {
            return (percentage < 0.5 ? (1 - OutBounce(1 - 2 * percentage)) / 2 : (1 + OutBounce(2 * percentage - 1)) / 2);
        }
    }
}

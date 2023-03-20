using Toolbox.Easings;
using UnityEngine;

namespace Toolbox.MethodExtensions
{
    public static class AnimationCurveExtensions
    {

        /// <summary>
        /// Returns the surface of the curve
        /// </summary>
        /// <param name="targetCurve"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static float GetSurface(this AnimationCurve targetCurve, float interval = 0.01f)
        {
            return targetCurve.GetSurface(0, targetCurve.GetDuration());
        }

        /// <summary>
        /// Returns the surface of the curve
        /// </summary>
        /// <param name="targetCurve"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static float GetSurface(this AnimationCurve targetCurve, float start, float end, float interval = 0.01f)
        {
            var duration = end - start;
            var surface = 0f;
            var previousCurve = targetCurve.Evaluate(start);
            for (int i = 0; i < duration / interval; i++)
            {
                var currentCurve = targetCurve.Evaluate(start + interval * (i + 1));
                var avgCurve = (currentCurve + previousCurve) / 2;
                surface += avgCurve * interval;
                previousCurve = currentCurve;
            }

            return surface;
        }

        /// <summary>
        /// Get the duration of a curve
        /// </summary>
        /// <param name="targetCurve"></param>
        /// <returns></returns>
        public static float GetDuration(this AnimationCurve targetCurve)
        {
            var lastKey = targetCurve.keys[targetCurve.keys.Length - 1];
            return lastKey.time;
        }

        /// <summary>
        /// Generate curve from method 
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="function"></param>
        /// <param name="resolution"></param>
        public static void CurveFromMethod(this AnimationCurve curve, EasingTools.EasingFunction function, int resolution = 15)
        {
            curve = EasingTools.GenerateCurve(function, resolution);
        }

        /// <summary>
        /// Converts the curve to a new Curve to prevent connection (stack / heap) 
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="newCurve"></param>
        /// <returns></returns>
        public static AnimationCurve ChainToCurve(this AnimationCurve curve, AnimationCurve newCurve)
        {
            curve = new AnimationCurve(newCurve.keys);
            return curve;
        }

        /// <summary>
        /// Clones the curve and creates a new instance of a curve to prevent connection (stack / heap)
        /// </summary>
        /// <param name="oldCurve"></param>
        /// <returns></returns>
        public static AnimationCurve Clone(this AnimationCurve oldCurve)
        {
            var clonedCurve = new AnimationCurve();
            clonedCurve.keys = (Keyframe[])oldCurve.keys.Clone();

            return clonedCurve;
        }

        /// <summary>
        /// Generates a array with a size of 256 from a curve by evaluating the curve position of the current iteration
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static float[] GenerateCurveArray(this AnimationCurve self)
        {
            float[] returnArray = new float[256];
            for (int j = 0; j <= 255; j++)
            {
                returnArray[j] = self.Evaluate(j / 256f);
            }

            return returnArray;
        }
    }
}
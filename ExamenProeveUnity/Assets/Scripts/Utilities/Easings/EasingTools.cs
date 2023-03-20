using System;
using System.Collections.Generic;
using UnityEngine;

namespace Toolbox.Easings
{
    /// <summary>
    /// This class has a list of tools
    /// - Enum with all easings
    /// - dict that uses enum to receive method.
    /// - dict that used enum to receive AnimationCurve
    /// - function that transfers method to AnimationCurve
    /// and much more to come! 
    /// </summary>
    public static class EasingTools
    {
        public delegate float EasingFunction(float time);
        public static AnimationCurve GenerateCurve(EasingFunction method, int resolution)
        {
            var curve = new AnimationCurve();
            for (var i = 0; i < resolution; ++i)
            {
                var time = i / (resolution - 1f);
                var value = method(time);
                var key = new Keyframe(time, value);
                curve.AddKey(key);
            }
            for (var i = 0; i < resolution; ++i)
            {
                curve.SmoothTangents(i, 0f);
            }
            return curve;
        }
        public enum EasingType {
            Linear,
            
            InSine,
            OutSine,
            InOutSine,

            InCubic,
            OutCubic,
            InOutCubic,

            InQuint,
            OutQuint,
            InOutQuint,

            InCirc,
            OutCirc,
            InOutCirc,

            InElastic,
            OutElastic,
            InOutElastic,
            OutInElastic,
            
            InQuad,
            OutQuad,
            InOutQuad,

            InQuart,
            OutQuart,
            InOutQuart,

            InExpo,
            OutExpo,
            InOutExpo,

            InBack,
            OutBack,
            InOutBack,

            InBounce,
            OutBounce,
            InOutBounce,
        }
        
        public static Dictionary<EasingType, Func<float, float>> easingFunction = new Dictionary<EasingType, Func<float, float>>
        {
            {EasingType.Linear, Easing.Linear},
            
            {EasingType.InSine, Easing.InSine},
            {EasingType.OutSine, Easing.OutSine},
            {EasingType.InOutSine, Easing.InOutSine},
            
            {EasingType.InCubic, Easing.InCubic},
            {EasingType.OutCubic, Easing.OutCubic},
            {EasingType.InOutCubic, Easing.InOutCubic},
            
            {EasingType.InQuint, Easing.InQuint},
            {EasingType.OutQuint, Easing.OutQuint},
            {EasingType.InOutQuint, Easing.InOutQuint},

            {EasingType.InCirc, Easing.InCirc},
            {EasingType.OutCirc, Easing.OutCirc},
            {EasingType.InOutCirc, Easing.InOutCirc},

            {EasingType.InElastic, Easing.InElastic},
            {EasingType.OutElastic, Easing.OutElastic},
            {EasingType.InOutElastic, Easing.InOutElastic},
            
            {EasingType.InQuad, Easing.InQuad},
            {EasingType.OutQuad, Easing.OutQuad},
            {EasingType.InOutQuad, Easing.InOutQuad},

            {EasingType.InQuart, Easing.InQuart},
            {EasingType.OutQuart, Easing.OutQuart},
            {EasingType.InOutQuart, Easing.InOutQuart},

            {EasingType.InExpo, Easing.InExpo},
            {EasingType.OutExpo, Easing.OutExpo},
            {EasingType.InOutExpo, Easing.InOutExpo},

            {EasingType.InBack, Easing.InBack},
            {EasingType.OutBack, Easing.OutBack},
            {EasingType.InOutBack, Easing.InOutBack},
            
            {EasingType.InBounce, Easing.InBounce},
            {EasingType.OutBounce, Easing.OutBounce},
            {EasingType.InOutBounce, Easing.InOutBounce},
        };
        
        public static Dictionary<EasingType, AnimationCurve> easingCurve = new Dictionary<EasingType, AnimationCurve>
        {
            {EasingType.Linear, GenerateCurve(Easing.Linear, 2)},
            
            {EasingType.InSine, GenerateCurve(Easing.InSine, 15)},
            {EasingType.OutSine, GenerateCurve(Easing.OutSine, 15)},
            {EasingType.InOutSine, GenerateCurve(Easing.InOutSine, 15)},
            
            {EasingType.InCubic, GenerateCurve(Easing.InCubic, 15)},
            {EasingType.OutCubic, GenerateCurve(Easing.OutCubic, 15)},
            {EasingType.InOutCubic, GenerateCurve(Easing.InOutCubic, 15)},
            
            {EasingType.InQuint, GenerateCurve(Easing.InQuint, 15)},
            {EasingType.OutQuint, GenerateCurve(Easing.OutQuint, 15)},
            {EasingType.InOutQuint, GenerateCurve(Easing.InOutQuint, 15)},

            {EasingType.InCirc, GenerateCurve(Easing.InCirc, 15)},
            {EasingType.OutCirc, GenerateCurve(Easing.OutCirc, 15)},
            {EasingType.InOutCirc, GenerateCurve(Easing.InOutCirc, 15)},

            {EasingType.InElastic, GenerateCurve(Easing.InElastic, 30)},
            {EasingType.OutElastic, GenerateCurve(Easing.OutElastic, 30)},
            {EasingType.InOutElastic, GenerateCurve(Easing.InOutElastic, 30)},
            
            {EasingType.InQuad, GenerateCurve(Easing.InQuad, 15)},
            {EasingType.OutQuad, GenerateCurve(Easing.OutQuad, 15)},
            {EasingType.InOutQuad, GenerateCurve(Easing.InOutQuad, 15)},

            {EasingType.InQuart, GenerateCurve(Easing.InQuart, 15)},
            {EasingType.OutQuart, GenerateCurve(Easing.OutQuart, 15)},
            {EasingType.InOutQuart, GenerateCurve(Easing.InOutQuart, 15)},

            {EasingType.InExpo, GenerateCurve(Easing.InExpo, 15)},
            {EasingType.OutExpo, GenerateCurve(Easing.OutExpo, 15)},
            {EasingType.InOutExpo, GenerateCurve(Easing.InOutExpo, 15)},

            {EasingType.InBack, GenerateCurve(Easing.InBack, 15)},
            {EasingType.OutBack, GenerateCurve(Easing.OutBack, 15)},
            {EasingType.InOutBack, GenerateCurve(Easing.InOutBack, 15)},
            
            {EasingType.InBounce, GenerateCurve(Easing.InBounce, 15)},
            {EasingType.OutBounce, GenerateCurve(Easing.OutBounce, 15)},
            {EasingType.InOutBounce, GenerateCurve(Easing.InOutBounce, 15)},
        };
    }
    
    
   
}
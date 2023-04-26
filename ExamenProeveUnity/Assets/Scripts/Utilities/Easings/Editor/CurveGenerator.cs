using System;
using UnityEditor;
using UnityEngine;

namespace Toolbox.Easings
{
    /// <summary>
    /// This class adds easing methods to new Unity AnimationCurve preset
    /// </summary>
    public class CurveGenerator : MonoBehaviour
    {
        [MenuItem("Assets/Create/EasingCurves")]
        static void CreateAsset()
        {
            var curvePresetLibraryType = Type.GetType("UnityEditor.CurvePresetLibrary, UnityEditor");
            var library = ScriptableObject.CreateInstance(curvePresetLibraryType);

            AddCurve(library, Easing.Linear, 2, "Linear");

            AddCurve(library, Easing.OutQuad, 15, "QuadOut");
            AddCurve(library, Easing.InQuad, 15, "QuadIn");
            AddCurve(library, Easing.InOutQuad, 15, "QuadInOut");

            AddCurve(library, Easing.OutExpo, 15, "ExpoOut");
            AddCurve(library, Easing.InExpo, 15, "ExpoIn");
            AddCurve(library, Easing.InOutExpo, 15, "ExpoInOut");

            AddCurve(library, Easing.OutCubic, 15, "CubicOut");
            AddCurve(library, Easing.InCubic, 15, "CubicIn");
            AddCurve(library, Easing.InOutCubic, 15, "CubicInOut");

            AddCurve(library, Easing.OutQuart, 15, "QuartOut");
            AddCurve(library, Easing.InQuart, 15, "QuartIn");
            AddCurve(library, Easing.InOutQuart, 15, "QuartInOut");

            AddCurve(library, Easing.OutQuint, 15, "QuintOut");
            AddCurve(library, Easing.InQuint, 15, "QuintIn");
            AddCurve(library, Easing.InOutQuint, 15, "QuintInOut");

            AddCurve(library, Easing.OutCirc, 15, "CircOut");
            AddCurve(library, Easing.InCirc, 15, "CircIn");
            AddCurve(library, Easing.InOutCirc, 15, "CircInOut");

            AddCurve(library, Easing.OutSine, 15, "SineOut");
            AddCurve(library, Easing.InSine, 15, "SineIn");
            AddCurve(library, Easing.InOutSine, 15, "SineInOut");

            AddCurve(library, Easing.OutElastic, 30, "ElasticOut");
            AddCurve(library, Easing.InElastic, 30, "ElasticIn");
            AddCurve(library, Easing.InOutElastic, 30, "ElasticInOut");

            AddCurve(library, Easing.OutBounce, 30, "BounceOut");
            AddCurve(library, Easing.InBounce, 30, "BounceIn");
            AddCurve(library, Easing.InOutBounce, 30, "BounceInOut");

            AddCurve(library, Easing.OutBack, 30, "BackOut");
            AddCurve(library, Easing.InBack, 30, "BackIn");
            AddCurve(library, Easing.InOutBack, 30, "BackInOut");

            AssetDatabase.CreateAsset(library, "Assets/Toolbox/Easings/Editor/EasingCurves.curves");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void AddCurve(object library, EasingTools.EasingFunction easingFunction, int resolution, string name)
        {
            var curvePresetLibraryType = Type.GetType("UnityEditor.CurvePresetLibrary, UnityEditor");
            if (curvePresetLibraryType == null) return;
            var addMethod = curvePresetLibraryType.GetMethod("Add");
            if (addMethod == null) return;
            addMethod.Invoke(library, new object[]
            {
                EasingTools.GenerateCurve(easingFunction, resolution), name
            });
        }
    }
}

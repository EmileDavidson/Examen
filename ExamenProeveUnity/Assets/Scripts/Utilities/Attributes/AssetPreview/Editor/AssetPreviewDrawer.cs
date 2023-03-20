using UnityEditor;
using UnityEngine;

namespace Toolbox.Attributes
{
    [CustomPropertyDrawer(typeof(AssetPreviewAttribute))]
    public class AssetPreviewAttributeDrawer : PropertyDrawerBase
    {
        /// <summary>
        /// Returns property <see cref="Object"/> depending on the <see cref="referenceValue"/> type.
        /// </summary>
        private Object GetValidTarget(Object referenceValue)
        {
            if (!referenceValue) return null;
            return referenceValue switch
            {
                Component component => component.gameObject,
                _ => referenceValue
            };
        }

        /// <summary>
        /// Draws asset preview using previously created <see cref="Texture2D"/> and base rect.
        /// </summary>
        private void DrawAssetPreview(Rect rect, Texture2D previewTexture)
        {
            //cache indent difference
            var indent = rect.width - EditorGUI.IndentedRect(rect).width;
            //set image base properties
            var width = Mathf.Clamp(Attribute.Width, 0, previewTexture.width);
            var height = Mathf.Clamp(Attribute.Height, 0, previewTexture.height);
            
            Style.TextureStyle.normal.background = previewTexture;
            //set additional height as preview + 2x spacing + 2x frame offset
            rect.width = width + Style.Offset + indent;
            rect.height = height + Style.Offset;
            rect.y += Style.Height + Style.Spacing;
            //draw background frame
            EditorGUI.LabelField(rect, GUIContent.none, Style.PreviewStyle);
            rect.width = width + indent;
            rect.height = height;
            //adjust image to frame center
            rect.y += Style.Offset / 2;
            rect.x += Style.Offset / 2;
            //draw texture without label
            EditorGUI.LabelField(rect, GUIContent.none, Style.TextureStyle);
        }

        private void DrawAssetPreviewEmpty(Rect rect)
        {
            //cache indent difference
            var indent = rect.width - EditorGUI.IndentedRect(rect).width;
            //set image base properties
            var width = Mathf.Clamp(Attribute.Width, 0, 25);
            var height = Mathf.Clamp(Attribute.Height, 0, 25);

            //set additional height as preview + 2x spacing + 2x frame offset
            rect.width = width + Style.Offset + indent;
            rect.height = height + Style.Offset;
            rect.y += Style.Height + Style.Spacing;
            //draw background frame
            EditorGUI.LabelField(rect, GUIContent.none, Style.PreviewStyle);
            rect.width = width + indent;
            rect.height = height;
            //adjust image to frame center
            rect.y += Style.Offset / 2;
            rect.x += Style.Offset / 2;
        }


        protected override float GetPropertyHeightSafe(SerializedProperty property, GUIContent label)
        {
            //return native height 
            if (!property.objectReferenceValue)
            {
                return base.GetPropertyHeightSafe(property, label);
            }

            //set additional height as preview + 2x spacing + 2x frame offset
            var additionalHeight = Attribute.Height + Style.Offset * 2 + Style.Spacing * 2;
            if (!Attribute.UseLabel) additionalHeight -= Style.Height;
            return Style.Height + additionalHeight;
        }

        protected override void OnGUISafe(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Attribute.UseLabel) EditorGUI.PropertyField(position, property, label, true);
           
            else position.y -= Style.Height;
            

            var target = GetValidTarget(property.objectReferenceValue);
            if (!target) return;
            
            var previewTexture = AssetPreview.GetAssetPreview(target);
            if (previewTexture == null)
            {
                DrawAssetPreviewEmpty(position);
                return;
            }

            //finally draw preview texture of the target
            DrawAssetPreview(position, previewTexture);
        }


        protected override bool IsPropertyValid(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.ObjectReference;
        }


        private AssetPreviewAttribute Attribute => attribute as AssetPreviewAttribute;


        private static class Style
        {
            public static readonly float Offset = 6.0f;
            public static readonly float Height = EditorGUIUtility.singleLineHeight;
            public static readonly float Spacing = 2.0f;

            public static readonly GUIStyle TextureStyle;
            public static readonly GUIStyle PreviewStyle;

            static Style()
            {
                TextureStyle = new GUIStyle();
                PreviewStyle = new GUIStyle("helpBox");
            }
        }
    }
}
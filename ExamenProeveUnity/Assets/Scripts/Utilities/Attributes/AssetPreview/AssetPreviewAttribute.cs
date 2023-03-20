using System;
using UnityEngine;

namespace Toolbox.Attributes
{
    /// <summary>
    /// Draws additional preview texture for the provided object.
    /// 
    /// <para>Supported types: any <see cref="UnityEngine.Object"/>.</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class AssetPreviewAttribute : PropertyAttribute
    {
        public AssetPreviewAttribute(float width = 64, float height = 64, bool useLabel = true)
        {
            Width = width;
            Height = height;
            UseLabel = useLabel;
        }

        public float Width { get; }

        public float Height { get; }

        public bool UseLabel { get; }
    }
}
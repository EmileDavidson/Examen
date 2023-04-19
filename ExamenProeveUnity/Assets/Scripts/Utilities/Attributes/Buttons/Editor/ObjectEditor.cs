﻿using UnityEditor;

namespace Toolbox.Attributes
{
    using Object = UnityEngine.Object;

    /// <summary>
    /// Custom inspector for <see cref="UnityEngine.Object"/> including derived classes.
    /// </summary>
    [CustomEditor(typeof(Object), true)]
    [CanEditMultipleObjects]
    internal class ObjectEditor : Editor
    {
        private ButtonsDrawer _buttonsDrawer;

        private void OnEnable()
        {
            _buttonsDrawer = new ButtonsDrawer(target);
        }

        public override void OnInspectorGUI()
        {
            if (target == null) { return; }
            DrawDefaultInspector();
            _buttonsDrawer.DrawButtons(targets);
        }
    }
}

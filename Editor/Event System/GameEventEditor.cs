using UnityEngine;
using UnityEditor;

namespace Amheklerior.Core.EventSystem.Editor {

    [CustomEditor(typeof(GameEvent), true)]
    public class GameEventEditor : UnityEditor.Editor {

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Raise")) ((GameEvent) target).Raise();
            EditorGUI.EndDisabledGroup();
        }
    }

}
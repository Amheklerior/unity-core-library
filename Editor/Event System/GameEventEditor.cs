using UnityEngine;
using UnityEditor;
using Amheklerior.Core.EventSystem;

[CustomEditor(typeof(GameEvent), true)]
public class GameEventEditor : Editor {
    
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Raise")) ((GameEvent) target).Raise();
        EditorGUI.EndDisabledGroup();
    }
    
}

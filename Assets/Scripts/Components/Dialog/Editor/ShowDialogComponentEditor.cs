using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShowDialogComponent))]
public class ShowDialogComponentEditor : UnityEditor.Editor
{
    private SerializedProperty _modeProp;
    private SerializedProperty _onCompleteProperty;
    private void OnEnable()
    {
        _modeProp = serializedObject.FindProperty("_mode");
        _onCompleteProperty = serializedObject.FindProperty("_onComplete");
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(_modeProp);

        ShowDialogComponent.Mode mode;
        if (_modeProp.GetEnum(out mode))
        {
            switch (mode)
            {
                case ShowDialogComponent.Mode.Bound:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_bound"));
                    break;
                case ShowDialogComponent.Mode.External:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_external"));
                    break;
                default:
                    break;
            }
        }
        EditorGUILayout.PropertyField(_onCompleteProperty);
        serializedObject.ApplyModifiedProperties();
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelBuilder))]
public class LevelBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        this.serializedObject.Update();
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("textureMap"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("cellSize"), true);
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("defaultObject"), true);

        EditorGUILayout.Separator();

        //EditorGUILayout.PropertyField(this.serializedObject.FindProperty("colorObjectList"), true);
        EditorGUILayout.LabelField(new GUIContent("Color-to-object function"), new GUIStyle() { fontStyle = FontStyle.Bold });
        SerializedProperty listProperty = this.serializedObject.FindProperty("colorObjectList");
        for (int i = 0; i < listProperty.arraySize; i++)
        {
            LevelBuilderEditor.ShowListProperty(listProperty, listProperty.GetArrayElementAtIndex(i), i);
        }

        if (GUILayout.Button(new GUIContent("+", "Add a new element.")))
            listProperty.InsertArrayElementAtIndex(listProperty.arraySize);

        this.serializedObject.ApplyModifiedProperties();
    }

    private static void ShowListProperty(SerializedProperty listProperty, SerializedProperty listItemProperty, int listItemIndex)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.PropertyField(listItemProperty, GUIContent.none);

        if (GUILayout.Button(new GUIContent("Up", "Move the element to a previous index."), EditorStyles.miniButtonLeft, GUILayout.Width(40.0f)))
            listProperty.MoveArrayElement(listItemIndex, listItemIndex - 1);
        
        if (GUILayout.Button(new GUIContent("Down", "Move the element to a next index."), EditorStyles.miniButtonMid, GUILayout.Width(40.0f)))
            listProperty.MoveArrayElement(listItemIndex, listItemIndex + 1);
        
        if (GUILayout.Button(new GUIContent("-", "Remove the element."), EditorStyles.miniButtonRight, GUILayout.Width(24.0f)))
        {
            int oldSize = listProperty.arraySize;
            listProperty.DeleteArrayElementAtIndex(listItemIndex);
            if (listProperty.arraySize == oldSize)
                listProperty.DeleteArrayElementAtIndex(listItemIndex);
        }

        EditorGUILayout.EndHorizontal();
    }

}
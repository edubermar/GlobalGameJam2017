using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ColorObjectPair))]
public class ColorObjectPairPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect drawerRect, SerializedProperty property, GUIContent label)
    {
        // Inicializar propiedad
        label = EditorGUI.BeginProperty(drawerRect, label, property);

        // Crear etiqueta
        Rect contentRect = EditorGUI.PrefixLabel(drawerRect, label);

        // Guardar indentación
        int indentLevel = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calcular rectángulos
        Rect colorRect = new Rect(contentRect.x, contentRect.y, 128, contentRect.height);
        Rect gameObjectRect = new Rect(contentRect.x + 128, contentRect.y, contentRect.width - 128, contentRect.height);
		
		// Insertar campos
        EditorGUI.PropertyField(colorRect, property.FindPropertyRelative("color"), GUIContent.none);
        EditorGUI.PropertyField(gameObjectRect, property.FindPropertyRelative("gameObject"), GUIContent.none);
        //EditorGUI.PropertyField(gameObjectRect, property.FindPropertyRelative("gameObject"), new GUIContent("Object", "Object that will be instantiated when the color matches."));
		
        // Restaurar indentación
        EditorGUI.indentLevel = indentLevel;

        // Finalizar propiedad
        EditorGUI.EndProperty();
    }

}
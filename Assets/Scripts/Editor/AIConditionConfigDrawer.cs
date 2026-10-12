using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AIConditionConfig))]
public class AIConditionConfigDrawer : PropertyDrawer
{
    public override void OnGUI(
        Rect position,
        SerializedProperty property,
        GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty compareType =
            property.FindPropertyRelative("compareType");

        SerializedProperty valueParam =
            property.FindPropertyRelative("valueParam");

        float line = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        Rect compareRect =
            new Rect(position.x, position.y, position.width, line);

        Rect valueRect =
            new Rect(
                position.x,
                position.y + line + spacing,
                position.width,
                EditorGUI.GetPropertyHeight(valueParam, true)
            );

        EditorGUI.PropertyField(
            compareRect,
            compareType,
            new GUIContent("Compare"));

        EditorGUI.PropertyField(
            valueRect,
            valueParam,
            new GUIContent("Value"),
            true);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(
        SerializedProperty property,
        GUIContent label)
    {
        SerializedProperty valueParam =
            property.FindPropertyRelative("valueParam");

        return EditorGUIUtility.singleLineHeight
             + EditorGUIUtility.standardVerticalSpacing
             + EditorGUI.GetPropertyHeight(valueParam, true);
    }
}
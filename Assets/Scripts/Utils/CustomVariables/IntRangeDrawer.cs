#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace FirerusUtilities
{
    [CustomPropertyDrawer(typeof(IntRange))]
    public class IntRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Rect minRect = new Rect(position.x, position.y, 50, position.height);
            Rect maxRect = new Rect(position.x + 75, position.y, 50, position.height);

            EditorGUI.PropertyField(minRect, property.FindPropertyRelative("_min"), GUIContent.none);
            EditorGUI.PropertyField(maxRect, property.FindPropertyRelative("_max"), GUIContent.none);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
#endif

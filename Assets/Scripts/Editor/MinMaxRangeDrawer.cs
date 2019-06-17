using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangedFloat), true)]
public class MinMaxRangeDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        SerializedProperty minProp = property.FindPropertyRelative("minValue");
        SerializedProperty maxProp = property.FindPropertyRelative("maxValue");

        float min = minProp.floatValue;
        float max = maxProp.floatValue;

        float minRange = 0.0f;
        float maxRange = 1.0f;

        MinMaxRange[] ranges = (MinMaxRange[])fieldInfo.GetCustomAttributes(typeof(MinMaxRange), true);
        if(ranges.Length > 0) {
            minRange = ranges[0].min;
            maxRange = ranges[0].max;
        }

        const float labelWidth = 40.0f;

        var boundsLabel = new Rect(position);
        boundsLabel.width = labelWidth;
        GUI.Label(boundsLabel, new GUIContent(min.ToString("F2")));
        position.xMin += labelWidth;

        var boundsLabel2 = new Rect(position);
        boundsLabel2.xMin = boundsLabel2.xMax - labelWidth;
        GUI.Label(boundsLabel2, new GUIContent(max.ToString("F2")));
        position.xMax -= labelWidth;

        EditorGUI.BeginChangeCheck();
        EditorGUI.MinMaxSlider(position, ref min, ref max, minRange, maxRange);
        if(EditorGUI.EndChangeCheck()) {
            minProp.floatValue = min;
            maxProp.floatValue = max;
        }

        EditorGUI.EndProperty();
    }
}

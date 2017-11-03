using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ObjectGetter))]
public class ObjectGetterDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var enumLabelSize = 80;
        var fieldLabelSize = 50;
        var enumLabelPosition = new Rect(position.x, position.y, enumLabelSize, position.height);
        var enumPosition = new Rect(enumLabelPosition.x + enumLabelPosition.width + 5, position.y, 80, position.height);
        var fieldLabelPosition = new Rect(enumPosition.x + enumPosition.width + 5, position.y, fieldLabelSize, position.height);
        var fieldPosition = new Rect(fieldLabelPosition.x + fieldLabelPosition.width + 5, position.y, position.width - enumLabelSize - fieldLabelSize - 80 - 15, position.height);

        EditorGUI.LabelField(enumLabelPosition, new GUIContent("Object from"));
        var typeProperty = property.FindPropertyRelative("type");
        var selectedTypeIndex = EditorGUI.Popup(enumPosition, typeProperty.enumValueIndex, typeProperty.enumDisplayNames);

        typeProperty.enumValueIndex = selectedTypeIndex;

        SerializedProperty serializedProperty;
        GUIContent propertyLabel;
        if ((GetterType)selectedTypeIndex == GetterType.Pool)
        {
            serializedProperty = property.FindPropertyRelative("objectPool");
            propertyLabel = new GUIContent("Pool");
        }
        else
        {
            serializedProperty = property.FindPropertyRelative("objectPrefab");
            propertyLabel = new GUIContent("Prefab");
        }

        EditorGUI.LabelField(fieldLabelPosition, propertyLabel);
        EditorGUI.PropertyField(fieldPosition, serializedProperty, GUIContent.none);

        EditorGUI.EndProperty();
    }
}

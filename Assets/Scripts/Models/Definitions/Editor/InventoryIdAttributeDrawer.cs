using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InventoryIdAttribute))]
public class InventoryIdAttributeDrawer : PropertyDrawer
{   
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var defs = DefsFacade.I.Items.ItemsForEditor;
        var ids = new List<string>();
        foreach (var item in defs)
        {
            ids.Add(item.Id);
        }

        var index = ids.IndexOf(property.stringValue);
        index = EditorGUI.Popup(position, property.displayName, index, ids.ToArray());
        property.stringValue = ids[index];
    }
}

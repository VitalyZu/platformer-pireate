using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public static class SerializedPropertyExtension {
    public static bool GetEnum<TEnumType>(this SerializedProperty prop, out TEnumType retValue) where TEnumType : Enum
    {
        retValue = default;

        var names = prop.enumNames;
        if (names == null || names.Length == 0) return false;

        var enumName = names[prop.enumValueIndex];

        retValue = (TEnumType)Enum.Parse(typeof(TEnumType), enumName);
        return true;
    }
}

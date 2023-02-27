using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExstension
{
    public static bool IsInLayer(this GameObject go, LayerMask layer) 
    {
        return layer == (layer | 1 << go.layer);
    }

    public static InterfaceType GetInterface<InterfaceType>(this GameObject go)
    {
        var components = go.GetComponents<Component>();
        foreach (var component in components)
        {
            if (component is InterfaceType type)
            {
                return type;
            }
        }
        return default;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static bool IsValid(this GameObject obj)
    {
        return obj != null && obj.activeInHierarchy;
    }

    public static bool IsValid(this Component component)
    {
        return component != null && component.gameObject.activeInHierarchy;
    }

    public static bool IsContain(this LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }
}

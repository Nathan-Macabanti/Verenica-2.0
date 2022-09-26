using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void SingletonErrorMessage(object obj)
    {
        Debug.LogError("Multiple instances of " + obj.GetType().Name + " exists");
    }
}

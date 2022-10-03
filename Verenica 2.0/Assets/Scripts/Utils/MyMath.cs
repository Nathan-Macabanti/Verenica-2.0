using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMath
{
    public static bool isOdd(int num)
    {
        var x = num;
        if (x % 2 == 0) { return false; }
        return true;
    }

    public static bool isOdd(float num)
    {
        var x = num;
        if (x % 2 == 0) { return false; }
        return true;
    }

    public static float Velocity1D(float displacement, float time)
    {
        return displacement / time;
    }
}

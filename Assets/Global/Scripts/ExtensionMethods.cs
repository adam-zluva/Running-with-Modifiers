using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class ExtensionMethods
{
    public static int Mod(this int x, int m)
    {
        return (x % m + m) % m;
    }

    public static float Mod(this float x, float m)
    {
        return (x % m + m) % m;
    }

    public static Vector3 Clamp(this Vector3 vector, float magnitude)
    {
        return Vector3.ClampMagnitude(vector, magnitude);
    }

    public static int ToMilliseconds(this float s)
    {
        return (int)(s * 1000);
    }

    public static void ForEach<T>(this T[] arr, UnityAction<T> action)
    {
        foreach (var item in arr)
        {
            action.Invoke(item);
        }
    }

    public static void ForEach<T>(this List<T> arr, UnityAction<T> action)
    {
        foreach (var item in arr)
        {
            action.Invoke(item);
        }
    }

    public static void BackwardsFor<T>(this List<T> arr, UnityAction<T> action)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            action.Invoke(arr[arr.Count - 1]);
        }
    }

    public static float Remap(this float v, float from, float to, float newFrom, float newTo)
    {
        return Mathf.Lerp(newFrom, newTo, Mathf.InverseLerp(from, to, v));
    }
}

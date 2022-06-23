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

    public static string OperatorSymbol(this MathExpression.Operation operation)
    {
        switch (operation)
        {
            case MathExpression.Operation.Addition:
                return "+";
            case MathExpression.Operation.Subtraction:
                return "-";
            case MathExpression.Operation.Multiplication:
                return "x";
            case MathExpression.Operation.Division:
                return ":";
            default:
                return "?";
        }
    }

    public static float Calculate(this MathExpression.Operation operation, float leftSide, float rightSide)
    {
        switch (operation)
        {
            case MathExpression.Operation.Addition:
                return leftSide + rightSide;
            case MathExpression.Operation.Subtraction:
                return leftSide - rightSide;
            case MathExpression.Operation.Multiplication:
                return leftSide * rightSide;
            case MathExpression.Operation.Division:
                return leftSide / rightSide;
            default:
                return leftSide;
        }
    }

    public static Vector3 Multiply(this Vector3 vector, Vector3 otherVector)
    {
        return new Vector3(vector.x * otherVector.x, vector.y * otherVector.y, vector.z * otherVector.z);
    }
}

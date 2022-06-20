using UnityEngine;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFeatures : MonoBehaviour
{
    public void Log(string msg)
    {
        Debug.Log(msg);
    }

    public void Log(float number)
    {
        Debug.Log(number);
    }
}

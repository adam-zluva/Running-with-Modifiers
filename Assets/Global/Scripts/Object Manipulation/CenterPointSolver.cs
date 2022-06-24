using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CenterPointSolver : MonoBehaviour
{
    public UnityEvent<Vector3> onCenterPointCalculated;

    public void CalculateCenterPoint(List<Transform> transforms)
    {
        onCenterPointCalculated.Invoke(GetCenterPoint(transforms));
    }

    Vector3 GetCenterPoint(List<Transform> transforms)
    {
        if (transforms.Count == 0) return Vector3.zero;
        else if (transforms.Count == 1) return transforms[0].position;

        Vector3 center = transforms[0].position;
        for (int i = 1; i < transforms.Count; i++)
        {
            var t = transforms[i];
            center = Vector3.Lerp(center, t.position, 0.5f);
        }

        return center;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    [SerializeField] private Reference<Transform> targetReference;
    private Transform target => targetReference.value;

    [SerializeField] private Vector3 defaultFaceDirection;
    [SerializeField] private Vector3 directionMultiplier;
    [SerializeField] private float steerSmoothTime = 0f;

    private Vector3 lastValidDirection;
    private Vector3 smoothVelocity;

    private void Start()
    {
        lastValidDirection = defaultFaceDirection;
    }

    private void Update()
    {
        Vector3 direction = (target.position - transform.position).Multiply(directionMultiplier);
        if (direction == Vector3.zero) direction = lastValidDirection;

        transform.forward = Vector3.SmoothDamp(transform.forward, direction, ref smoothVelocity, steerSmoothTime);
        lastValidDirection = transform.forward;
    }

    public void SetTarget(Transform target)
    {
        if (target) targetReference.value = target;
        else ResetTarget();
    }

    public void ResetTarget()
    {
        targetReference.BuildReferenceValue();
    }
}

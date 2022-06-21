using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMovingDirection : MonoBehaviour
{
    [SerializeField] private Vector3 defaultFaceDirection;
    [SerializeField] private Vector3 directionMultiplier = Vector3.one;
    [SerializeField] private float steerSpeed = 0f;

    private Vector3 lastValidDirection;
    private Vector3 lastPosition;
    private Vector3 steerVelocity;

    private void Start()
    {
        lastValidDirection = defaultFaceDirection;
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 position = transform.position;
        Vector3 direction = position - lastPosition;

        if (direction == Vector3.zero) direction = lastValidDirection;

        Vector3 targetlookDirection = new Vector3(
            direction.x * directionMultiplier.x,
            direction.y * directionMultiplier.y,
            direction.z * directionMultiplier.z);

        Vector3 lookDirection = Vector3.SmoothDamp(lastValidDirection, targetlookDirection, ref steerVelocity, steerSpeed);
        transform.forward = lookDirection;

        lastValidDirection = lookDirection;
        lastPosition = position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;

    private Vector3 offset;
    private Vector3 smoothVelocity;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref smoothVelocity, smoothTime);
    }
}

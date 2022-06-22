using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 bounds;
    [SerializeField] private LayerMask groundLayer;

    private Vector2 _position;
    public Vector2 position
    {
        get => _position;
        set
        {
            _position = value;
            RecalculatePosition();
        }
    }

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void RecalculatePosition()
    {
        float point = target.localPosition.x;

        Ray ray = cam.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer.value))
        {
            var localHit = (target.worldToLocalMatrix * hit.point);
            point = localHit.x;
        }

        target.localPosition = new Vector3(Mathf.Clamp(point, bounds.x, bounds.y), 0f, 0f);
    }

    public void ResetPosition()
    {
        target.localPosition = Vector3.zero;
    }
}

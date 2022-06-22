using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Transform movementTarget;
    [SerializeField] private Vector2 moveTargetBounds;

    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Vector2 camTargetBounds;

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
        // Movement Target
        float point = movementTarget.localPosition.x;

        Ray ray = cam.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer.value))
        {
            var localHit = (movementTarget.worldToLocalMatrix * hit.point);
            point = Mathf.Clamp(localHit.x, moveTargetBounds.x, moveTargetBounds.y);
        }

        movementTarget.localPosition = new Vector3(point, 0f, 0f);

        // Camera Target
        float cameraTargetX = Mathf.Lerp(0f, point, 0.5f);
        cameraTarget.localPosition = new Vector3(cameraTargetX, 0f, 0f);
    }

    public void ResetPosition()
    {
        movementTarget.localPosition = Vector3.zero;
        cameraTarget.localPosition = Vector3.zero;
    }
}

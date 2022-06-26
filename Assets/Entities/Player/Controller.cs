using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float movementTargetSpeed;
    [SerializeField] private Transform movementTarget;
    [SerializeField] private Vector2 moveTargetBounds;

    [SerializeField] private Transform cameraTarget;

    public float touchAxis { get; set; }

    public void RecalculatePosition()
    {
        // Movement Target
        float increment = touchAxis > Screen.width / 2 ? 1f : -1f;
        float point = movementTarget.localPosition.x + increment * movementTargetSpeed * Time.deltaTime;
        point = Mathf.Clamp(point, moveTargetBounds.x, moveTargetBounds.y);
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

    private void Update()
    {
        RecalculatePosition();
    }
}

using UnityEngine.Events;
using UnityEngine;

public class TransformLimiter : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private Vector2 xRange;
    [SerializeField] private Vector2 yRange;
    [SerializeField] private Vector2 zRange;

    [SerializeField] private UnityEvent onPositionReset;

    private void Update()
    {
        Vector3 position = transform.localPosition;

        position.x = RestrictPosition(ref position.x, xRange);
        position.y = RestrictPosition(ref position.y, yRange);
        position.z = RestrictPosition(ref position.z, zRange);

        if (position != transform.localPosition) onPositionReset.Invoke();

        transform.localPosition = position;
    }

    float RestrictPosition(ref float position, Vector2 range)
    {
        if (range == Vector2.zero) return position;
        return position < range.x ? range.y : (position > range.y ? range.x : position);
    }
}

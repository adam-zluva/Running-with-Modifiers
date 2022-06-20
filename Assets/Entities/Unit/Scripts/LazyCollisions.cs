using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LazyCollisions : MonoBehaviour
{
    [SerializeField] private float pushStrength = 1f;
    [SerializeField] private Rigidbody rb;
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 pushDirection = transform.position - other.transform.position;
        Vector3 pushForce = (pushDirection / Mathf.Max(pushDirection.magnitude, Mathf.Epsilon)) * pushStrength;
        rb.AddForce(pushForce, ForceMode.Acceleration);
    }
}

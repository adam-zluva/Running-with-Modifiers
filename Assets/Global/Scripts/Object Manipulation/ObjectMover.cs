using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObjectMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private bool useRigidbody;
    [Space]
    [SerializeField] private MovementType movementType;
    [SerializeField, ShowIf("movementType", MovementType.Translation)] private Vector3 moveDirection;
    [SerializeField, ShowIf("movementType", MovementType.Target)] private Reference<Transform> followTarget;
    [Space]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float _speedMultiplier = 1f;
    public float speedMultiplier { get => _speedMultiplier; set => _speedMultiplier = value; }

    private UnityAction movementAction;
    private Rigidbody rb;

    private void Awake()
    {
        if (useRigidbody) rb = GetComponent<Rigidbody>();
        BuildMovementAction();
    }

    private void Update()
    {
        movementAction?.Invoke();
    }

    public void ResetTarget()
    {
        followTarget.BuildReferenceValue();
    }

    public void SetTarget(Transform target)
    {
        if (target != null) followTarget.value = target;
        else ResetTarget();
    }

    public void TeleportTo(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }
    
    public void TeleportTo(Transform transform)
    {
        this.transform.position = transform.position;
    }

    void BuildMovementAction()
    {
        switch (movementType)
        {
            case MovementType.Translation:
                movementAction = useRigidbody ? () =>
                {
                    rb.MovePosition(rb.position + moveDirection * speed * speedMultiplier * Time.deltaTime);
                }
                : () =>
                {
                    transform.Translate(moveDirection * speed * speedMultiplier * Time.deltaTime);
                };
                break;
            case MovementType.Target:
                movementAction = useRigidbody ? () =>
                {
                    Vector3 direction = (followTarget.value.position - transform.position).normalized;
                    rb.AddForce(direction * speed * speedMultiplier * Time.deltaTime, ForceMode.Acceleration);
                }
                : () =>
                {
                    Vector3 direction = (followTarget.value.position - transform.position).normalized;
                    transform.Translate(direction * speed * speedMultiplier * Time.deltaTime);
                };
                break;
        }
    }

    public enum MovementType
    {
        Translation, Target
    }
}

using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector3 moveSpeed;

    [SerializeField] private float _speedMultiplier = 1f;
    public float speedMultiplier { get => _speedMultiplier; set => _speedMultiplier = value; }

    private void Update()
    {
        transform.Translate(moveSpeed * speedMultiplier * Time.deltaTime);
    }

    public void TeleportTo(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }
    
    public void TeleportTo(Transform transform)
    {
        this.transform.position = transform.position;
    }
}

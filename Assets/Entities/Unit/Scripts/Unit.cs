using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectMover))]
public class Unit : MonoBehaviour
{
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask enemyMask;

    public UnityEvent onUnitDeath;
    public UnityEvent<Transform> onEnemyFound;

    private void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;
        // if layer is in enemyMask
        if ((enemyMask.value & (1 << layer)) != 0)
        {
            Death();
        }
    }

    void Death()
    {
        onUnitDeath.Invoke();
        onUnitDeath.RemoveAllListeners();
    }

    public void SearchForEnemy()
    {
        Transform target = GetNearestEnemy();
        if (target && target.TryGetComponent(out Unit unit))
        {
            SetTarget(unit);
        }
        else UnityEngine.Debug.LogWarning("No enemy found");
    }

    void SetTarget(Unit otherUnit)
    {
        onEnemyFound.Invoke(otherUnit.transform);
        otherUnit.onUnitDeath.AddListener(SearchForEnemy);
    }

    Transform GetNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyMask.value);
        Transform nearestTransform = null;
        float nearestDistance = Mathf.Infinity;
        foreach (var collider in colliders)
        {
            Vector3 position = collider.transform.position;
            float distance = Vector3.Distance(transform.position, position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTransform = collider.transform;
            }
        }

        return nearestTransform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

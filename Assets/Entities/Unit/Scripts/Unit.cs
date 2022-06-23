using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private ObjectMover mover;
    [SerializeField] private string enemyTag;

    public UnityEvent onInit;
    public UnityEvent onDeath;
    public UnityEvent onDispose;
    public UnityEvent onEncounterStart;
    public UnityEvent onEncounterEnd;

    private List<Unit> targets;

    public void Init()
    {
        onInit.RemoveAllListeners();
        onDeath.RemoveAllListeners();
        onDispose.RemoveAllListeners();

        onInit.Invoke();
    }

    public void Death()
    {
        onDeath.Invoke();
    }

    public void Dispose()
    {
        onDispose.Invoke();
    }

    public void StartEncounter(List<Unit> targets)
    {
        onEncounterStart.Invoke();
        this.targets = targets;
        SetTarget(GetNearestTarget());
    }

    public void EndEncounter()
    {
        onEncounterEnd.Invoke();
    }

    public void SetTarget(Unit target)
    {
        if (target == null)
        {
            EndEncounter();
            return;
        }

        target.onDeath.AddListener(() =>
        {
            targets.Remove(target);
            SetTarget(GetNearestTarget());
        });
        mover.SetTarget(target.transform);
    }

    private Unit GetNearestTarget()
    {
        Unit nearestUnit = null;
        float nearestDistance = Mathf.Infinity;
        foreach (var target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < nearestDistance)
            {
                nearestUnit = target;
                nearestDistance = distance;
            }
        }

        return nearestUnit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            if (other.TryGetComponent(out Unit otherUnit))
            {
                otherUnit.Death();
            }

            Death();
        }
    }
}

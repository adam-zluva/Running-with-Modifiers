using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private string enemyTag;

    public UnityEvent onInit;
    public UnityEvent onDeath;
    public UnityEvent onDispose;
    public UnityEvent onEncounterStart;
    public UnityEvent<Transform> onTargetSet;
    public UnityEvent onEncounterEnd;

    public UnitGroup parentGroup { get; set; }

    private List<Unit> targets;
    private bool dead;

    public void Init()
    {
        onInit.RemoveAllListeners();
        onDeath.RemoveAllListeners();
        onDispose.RemoveAllListeners();

        onInit.Invoke();
        dead = false;
    }

    public void Death()
    {
        if (dead) return;

        onDeath.Invoke();
        dead = true;
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
        if (dead) return;
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
        onTargetSet.Invoke(target.transform);
    }

    public void Unparent()
    {
        parentGroup.UnparentUnit(this);
        parentGroup = null;
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
            Death();
        }
    }
}

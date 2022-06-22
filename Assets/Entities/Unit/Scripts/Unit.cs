using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObjectPool pool;
    public UnityEvent onUnitDeath;
    [SerializeField] private ObjectMover mover;
    [SerializeField] private string enemyTag;

    private GameObject currentTarget;
    private List<Unit> targets;

    private bool dead;

    public void Init()
    {
        dead = false;
        mover.SetTarget(null);
    }

    public void StartEncounter(UnitGroup enemyGroup)
    {
        targets = new List<Unit>(enemyGroup.units);

        foreach (var target in targets)
        {
            target.onUnitDeath.AddListener(() =>
            {
                targets.Remove(target);
            });
        }

        SetTarget(GetNearestUnit());
    }

    public void EndEncounter()
    {
        mover.SetTarget(null);
    }

    public void SetTarget(Unit target)
    {
        if (target == null)
        {
            EndEncounter();
            return;
        }

        currentTarget = target.gameObject;
        target.onUnitDeath.AddListener(() =>
        {
            SetTarget(GetNearestUnit());
        });
        mover.SetTarget(currentTarget.transform);
        mover.enabled = true;
    }

    private Unit GetNearestUnit()
    {
        float nearestDist = Mathf.Infinity;
        Unit unit = null;
        foreach (var target in targets)
        {
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist < nearestDist)
            {
                nearestDist = dist;
                unit = target;
            }
        }

        return unit;
    }

    public void Death()
    {
        if (dead) return;

        dead = true;
        onUnitDeath.Invoke();
        Dispose();
    }

    public void Dispose()
    {
        onUnitDeath.RemoveAllListeners();
        pool.ReturnObject(gameObject);
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

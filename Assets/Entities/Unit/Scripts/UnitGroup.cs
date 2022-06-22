using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitGroup : MonoBehaviour
{
    [SerializeField] private PoolSpawner poolSpawner;
    [SerializeField] private UnityEvent onGroupEmpty;

    private List<Unit> _units = new List<Unit>();
    public List<Unit> units => _units;
    public int numberOfUnits => units.Count;

    public void Clear()
    {
        units.ForEach(unit =>
        {
            unit.Dispose();
        });
        units.Clear();
    }

    public void AddUnit(Unit unit)
    {
        units.Add(unit);
        unit.onUnitDeath.AddListener(() =>
        {
            RemoveUnit(unit);
        });
    }

    public void RemoveUnit(Unit unit)
    {
        units.Remove(unit);

        if (_units.Count == 0)
        {
            onGroupEmpty.Invoke();
        }
    }

    public GameObject SpawnUnit(Vector3 position, bool worldSpace = false)
    {
        var unitObj = poolSpawner.GetObject(position, worldSpace);

        if (unitObj.TryGetComponent(out Unit unit))
        {
            AddUnit(unit);
        }

        return unitObj;
    }

    public void HandleExpression(Vector3 worldPosition, MathExpression expression)
    {
        int newUnitCount = (int)expression.operation.Calculate(numberOfUnits, expression.value);
        int unitsNeeded = Mathf.Abs(newUnitCount - numberOfUnits);

        if (newUnitCount > numberOfUnits)
        {
            for (int i = 0; i < unitsNeeded; i++)
            {
                Vector3 offset = new Vector3(Random.value, 0f, Random.value).Clamp(0.25f);
                var unit = SpawnUnit(worldPosition + offset, true);
            }
        } else if (newUnitCount < numberOfUnits)
        {
            for (int i = 0; i < unitsNeeded; i++)
            {
                var unit = units[units.Count - 1];
                RemoveUnit(unit);
                unit.Dispose();
            }
        }
    }
}

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
        _units?.Clear();
    }

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
        unit.onUnitDeath.AddListener(() =>
        {
            RemoveUnit(unit);
        });
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);

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

    public void Multiply(Vector3 worldPosition, int multiplier)
    {
        int newUnitCount = numberOfUnits * multiplier;
        int unitsNeeded = newUnitCount - numberOfUnits;

        for (int i = 0; i < unitsNeeded; i++)
        {
            Vector3 offset = new Vector3(Random.value, 0f, Random.value).Clamp(0.25f);
            var unit = SpawnUnit(worldPosition + offset, true);
        }
    }
}

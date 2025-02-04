using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UnitGroup : MonoBehaviour
{
    [SerializeField] private PoolSpawner poolSpawner;
    [SerializeField] private CenterPointSolver centerPointSolver;
    [SerializeField] private TextMeshProUGUI unitPowerText;

    public UnityEvent onGroupEmpty;
    public UnityEvent<int> onUnitCountChanged;

    private List<Unit> _units = new List<Unit>();
    private List<Unit> _activeUnits = new List<Unit>();
    private List<Unit> _deadUnits = new List<Unit>();

    public List<Unit> units => _units;
    public List<Unit> activeUnits => _activeUnits;
    private List<Unit> deadUnits => _deadUnits;

    public int numberOfUnits => activeUnits.Count;

    private const int MAX_UNITS = 300;

    public void StartEncounter(UnitGroup enemyGroup)
    {
        activeUnits.ForEach(unit => unit.StartEncounter(enemyGroup.activeUnits));
    }

    public void EndEncounter()
    {
        activeUnits.ForEach(unit => unit.EndEncounter());
    }

    public void Dispose()
    {
        units.ForEach(unit => unit.Dispose());

        _units = new List<Unit>();
        _activeUnits = new List<Unit>();
        _deadUnits = new List<Unit>();
        onUnitCountChanged.Invoke(numberOfUnits);
    }

    public void RemoveUnit(Unit unit)
    {
        activeUnits.Remove(unit);
        deadUnits.Add(unit);

        onUnitCountChanged.Invoke(numberOfUnits);
        if (activeUnits.Count == 0)
        {
            onGroupEmpty.Invoke();
        }
    }

    public void UnparentUnit(Unit unit)
    {
        units.Remove(unit);
        activeUnits.Remove(unit);
        deadUnits.Remove(unit);
        onUnitCountChanged.Invoke(numberOfUnits);
    }

    public void AddUnit(Unit unit)
    {
        unit.parentGroup = this;
        units.Add(unit);
        activeUnits.Add(unit);
        unit.onDeath.AddListener(() =>
        {
            RemoveUnit(unit);
        });
        onUnitCountChanged.Invoke(numberOfUnits);
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

    public int UnitsAfterExpression(MathExpression expression)
    {
        float newUnitCount = Mathf.Clamp(expression.Calculate(numberOfUnits), 0, MAX_UNITS);
        return (int)newUnitCount;
    }

    public void HandleExpression(Vector3 localPosition, MathExpression expression)
    {
        int newUnitCount = UnitsAfterExpression(expression);
        int unitsNeeded = Mathf.Abs(newUnitCount - numberOfUnits);

        if (newUnitCount > numberOfUnits)
        {
            for (int i = 0; i < unitsNeeded; i++)
            {
                Vector3 offset = new Vector3(Random.value, 0f, Random.value).Clamp(0.25f);
                var unit = SpawnUnit(localPosition + offset, false);
            }
        } else if (newUnitCount < numberOfUnits)
        {
            for (int i = 0; i < unitsNeeded; i++)
            {
                var unit = activeUnits[numberOfUnits - 1];
                unit.Death();
            }
        }
    }

    public void CalculateCenterPoint()
    {
        List<Transform> activeUnitTransforms = new List<Transform>();
        activeUnits.ForEach(unit => activeUnitTransforms.Add(unit.transform));

        centerPointSolver.CalculateCenterPoint(activeUnitTransforms);
    }

    public void UpdateUnitPowerText(int newPower)
    {
        unitPowerText.gameObject.SetActive(newPower > 0);
        unitPowerText.text = $"{newPower}";
    }
}

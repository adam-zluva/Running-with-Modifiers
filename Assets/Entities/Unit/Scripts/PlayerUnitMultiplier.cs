using UnityEngine;

public class PlayerUnitMultiplier : MonoBehaviour
{
    [SerializeField] private PoolSpawner spawner;

    private int _units;
    public int units
    {
        get => _units;
        set => _units = value;
    }

    void SpawnUnit()
    {
        GameObject unitObject = spawner.GetObject(Vector3.zero);

        if (unitObject.TryGetComponent(out Unit unit))
        {
            unit.onUnitDeath.AddListener(PopUnit);
        }
    }

    public void LevelStart(LevelSet set)
    {
        units = set.startingPlayerUnits;
        for (int i = 0; i < units; i++)
        {
            SpawnUnit();
        }
    }

    // multiplier is float, because the EventChannel (and thus
    // EventChannelListener) used to call this method is of type float
    public void MultiplyUnits(float multiplier)
    {
        int newUnitCount = units * (int)multiplier;
        int unitsNeeded = newUnitCount - units;
        units = newUnitCount;

        for (int i = 0; i < unitsNeeded; i++)
        {
            SpawnUnit();
        }
    }

    public void PopUnit()
    {
        units--;
    }
}

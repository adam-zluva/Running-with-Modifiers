using UnityEngine;
using UnityEngine.Pool;

public class UnitMultiplier : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Transform parent;
    private ObjectPool<GameObject> unitsPool;

    private int units;

    private void Awake()
    {
        unitsPool = new ObjectPool<GameObject>(
            () =>
            {
                var unit = Instantiate(unitPrefab);
                if (parent)
                {
                    unit.transform.SetParent(parent);
                }
                unit.GetComponent<PoolGameObject>().Init(unitsPool);
                return unit;
            },
            unit => { unit.SetActive(true); },
            unit => { unit.SetActive(false); },
            unit => { Destroy(unit); },
            false, 50 );
    }

    public void MutliplyUnits(float multiplier)
    {
        var count = (int)multiplier;

        for (int i = 0; i < count; i++)
        {
            unitsPool.Get();
        }
    }
}

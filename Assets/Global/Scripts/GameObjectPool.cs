using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(menuName = "Object Pool")]
public class GameObjectPool : ScriptableObject
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private int startingCapacity = 300;
    public ObjectPool<GameObject> pool { get; private set; }

    public void Init()
    {
        pool = new ObjectPool<GameObject>(
            () =>
            {
                var instance = Instantiate(gameObject);
                return instance;
            },
            unit => { unit.SetActive(true); },
            unit => { unit.SetActive(false); },
            unit => { Destroy(unit); },
            true, startingCapacity);

        var poolParent = new GameObject($"--- {gameObject.name} Pool ---").transform;
        GameObject[] pooledObjects = new GameObject[startingCapacity];
        for (int i = 0; i < startingCapacity; i++)
        {
            var obj = pool.Get();
            obj.transform.SetParent(poolParent);
            pooledObjects[i] = obj;
        }

        for (int i = 0; i < startingCapacity; i++)
        {
            pool.Release(pooledObjects[i]);
        }
    }

    public void ReturnObject(GameObject obj)
    {
        pool.Release(obj);
    }

    public void Dispose()
    {
        pool?.Dispose();
    }
}

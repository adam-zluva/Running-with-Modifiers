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
            false, startingCapacity);
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

using UnityEngine.Pool;
using UnityEngine;

public class PoolGameObject : MonoBehaviour
{
    private ObjectPool<GameObject> objectPool;

    public void Init(ObjectPool<GameObject> objectPool)
    {
        this.objectPool = objectPool;
    }

    public void ReturnObject()
    {
        objectPool.Release(gameObject);
    }
}

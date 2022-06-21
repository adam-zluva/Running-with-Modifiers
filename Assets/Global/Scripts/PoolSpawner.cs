using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    [SerializeField] private GameObjectPool poolAsset;
    [SerializeField] private Transform parent;

    public GameObject GetObject(Vector3 localPosition)
    {
        GameObject obj = poolAsset.pool.Get();
        if (parent) obj.transform.SetParent(parent);
        obj.transform.localPosition = localPosition;

        return obj;
    }
}

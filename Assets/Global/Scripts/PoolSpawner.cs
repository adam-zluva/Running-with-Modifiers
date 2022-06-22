using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    [SerializeField] private GameObjectPool poolAsset;
    [SerializeField] private Transform parent;

    public void GetObject()
    {
        GetObject(Vector3.zero);
    }

    public GameObject GetObject(Vector3 position, bool worldSpace = false)
    {
        GameObject obj = poolAsset.pool.Get();
        if (parent) obj.transform.SetParent(parent);
        if (worldSpace) obj.transform.position = position;
        else obj.transform.localPosition = position;

        return obj;
    }
}

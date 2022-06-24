using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour, ILevelBuilder
{
    [SerializeField] private PoolSpawner poolSpawner;
    [SerializeField] private float platformSpacing;
    [SerializeField] private UnitGroup playerUnitGroup;

    public void BuildLevel(LevelSet level)
    {
        Vector3 localPosition = Vector3.zero;
        foreach (var section in level.levelSections)
        {
            GameObject platformObj = SpawnPlatform(localPosition);
            //Debug.Log($"{platformObj.gameObject.name} - {platformObj.activeInHierarchy}", platformObj.gameObject);
            if (platformObj.TryGetComponent(out Platform platform))
            {
                platform.SetSection(section);
            }

            localPosition += Vector3.forward * platformSpacing;
        }

        playerUnitGroup.HandleExpression(Vector3.zero,
            new MathExpression(MathExpression.Operation.Addition, level.startingPlayerUnits));
    }

    public GameObject SpawnPlatform(Vector3 localPosition)
    {
        return poolSpawner.GetObject(localPosition);
    }
}

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
            if (platformObj.TryGetComponent(out Platform platform))
            {
                platform.SetSection(section);
            }

            localPosition += Vector3.forward * platformSpacing;
        }

        for (int i = 0; i < level.startingPlayerUnits; i++)
        {
            playerUnitGroup.SpawnUnit(Vector3.zero);
        }
    }

    public GameObject SpawnPlatform(Vector3 localPosition)
    {
        return poolSpawner.GetObject(localPosition);
    }
}

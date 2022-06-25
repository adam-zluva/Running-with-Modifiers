using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour, ILevelBuilder
{
    [SerializeField] private PoolSpawner poolSpawner;
    [SerializeField] private float platformSpacing;
    [SerializeField] private UnitGroup playerUnitGroup;
    [Space]
    [SerializeField] private int endlessStartingUnitCount = 1;
    [SerializeField] private float enemyCountBias = 0.5f;
    [SerializeField] private MathExpression endlessFallbackExpression;
    [SerializeField] private OperationRange[] endlessOperationRanges;

    private LevelManager levelManager;

    public void BuildLevel(LevelSet level)
    {
        Vector3 localPosition = Vector3.zero;
        var sections = level.levelSections;
        foreach (var section in sections)
        {
            GameObject platformObj = SpawnPlatform(localPosition);
            if (platformObj.TryGetComponent(out Platform platform))
            {
                platform.SetSection(section);

                if (section == sections[sections.Length -1])
                {
                    platform.onEncounterEnded.AddListener(levelManager.LevelWon);
                }
            }

            localPosition += Vector3.forward * platformSpacing;
        }

        playerUnitGroup.HandleExpression(Vector3.zero,
            new MathExpression(MathExpression.Operation.Addition, level.startingUnitCount));
    }

    public void BuildLevelProcedural()
    {
        playerUnitGroup.HandleExpression(Vector3.zero,
            new MathExpression(MathExpression.Operation.Addition, endlessStartingUnitCount));

        SpawnProceduralPlatform();
    }

    private void SpawnProceduralPlatform()
    {
        GameObject platformObj = SpawnPlatform(Vector3.zero);
        if (platformObj.TryGetComponent(out Platform platform))
        {
            var operationRangeA = endlessOperationRanges.GetRandom();
            MathExpression expressionA = new MathExpression(operationRangeA.operation, operationRangeA.GetValidValue());
            int unitsAfterA = playerUnitGroup.UnitsAfterExpression(expressionA);

            var operationRangeB = endlessOperationRanges.GetRandom(operationRangeA);
            MathExpression expressionB = new MathExpression(operationRangeB.operation, operationRangeB.GetValidValue());
            int unitsAfterB = playerUnitGroup.UnitsAfterExpression(expressionB);

            if (unitsAfterA <= 1 && unitsAfterB <= 1)
            {
                expressionA = endlessFallbackExpression;
                unitsAfterA = playerUnitGroup.UnitsAfterExpression(expressionA);
            }

            int unitCountDifference = Mathf.Abs(unitsAfterA - unitsAfterB);
            int enemyCount = Mathf.Max(unitsAfterA, unitsAfterB) - Random.Range(1, unitCountDifference);

            LevelSection section = new LevelSection(expressionA, expressionB, enemyCount);
            platform.SetSection(section);
            platform.onEncounterEnded.AddListener(SpawnProceduralPlatform);
        }
    }

    public ILevelBuilder Init(LevelManager levelManager)
    {
        this.levelManager = levelManager;
        return this;
    }

    public GameObject SpawnPlatform(Vector3 localPosition)
    {
        return poolSpawner.GetObject(localPosition);
    }

    [System.Serializable]
    private class OperationRange
    {
        [SerializeField] private MathExpression.Operation _operation;
        public MathExpression.Operation operation => _operation;

        [SerializeField] private Vector2[] validRanges;

        public float GetValidValue()
        {
            var range = validRanges.GetRandom();
            return Mathf.Round(Random.Range(range.x, range.y));
        }
    }
}

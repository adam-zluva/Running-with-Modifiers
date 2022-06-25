using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Level Set")]
public class LevelSet : ScriptableObject
{
    [SerializeField] private int _startingUnitCount = 1;
    public int startingUnitCount { get => _startingUnitCount; private set => _startingUnitCount = value; }

    [SerializeField] private LevelSection[] _levelSections;
    public LevelSection[] levelSections => _levelSections;

#if UNITY_EDITOR
    private void OnValidate()
    {
        int unitCount = startingUnitCount;
        foreach (var section in levelSections)
        {
            int unitsAfterA = (int)Mathf.Max(section.expressionA.Calculate(unitCount), 0);
            int unitsAfterB = (int)Mathf.Max(section.expressionB.Calculate(unitCount), 0);

            unitCount = Mathf.Max(unitsAfterA, unitsAfterB) - section.enemies;
            section.unitsBeforeEncounter = unitCount + section.enemies;
            section.unitsAfterSection = unitCount;
        }
    }
#endif
}

[System.Serializable]
public class LevelSection
{
    [SerializeField] private MathExpression _expressionA;
    public MathExpression expressionA => _expressionA;

    [SerializeField] private MathExpression _expressionB;
    public MathExpression expressionB => _expressionB;

    [SerializeField] private int _enemies;
    public int enemies => _enemies;

#if UNITY_EDITOR
    [ReadOnly] public int unitsBeforeEncounter;
    [ReadOnly] public int unitsAfterSection;
#endif

    public LevelSection(MathExpression expressionA, MathExpression expressionB, int enemies)
    {
        _expressionA = expressionA;
        _expressionB = expressionB;
        _enemies = enemies;
    }
}

[System.Serializable, InlineProperty]
public class MathExpression
{
    [HorizontalGroup, SerializeField, HideLabel]
    private Operation _operation;
    public Operation operation => _operation;

    [HorizontalGroup, SerializeField, HideLabel]
    private float _value;
    public float value => _value;

    public MathExpression(Operation operation, float value)
    {
        _operation = operation;
        _value = value;
    }

    public static MathExpression GetRandomExpression(int minValue, int maxValue)
    {
        float value = Random.Range(minValue, maxValue);
        return new MathExpression(GetRandomOperation(), value);
    }

    public static Operation GetRandomOperation()
    {
        return (Operation)Random.Range(0, 4);
    }

    public float Calculate(float leftSide)
    {
        return operation.Calculate(leftSide, value);
    }

    public enum Operation
    {
        Addition = 0, Subtraction = 1, Multiplication = 2, Division = 3
    }
}
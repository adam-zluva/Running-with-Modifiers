using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Level Set")]
public class LevelSet : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private int _startingPlayerUnits = 1;
    public int startingPlayerUnits { get => _startingPlayerUnits; private set => _startingPlayerUnits = value; }

    [SerializeField] private LevelSection[] _levelSections;
    public LevelSection[] levelSections => _levelSections;

    public void OnBeforeSerialize()
    {
        levelSections[levelSections.Length - 1].lastSection = true;
    }

    public void OnAfterDeserialize() { }
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

    [HideInInspector] public bool lastSection;
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

    public enum Operation
    {
        Addition, Subtraction, Multiplication, Division
    }
}
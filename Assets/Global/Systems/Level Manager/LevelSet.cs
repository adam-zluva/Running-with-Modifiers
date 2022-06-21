using UnityEngine;

[CreateAssetMenu(menuName = "Level Set")]
public class LevelSet : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private float _levelSpeed;
    public float levelSpeed => _levelSpeed;

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
    [SerializeField] private int _multiplierA;
    public int multiplierA => _multiplierA;

    [SerializeField] private int _multiplierB;
    public int multiplierB => _multiplierB;

    [SerializeField] private int _enemies;
    public int enemies => _enemies;

    [HideInInspector] public bool lastSection;
}
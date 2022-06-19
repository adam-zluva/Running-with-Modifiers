using UnityEngine;

[CreateAssetMenu(menuName = "Level Set")]
public class LevelSet : ScriptableObject
{
    [SerializeField] private float _levelSpeed;
    public float levelSpeed => _levelSpeed;

    [SerializeField] private int startingPlayerUnits = 1;

    [SerializeField] private LevelSection[] _levelSections;
    public LevelSection[] levelSections => _levelSections;
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
}
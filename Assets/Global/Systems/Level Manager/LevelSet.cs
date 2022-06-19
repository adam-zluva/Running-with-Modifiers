using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Set")]
public class LevelSet : ScriptableObject
{
    [SerializeField] private float _levelSpeed;
    public float levelSpeed => _levelSpeed;

    [SerializeField] private int startingPlayerUnits = 1;

    [SerializeField] private LevelSection[] levelSections;
}

[System.Serializable]
public class LevelSection
{
    [SerializeField] private int[] _multipliers;
    public int[] multipliers => _multipliers;

    [SerializeField] private int _enemies;
    public int enemies => _enemies;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Scheme")]
public class LevelScheme : ScriptableObject
{
    [SerializeField] private Color _skyColor;
    public Color skyColor => _skyColor;

    [SerializeField] private Color _groundColor;
    public Color groundColor => _groundColor;

    [SerializeField] private Color _gateColor;
    public Color gateColor => _gateColor;

    [SerializeField] private Color _playerColor;
    public Color playerColor => _playerColor;

    [SerializeField] private Color _enemyColor;
    public Color enemyColor => _enemyColor;
}

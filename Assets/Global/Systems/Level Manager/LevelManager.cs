using UnityEngine;
using EventChannels;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannel gameStartChannel;
    [SerializeField] private FloatEventChannel platformsSpeedChannel;
    private LevelSet currentLevel;

    public void PlayLevel(LevelSet level)
    {
        currentLevel = level;
        gameStartChannel.RaiseEvent();
        platformsSpeedChannel.RaiseEvent(currentLevel.levelSpeed);
    }
}

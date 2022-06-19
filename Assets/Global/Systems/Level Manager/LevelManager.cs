using UnityEngine;
using EventChannels;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ServiceProvider serviceProvider;
    [Space]
    [SerializeField] private VoidEventChannel gameStartChannel;
    [SerializeField] private FloatEventChannel platformsSpeedChannel;

    private LevelSet currentLevel;
    private int section;

    private void Start()
    {
        serviceProvider.AddService(this);
    }

    public void PlayLevel(LevelSet level)
    {
        currentLevel = level;

        section = 0;

        gameStartChannel.RaiseEvent();
        platformsSpeedChannel.RaiseEvent(currentLevel.levelSpeed);
    }

    public LevelSection GetLevelSection()
    {
        if (section < currentLevel.levelSections.Length)
        {
            var index = section;
            section++;
            return currentLevel.levelSections[index];
        }

        return null;
    }
}

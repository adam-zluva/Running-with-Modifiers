using UnityEngine;
using UnityEngine.Events;
using EventChannels;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ServiceProvider serviceProvider;
    [Space]
    [SerializeField] private VoidEventChannel gameStartChannel;
    [SerializeField] private FloatEventChannel platformsSpeedChannel;

    public UnityEvent onLevelFinished;

    private LevelSet currentLevel;
    private int _section;
    private int section
    {
        get => _section;
        set
        {
            _section = value;
            if (_section >= currentLevel.levelSections.Length)
            {
                onLevelFinished.Invoke();
            }
        }
    }

    private void OnEnable()
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

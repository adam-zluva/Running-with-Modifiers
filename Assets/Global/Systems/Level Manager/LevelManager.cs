using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public UnityEvent<LevelSet> onLevelSelected;
    public UnityEvent onLevelBuilt;
    [Space]
    private ILevelBuilder levelBuilder;

    private void Awake()
    {
        levelBuilder = GetComponent<ILevelBuilder>();
    }

    public void SelectLevel(LevelSet level)
    {
        onLevelSelected.Invoke(level);
    }

    public void BuildLevel(LevelSet level)
    {
        levelBuilder.BuildLevel(level);
        onLevelBuilt.Invoke();
    }
}

public interface ILevelBuilder
{
    public void BuildLevel(LevelSet level);
}

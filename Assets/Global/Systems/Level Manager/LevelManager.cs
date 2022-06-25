using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public UnityEvent<LevelSet> onLevelSelected;
    public UnityEvent onLevelBuilt;
    public UnityEvent onLevelQuit;
    public UnityEvent onLevelWon;
    public UnityEvent onLevelLost;

    private ILevelBuilder levelBuilder;

    private void Awake()
    {
        levelBuilder = GetComponent<ILevelBuilder>().Init(this);
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

    public void BuildLevelProcedural()
    {
        levelBuilder.BuildLevelProcedural();
        onLevelBuilt.Invoke();
    }

    public void QuitLevel()
    {
        onLevelQuit.Invoke();
    }

    public void LevelLost()
    {
        onLevelLost.Invoke();
    }

    public void LevelWon()
    {
        onLevelWon.Invoke();
    }
}

public interface ILevelBuilder
{
    public ILevelBuilder Init(LevelManager levelManager);
    public void BuildLevel(LevelSet level);
    public void BuildLevelProcedural();
}

using UnityEngine;
using UnityEngine.Events;
using EventChannels;
using TMPro;

public class Platform : MonoBehaviour
{
    [SerializeField] private Gate gateA;
    [SerializeField] private Gate gateB;
    [Space]
    [SerializeField] private FloatEventChannel multiplyPlayerChannel;
    [SerializeField] private ServiceProvider serviceProvider;
    [Space]
    [SerializeField] private UnityEvent onLevelFinished;

    private LevelManager levelManager;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.localPosition;

        levelManager = serviceProvider.GetService<LevelManager>(typeof(LevelManager));
        levelManager.onLevelFinished.AddListener(() => onLevelFinished.Invoke());
    }

    public void ResetToStart()
    {
        transform.localPosition = startPosition;
    }

    public void UpdatePlatform()
    {
        var section = levelManager.GetLevelSection();
        if (section != null) SetSection(section);
    }

    private void SetSection(LevelSection section)
    {
        gateA.SetMultiplier(section.multiplierA, () => MultiplyPlayer(section.multiplierA));
        gateB.SetMultiplier(section.multiplierB, () => MultiplyPlayer(section.multiplierB));
    }

    private void MultiplyPlayer(int multiplier)
    {
        multiplyPlayerChannel.RaiseEvent(multiplier);
    }

    [System.Serializable]
    public class Gate
    {
        [SerializeField] private Trigger trigger;
        [SerializeField] private TextMeshProUGUI multiplierText;

        public void SetMultiplier(int multiplier, UnityAction multAction)
        {
            trigger.onTriggerEnter.AddListener(multAction);
            trigger.onTriggerEnter.AddListener(() =>
            {
                trigger.onTriggerEnter.RemoveAllListeners();
            });

            multiplierText.text = $"x{multiplier}";
        }
    }
}
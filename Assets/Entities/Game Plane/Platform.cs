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

    public void UpdatePlatform()
    {
        var levelManager = serviceProvider.GetService<LevelManager>(typeof(LevelManager));
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
            trigger.onTriggerEnter.RemoveAllListeners();
            trigger.onTriggerEnter.AddListener(multAction);

            multiplierText.text = $"x{multiplier}";
        }
    }
}
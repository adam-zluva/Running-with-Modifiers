using UnityEngine;
using UnityEngine.Events;
using EventChannels;
using TMPro;

public class Platform : MonoBehaviour
{
    [SerializeField] private Gate gateA;
    [SerializeField] private Gate gateB;
    [Space]
    [SerializeField] private PoolSpawner enemySpawner;
    [SerializeField] private FloatEventChannel multiplyPlayerChannel;
    [SerializeField] private ServiceProvider serviceProvider;
    [Space]
    [SerializeField] private UnityEvent onSectionCleared;

    private LevelManager levelManager;
    private Vector3 startPosition;
    private int _enemiesLeft;
    public int enemiesLeft
    {
        get => _enemiesLeft;
        set
        {
            _enemiesLeft = value;
            if (_enemiesLeft <= 0) onSectionCleared.Invoke();
        }
    }

    private void Start()
    {
        gateA.SetMultiplier(0, () => { });
        gateB.SetMultiplier(0, () => { });
        startPosition = transform.localPosition;

        levelManager = serviceProvider.GetService<LevelManager>(typeof(LevelManager));
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
        SetEnemies(section.enemies);
        gateA.SetMultiplier(section.multiplierA, () => GatePassed(section.multiplierA));
        gateB.SetMultiplier(section.multiplierB, () => GatePassed(section.multiplierB));
    }

    private void SetEnemies(int count)
    {
        enemiesLeft = count;

        for (int i = 0; i < count; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle;
            Vector3 localPosition = new Vector3(randomCircle.x, 0f, randomCircle.y);
            GameObject enemyUnit = enemySpawner.GetObject(localPosition);

            if (enemyUnit.TryGetComponent(out Unit unit))
            {
                unit.onUnitDeath.AddListener(PopEnemy);
            }
        }
    }

    private void GatePassed(int multiplier)
    {
        multiplyPlayerChannel.RaiseEvent(multiplier);

        gateA.gameObject.SetActive(false);
        gateB.gameObject.SetActive(false);
    }

    public void PopEnemy()
    {
        enemiesLeft--;
    }

    [System.Serializable]
    public class Gate
    {
        [field: SerializeField] public GameObject gameObject { get; private set; }
        [SerializeField] private UnityEventBuffer gateTrigger;
        [SerializeField] private TextMeshProUGUI multiplierText;

        public void SetMultiplier(int multiplier, UnityAction multAction)
        {
            gameObject.gameObject.SetActive(multiplier > 0);

            gateTrigger.eventBuffer.AddListener(multAction);

            multiplierText.text = $"x{multiplier}";
        }
    }
}
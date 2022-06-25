using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private LevelSet[] levels;
    [SerializeField] private LevelManager levelManager;
    [Space]
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Transform container;

    public void PlaceLevelButtons()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            var levelSet = levels[i];
            var levelButtonObj = Instantiate(levelButtonPrefab, container);

            if (levelButtonObj.TryGetComponent(out Button levelButton))
            {
                levelButton.onClick.AddListener(() =>
                {
                    levelManager.SelectLevel(levelSet);
                });
            }

            var levelButtonText = levelButtonObj.GetComponentInChildren<TextMeshProUGUI>();
            if (levelButtonText)
            {
                levelButtonText.text = $"{i + 1}";
            }
        }
    }
}

using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsDebugText;

    public void DisplayFPS()
    {
        int fps = (int)(1f / Time.unscaledDeltaTime);
        fpsDebugText.text = fps.ToString();
    }

    public void SetFrameRate(int frameRate)
    {
        Application.targetFrameRate = frameRate;
        QualitySettings.vSyncCount = 0;
    }
}

using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private GameObject gfx;
    private Collider triggerCollider;

    [SerializeField] private UnityEvent<Vector3, int> onGatePassed;

    private int multiplier;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }

    public void SetGate(int multiplier)
    {
        this.multiplier = multiplier;
        multiplierText.text = $"x{multiplier}";

        Show();
    }

    public void Show()
    {
        gfx.SetActive(true);
        triggerCollider.enabled = true;
    }

    public void Hide()
    {
        gfx.SetActive(false);
        triggerCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        onGatePassed.Invoke(other.transform.position, multiplier);
    }
}

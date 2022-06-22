using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private GameObject gfx;
    private Collider triggerCollider;

    [SerializeField] private UnityEvent<Vector3, MathExpression> onGatePassed;

    private MathExpression mathExpression;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }

    public void SetGate(MathExpression expression)
    {
        this.mathExpression = expression;
        multiplierText.text = $"{expression.operation.OperatorSymbol()}{expression.value}";

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
        onGatePassed.Invoke(other.transform.position, mathExpression);
    }
}

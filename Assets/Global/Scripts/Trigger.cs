using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour
{
    [HorizontalGroup("CompareTag")]
    [SerializeField] private bool compareTag;
    [SerializeField, HorizontalGroup("CompareTag"), HideLabel] private string targetTag;
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke();
    }
}

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
        if (compareTag)
        {
            if (other.CompareTag(targetTag)) onTriggerEnter.Invoke();
        } else
        {
            onTriggerEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (compareTag)
        {
            if (other.CompareTag(targetTag)) onTriggerExit.Invoke();
        }
        else
        {
            onTriggerExit.Invoke();
        }
    }
}

using UnityEngine.Events;
using UnityEngine;

public class UnityEventBuffer : MonoBehaviour
{
    [SerializeField] private bool clearOnInvoke;
    [SerializeField] private UnityEvent eventBuffer;

    public void AddListener(UnityAction action)
    {
        eventBuffer.AddListener(action);
    }

    public void Invoke()
    {
        eventBuffer.Invoke();
        if (clearOnInvoke) eventBuffer.RemoveAllListeners();
    }
}

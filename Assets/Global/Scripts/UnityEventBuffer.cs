using UnityEngine.Events;
using UnityEngine;

public class UnityEventBuffer : MonoBehaviour
{
    [SerializeField] private bool clearOnInvoke;
    public UnityEvent eventBuffer;

    private void Awake()
    {
        if (clearOnInvoke)
        {
            eventBuffer.AddListener(() =>
            {
                eventBuffer.RemoveAllListeners();
            });
        }
    }

    public void Invoke()
    {
        eventBuffer.Invoke();
        if (clearOnInvoke) eventBuffer.RemoveAllListeners();
    }
}

using UnityEngine.Events;
using UnityEngine;

public class BehaviourEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent onAwake;
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onUpdate;

    private void Awake()
    {
        onAwake.Invoke();
    }

    private void Start()
    {
        onStart.Invoke();
    }

    private void Update()
    {
        onUpdate.Invoke();
    }
}

using UnityEngine.Events;
using UnityEngine;

public class BehaviourEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent onAwake;
    [SerializeField] private UnityEvent onEnable;
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onUpdate;
    [SerializeField] private UnityEvent onDisable;
    [SerializeField] private UnityEvent onDestroy;

    private void Awake()
    {
        onAwake.Invoke();
    }

    private void OnEnable()
    {
        onEnable.Invoke();
    }

    private void Start()
    {
        onStart.Invoke();
    }

    private void Update()
    {
        onUpdate.Invoke();
    }

    private void OnDisable()
    {
        onDisable.Invoke();
    }

    private void OnDestroy()
    {
        onDestroy.Invoke();
    }
}

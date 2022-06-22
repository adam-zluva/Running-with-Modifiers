using UnityEngine;
using UnityEngine.Events;

public class View : MonoBehaviour
{
    [SerializeField] private GameObject _viewObject;
    public GameObject viewObject => _viewObject;

    [SerializeField] private UnityEvent onViewOpen;
    [SerializeField] private UnityEvent onViewClose;

    public void Open()
    {
        onViewOpen.Invoke();
    }

    public void Close()
    {
        onViewClose.Invoke();
    }
}

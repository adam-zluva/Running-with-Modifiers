using System;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Event Channels/Void")]
public class VoidEventChannel : ScriptableObject
{
    public Action onEventRaised;

    [Button("Raise Event", ButtonSizes.Medium)]
    public void RaiseEvent()
    {
        onEventRaised?.Invoke();
    }
}

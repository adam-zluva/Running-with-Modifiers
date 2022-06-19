using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace EventChannels
{
    public abstract class EventChannel<T> : ScriptableObject
    {
        public Action<T> onEventRaised;

        [Button("Raise Event", ButtonSizes.Medium)]
        public void RaiseEvent(T obj)
        {
            onEventRaised?.Invoke(obj);
        }
    }
}
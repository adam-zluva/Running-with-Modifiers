using UnityEngine.Events;
using UnityEngine;

namespace EventChannels
{
    public abstract class EventChannelListener<T, U> : MonoBehaviour where T : EventChannel<U>
    {
        [SerializeField] private T eventChannel;
        [SerializeField] private UnityEvent<U> onEventRaised;

        void RaiseEvent(U obj)
        {
            onEventRaised.Invoke(obj);
        }

        private void OnEnable()
        {
            eventChannel.onEventRaised += RaiseEvent;
        }

        private void OnDisable()
        {
            eventChannel.onEventRaised -= RaiseEvent;
        }
    }
}
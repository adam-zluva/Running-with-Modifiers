using UnityEngine.Events;
using UnityEngine;

namespace EventChannels
{
    public class VoidEventChannelListener : MonoBehaviour
    {
        [SerializeField] private VoidEventChannel eventChannel;
        [SerializeField] private UnityEvent onEventRaised;

        void RaiseEvent()
        {
            onEventRaised.Invoke();
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
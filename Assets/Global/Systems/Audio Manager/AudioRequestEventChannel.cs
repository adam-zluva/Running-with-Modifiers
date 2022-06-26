using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventChannels
{
    [CreateAssetMenu(menuName = "Event Channels/Audio")]
    public class AudioRequestEventChannel : EventChannel<Sound> { }
}
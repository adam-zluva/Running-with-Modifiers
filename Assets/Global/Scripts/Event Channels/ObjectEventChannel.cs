using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventChannels
{
    [CreateAssetMenu(menuName = "Event Channels/ Object")]
    ///<summary>
    /// Avoid using if possible
    ///</summary>
    public class ObjectEventChannel : EventChannel<object> { }
}
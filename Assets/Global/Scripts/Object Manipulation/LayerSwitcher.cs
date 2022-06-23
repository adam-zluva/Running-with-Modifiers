using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    public void MoveToLayer(string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }
}

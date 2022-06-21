using UnityEngine;
using Sirenix.OdinInspector;

[InlineProperty, System.Serializable]
public class TransformReference
{
    [SerializeField, HorizontalGroup(50), HideLabel] private ReferenceType referenceType;

    [SerializeField, ShowIf("referenceType", ReferenceType.Assign), HorizontalGroup, HideLabel]
    private Transform assignedTransform;

    [SerializeField, ShowIf("referenceType", ReferenceType.FindByTag), HorizontalGroup, HideLabel]
    private string searchTag;

    private Transform _value;
    public Transform value
    {
        get
        {
            if (_value) return _value;

            BuildReferenceValue();
            return _value;
        }

        set => _value = value;
    }

    public void BuildReferenceValue()
    {
        switch (referenceType)
        {
            case ReferenceType.Assign:
                _value = assignedTransform;
                break;
            case ReferenceType.FindByTag:
                _value = GameObject.FindGameObjectWithTag(searchTag).transform;
                break;
        }
    }

    public enum ReferenceType
    {
        Assign, FindByTag
    }
}

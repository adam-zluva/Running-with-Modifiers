using UnityEngine;
using Sirenix.OdinInspector;

[InlineProperty, System.Serializable]
public class Reference<T>
{
    [SerializeField, HorizontalGroup(50), HideLabel] private ReferenceType referenceType;

    [SerializeField, ShowIf("referenceType", ReferenceType.Assign), HorizontalGroup, HideLabel]
    private T assignedTransform;

    [SerializeField, ShowIf("referenceType", ReferenceType.FindByTag), HorizontalGroup, HideLabel]
    private string searchTag;

    private T _value;
    public T value
    {
        get
        {
            if (_value == null) BuildReferenceValue();

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
                _value = GameObject.FindGameObjectWithTag(searchTag).GetComponent<T>();
                break;
        }
    }
}

public enum ReferenceType
{
    Assign, FindByTag
}
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class UITweener : MonoBehaviour
{
    [SerializeField] private GameObject objectToAnimate;
    [Space]
    [SerializeField] private AnimationType tweenType;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float delay = 0f;
    [SerializeField] private Ease easeType;
    [SerializeField] private Vector4 from;
    [SerializeField] private Vector4 to;
    [SerializeField] private bool showOnEnable;
    [Space]
    [SerializeField] private UnityEvent onComplete;

    private Tween tweenObject;

    private void Awake()
    {
        DOTween.Init();
    }

    private void OnEnable()
    {
        if (showOnEnable) Show();
    }

    public void Show()
    {
        HandleTween();
    }

    void HandleTween()
    {
        if (objectToAnimate == null) objectToAnimate = gameObject;

        switch (tweenType)
        {
            case AnimationType.ScaleAbsolute:
                ScaleAbsoluteTween();
                break;
            case AnimationType.ScaleRelative:
                ScaleRelativeTween();
                break;
            case AnimationType.FadeAlphaAbsolute:
                FadeAlphaAbsolute();
                break;
            case AnimationType.FadeAlphaRelative:
                FadeAlphaRelative();
                break;
            default:
                break;
        }

        tweenObject.SetDelay(delay);
        tweenObject.SetEase(easeType);
        tweenObject.OnComplete(() =>
        {
            onComplete.Invoke();
        });
    }

    void ScaleAbsoluteTween()
    {
        tweenObject = objectToAnimate.transform.DOScale(to, duration).From(from);
    }

    void ScaleRelativeTween()
    {
        tweenObject = objectToAnimate.transform.DOScale(to, duration);
    }

    void FadeAlphaAbsolute()
    {
        if (objectToAnimate.TryGetComponent(out CanvasGroup cg))
        {
            tweenObject = cg.DOFade(to.x, duration).From(from.x);
        } else
        {
            Debug.LogWarning($"Canvas Group not found on {objectToAnimate}");
        }
    }

    void FadeAlphaRelative()
    {
        if (objectToAnimate.TryGetComponent(out CanvasGroup cg))
        {
            tweenObject = cg.DOFade(to.x, duration);
        }
        else
        {
            Debug.LogWarning($"Canvas Group not found on {objectToAnimate}");
        }
    }

    public enum AnimationType
    {
        ScaleAbsolute, ScaleRelative, FadeAlphaAbsolute, FadeAlphaRelative
    }
}

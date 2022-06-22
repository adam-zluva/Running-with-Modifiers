using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class AsyncEventSequence : MonoBehaviour
{
    [SerializeField] private SequenceAction[] sequence;

    private Coroutine runningSequence;

    public void StartSequence()
    {
        runningSequence = StartCoroutine(DoSequence());
    }

    public void CancelSequence()
    {
        if (runningSequence != null)
        {
            StopCoroutine(runningSequence);
        }
    }

    private IEnumerator DoSequence()
    {
        foreach (var action in sequence)
        {
            yield return new WaitForSeconds(action.delay);
            action.onInvoke.Invoke();
        }
    }

    [System.Serializable]
    public class SequenceAction
    {
        [SerializeField] private float _delay;
        public float delay => _delay;

        public UnityEvent onInvoke;
    }
}

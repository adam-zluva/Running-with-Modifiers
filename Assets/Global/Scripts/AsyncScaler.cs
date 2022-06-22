using System.Collections;
using UnityEngine;

public class AsyncScaler : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 1f;
    [Space]
    [SerializeField] private Vector3 from;
    [SerializeField] private Vector3 to;

    private Coroutine runningRoutine;

    public void Scale()
    {
        target.localScale = from;
        Scale(to);
    }

    public void Scale(Vector3 targetScale)
    {
        if (runningRoutine != null) StopCoroutine(runningRoutine);
        
        runningRoutine = StartCoroutine(ScaleRoutine(targetScale));
    }

    private IEnumerator ScaleRoutine(Vector3 targetScale)
    {
        Vector3 startScale = target.localScale;
        float startTime = Time.time;
        while (target.lossyScale != targetScale)
        {
            target.localScale = Vector3.Lerp(startScale, targetScale, (Time.time - startTime) * speed);
            yield return null;
        }
    }
}

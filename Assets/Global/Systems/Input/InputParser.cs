using UnityEngine;
using UnityEngine.Events;

public class InputParser : MonoBehaviour
{
    [SerializeField] private UnityEvent onInputDown;
    [SerializeField] private UnityEvent<float> onInput;
    [SerializeField] private UnityEvent onInputUp;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                onInputDown.Invoke();
            } else if (touch.phase == TouchPhase.Ended)
            {
                onInputUp.Invoke();
            } else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                onInput.Invoke(touch.position.x);
            }
        }
    }
}

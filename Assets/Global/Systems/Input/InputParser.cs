using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class InputParser : MonoBehaviour
{
    [SerializeField] private string movementActionName = "Movement";
    [SerializeField] private string touchActionName = "Touch";
    [Space]
    [SerializeField] private UnityEvent onInputDown;
    [SerializeField] private UnityEvent<float> onInput;
    [SerializeField] private UnityEvent onInputUp;

    private PlayerInput playerInput;

    private InputAction movementAction;
    private InputAction touchAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        movementAction = playerInput.actions[movementActionName];
        touchAction = playerInput.actions[touchActionName];

        movementAction.performed += ctx =>
        {
            onInput.Invoke(ctx.ReadValue<float>());
        };
        touchAction.started += ctx =>
        {
            onInputDown.Invoke();
        };
        touchAction.canceled += ctx =>
        {
            onInputUp.Invoke();
        };
    }
}

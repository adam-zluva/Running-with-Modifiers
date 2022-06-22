using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class InputParser : MonoBehaviour
{
    [SerializeField] private string movementActionName = "Movement";
    [SerializeField] private UnityEvent<Vector2> onPlayerInput;

    private PlayerInput playerInput;

    private InputAction movementAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        movementAction = playerInput.actions[movementActionName];
        movementAction.performed += ctx =>
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            onPlayerInput.Invoke(input);
        };
    }
}

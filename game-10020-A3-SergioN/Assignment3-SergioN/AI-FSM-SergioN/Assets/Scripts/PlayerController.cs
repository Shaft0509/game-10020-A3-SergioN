using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float gravityForce = -9.81f;

    private CharacterController controller;
    private PlayerInput inputSystem;
    private Vector3 verticalVelocity;

    void Awake()
    {
        // Get required components
        controller = GetComponent<CharacterController>();
        inputSystem = GetComponent<PlayerInput>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Keep player grounded
        if (controller.isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f;
        }

        // Read input movement
        Vector2 inputDir = inputSystem.currentActionMap["Move"].ReadValue<Vector2>();

        // Convert to world movement
        Vector3 moveDirection = new Vector3(inputDir.x, 0f, inputDir.y);

        Vector3 finalMove = moveDirection * speed;

        // Apply gravity
        verticalVelocity.y += gravityForce * Time.deltaTime;
        finalMove.y = verticalVelocity.y;

        // Move player
        controller.Move(finalMove * Time.deltaTime);

        // Rotate player in move direction
        Vector3 flatVelocity = new Vector3(finalMove.x, 0f, finalMove.z);

        if (flatVelocity.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(flatVelocity);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                12f * Time.deltaTime
            );
        }
    }
}
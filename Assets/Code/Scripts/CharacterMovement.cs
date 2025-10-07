using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Transform viewCam;
    CharacterController cc;
    PlayerInput pi;

    [Header("Parameters")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpPower = 2f;
    [SerializeField] float gravity = 7.5f;
    [SerializeField] float maxFallSpeed = 10f;

    [Header("Actions")]
    InputAction moveAction;
    InputAction jumpAction;

    [Header("Calculation")]
    float updownVel = 0;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        pi = GetComponent<PlayerInput>();

        moveAction = pi.actions["Move"];
        jumpAction = pi.actions["Jump"];
    }

    void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 moveDir = (viewCam.forward * moveInput.y + viewCam.right * moveInput.x).normalized;

        if (jumpAction.triggered && cc.isGrounded) updownVel = jumpPower;
        else
        {
            updownVel -= gravity * Time.deltaTime;
            updownVel = Mathf.Clamp(updownVel, -maxFallSpeed, maxFallSpeed);
        }
        moveDir.y = updownVel;

        cc.Move(moveDir * moveSpeed * Time.deltaTime);
    }
}

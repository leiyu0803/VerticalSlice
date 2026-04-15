using UnityEngine;
using UnityEngine.InputSystem;

public class MovementStateManager : MonoBehaviour
{
    public float currentmoveSpeed;
    public float walkSpeed = 3;
    public float walkBackwardSpeed = 2;
    public float runSpeed = 6;
    public float runBackwardSpeed = 4;
    public float crouchSpeed = 1.5f;
    public float crouchBackwardSpeed = 1;
    [SerializeField] float inputSmoothTime = 0.1f;
    [HideInInspector] public float horizontalInput, verticalInput;
    float smoothHorizontalInput, smoothVerticalInput;
    float smoothHorizontalInputTarget, smoothVerticalInputTarget;
    float horizontalVelocity, verticalVelocity;
    [HideInInspector] public Vector3 moveDirection;
    CharacterController characterController;
    [HideInInspector] public PlayerInput playerInput;

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();
    public CrouchState Crouch = new CrouchState();

    [HideInInspector] public Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        currentState.UpdateState(this);

        animator.SetFloat("hzInput", smoothHorizontalInput);
        animator.SetFloat("vInput", smoothVerticalInput);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;

        if(horizontalInput>0) smoothHorizontalInputTarget = 1;
        else if(horizontalInput<0) smoothHorizontalInputTarget = -1;
        else smoothHorizontalInputTarget = 0;

        if(verticalInput>0) smoothVerticalInputTarget = 1;
        else if(verticalInput<0) smoothVerticalInputTarget = -1;
        else smoothVerticalInputTarget = 0;

        smoothHorizontalInput = Mathf.SmoothDamp(smoothHorizontalInput, smoothHorizontalInputTarget, ref horizontalVelocity, inputSmoothTime);
        smoothVerticalInput = Mathf.SmoothDamp(smoothVerticalInput, smoothVerticalInputTarget, ref verticalVelocity, inputSmoothTime);

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        characterController.Move(moveDirection * currentmoveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        return Physics.CheckSphere(spherePos, characterController.radius - 0.05f, groundMask);
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        characterController.Move(velocity * Time.deltaTime);
    }


}

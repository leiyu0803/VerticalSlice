using UnityEngine.InputSystem;

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.animator.SetBool("Crouching", true);
    }
    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (movementStateManager.playerInput.actions["Crouch"].WasPressedThisFrame())
        {
            if (movementStateManager.moveDirection.magnitude > 0.1f)
            {
                ExitState(movementStateManager, movementStateManager.Walk);
            }
            else
            {
                ExitState(movementStateManager, movementStateManager.Idle);
            }
        }
        else if (movementStateManager.playerInput.actions["Sprint"].IsPressed())
        {
            ExitState(movementStateManager, movementStateManager.Run);
        }
        if (movementStateManager.verticalInput < 0) movementStateManager.currentmoveSpeed = movementStateManager.crouchBackwardSpeed;
        else movementStateManager.currentmoveSpeed = movementStateManager.crouchSpeed;
    }
    void ExitState(MovementStateManager movementStateManager, MovementBaseState movementBaseState)
    {
        movementStateManager.animator.SetBool("Crouching", false);
        movementStateManager.SwitchState(movementBaseState);
    }
}

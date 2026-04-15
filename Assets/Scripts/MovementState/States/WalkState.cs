using UnityEngine.InputSystem;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.animator.SetBool("Walking", true);
    }
    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (movementStateManager.playerInput.actions["Sprint"].IsPressed())
        {
            ExitState(movementStateManager, movementStateManager.Run);
        }
        else if (movementStateManager.moveDirection.magnitude < 0.1f)
        {
            ExitState(movementStateManager, movementStateManager.Idle);
        }
        else if (movementStateManager.playerInput.actions["Crouch"].WasPressedThisFrame())
        {
            ExitState(movementStateManager, movementStateManager.Crouch);
        }

        if (movementStateManager.verticalInput < 0) movementStateManager.currentmoveSpeed = movementStateManager.walkBackwardSpeed;
        else movementStateManager.currentmoveSpeed = movementStateManager.walkSpeed;
    }
    void ExitState(MovementStateManager movementStateManager, MovementBaseState movementBaseState)
    {
        movementStateManager.animator.SetBool("Walking", false);
        movementStateManager.SwitchState(movementBaseState);
    }
}

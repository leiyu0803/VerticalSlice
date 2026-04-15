using UnityEngine.InputSystem;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.animator.SetBool("Running", true);
    }
    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (!movementStateManager.playerInput.actions["Sprint"].IsPressed())
        {
            ExitState(movementStateManager, movementStateManager.Walk);
        }
        else if (movementStateManager.moveDirection.magnitude < 0.1f)
        {
            ExitState(movementStateManager, movementStateManager.Idle);
        }

        if (movementStateManager.verticalInput < 0) movementStateManager.currentmoveSpeed = movementStateManager.runBackwardSpeed;
        else movementStateManager.currentmoveSpeed = movementStateManager.runSpeed;
    }
    void ExitState(MovementStateManager movementStateManager, MovementBaseState movementBaseState)
    {
        movementStateManager.animator.SetBool("Running", false);
        movementStateManager.SwitchState(movementBaseState);
    }
}

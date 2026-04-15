using UnityEngine.InputSystem;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
    }
    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if(movementStateManager.moveDirection.magnitude > 0.1f)
        {
            if (movementStateManager.playerInput.actions["Sprint"].IsPressed())
            {
                movementStateManager.SwitchState(movementStateManager.Run);
            }
            else
            {
                movementStateManager.SwitchState(movementStateManager.Walk);
            }
        }
        if (movementStateManager.playerInput.actions["Crouch"].WasPressedThisFrame())
        {
            movementStateManager.SwitchState(movementStateManager.Crouch);
        }
    }
}

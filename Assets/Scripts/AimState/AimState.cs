using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStateManager aimStateManager)
    {
        aimStateManager.currentFOV = aimStateManager.adsFOV;
    }
    public override void UpdateState(AimStateManager aimStateManager)
    {
        if (!aimStateManager.playerInput.actions["ADS"].IsPressed())
        {
            aimStateManager.SwitchState(aimStateManager.hip);
        }
    }
}

using UnityEngine;

public class HipfireStete : AimBaseState
{
    public override void EnterState(AimStateManager aimStateManager)
    {
        aimStateManager.currentFOV = aimStateManager.hipFOV;
    }
    public override void UpdateState(AimStateManager aimStateManager)
    {
        if (aimStateManager.playerInput.actions["ADS"].IsPressed())
        {
                aimStateManager.SwitchState(aimStateManager.aim);
        }
    }
}

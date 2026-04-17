using UnityEngine;

public class DefaultState : ActionBaseState
{
    public override void EnterState(ActionStateManager actionStateManager)
    {

    }

    public override void UpdateState(ActionStateManager actionStateManager)
    {
        actionStateManager.rHandAim.weight = Mathf.Lerp(actionStateManager.rHandAim.weight, 1, 10 * Time.deltaTime);
        actionStateManager.lHandIK.weight = Mathf.Lerp(actionStateManager.lHandIK.weight, 1, 10 * Time.deltaTime);
        if (actionStateManager.playerInput.actions["Reload"].WasPressedThisFrame() && CanReload(actionStateManager))
        {
            actionStateManager.SwitchState(actionStateManager.Reload);
        }
    }

    bool CanReload(ActionStateManager actionStateManager)
    {
        if (actionStateManager.ammo.currentAmmo == actionStateManager.ammo.clipSize + 1)
        {
            return false;
        }
        else if (actionStateManager.ammo.extraAmmo == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

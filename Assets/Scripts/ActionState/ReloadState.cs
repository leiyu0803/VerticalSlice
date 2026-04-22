using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actionStateManager)
    {
        actionStateManager.uIManager.StartBlink();
        if (actionStateManager.ammo.currentAmmo > 0)
        {
            actionStateManager.animator.SetTrigger("ReloadNotEmpty");
            actionStateManager.weaponAnimator.SetTrigger("ReloadNotEmpty");
            actionStateManager.audioSource.PlayOneShot(actionStateManager.ammo.reloadNotEmpty);
        }
        else
        {
            actionStateManager.animator.SetTrigger("ReloadEmpty");
            actionStateManager.weaponAnimator.SetTrigger("ReloadEmpty");
            actionStateManager.audioSource.PlayOneShot(actionStateManager.ammo.reloadEmpty);
        }
    }

    public override void UpdateState(ActionStateManager actionStateManager)
    {
        actionStateManager.rHandAim.weight = Mathf.Lerp(actionStateManager.rHandAim.weight, 0, 10 * Time.deltaTime);
        actionStateManager.lHandIK.weight = Mathf.Lerp(actionStateManager.lHandIK.weight, 0, 10 * Time.deltaTime);
    }
}

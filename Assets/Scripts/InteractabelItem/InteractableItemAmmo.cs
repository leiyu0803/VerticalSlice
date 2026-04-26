using UnityEngine;

public class InteractableItemAmmo : InteractableItem
{
    public int ammoAmount = 20;
    public string ammoType = "AR";
    private WeaponAmmo weaponAmmo;
    public override void Interact()
    {
        weaponAmmo.extraAmmo += ammoAmount;
    }
    public override void Start()
    {
        base.Start();
        weaponAmmo = GameObject.FindGameObjectWithTag(ammoType).GetComponent<WeaponAmmo>();
    }
}

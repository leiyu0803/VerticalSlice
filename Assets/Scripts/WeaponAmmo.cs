using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    [HideInInspector] public int currentAmmo;

    public AudioClip reloadEmpty;
    public AudioClip reloadNotEmpty;
    void Start()
    {
        currentAmmo = clipSize + 1;
    }

    public void Reload()
    {
        int ammoNeeded;
        if (currentAmmo > 0)
        {
            ammoNeeded = clipSize - currentAmmo + 1;
        }
        else 
        {
            ammoNeeded = clipSize;
        }
        if (extraAmmo >= ammoNeeded)
        {
            extraAmmo -= ammoNeeded;
            currentAmmo += ammoNeeded;
        }
        else
        {
            currentAmmo += extraAmmo;
            extraAmmo = 0;
        }
    }
}

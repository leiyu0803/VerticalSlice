using UnityEngine;
using UnityEngine.UI;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    [HideInInspector] public int currentAmmo;

    public bool fullInStart = true;

    public AudioClip reloadEmpty;
    public AudioClip reloadNotEmpty;

    public RectTransform NoAmmoUI;
    public Sprite Crosshair;
    public Sprite NoAmmoCrosshair;
    public Image CrosshairImage;

    private UIManager UIManager;
    void Start()
    {
        if (fullInStart)
        {
            currentAmmo = clipSize;
        }
        else
        {
            currentAmmo = 0;
        }
        UIManager = GetComponentInParent<UIManager>();
    }

    private void Update()
    {
        if (currentAmmo + extraAmmo <= 0)
        {
            NoAmmoUI.localScale = new Vector3(Mathf.Lerp(NoAmmoUI.localScale.x, 0.5f, Time.deltaTime * 20), 0.5f, 0.5f);
        }
        else
        {
            NoAmmoUI.localScale = new Vector3(Mathf.Lerp(NoAmmoUI.localScale.x, 0, Time.deltaTime * 20), 0.5f, 0.5f);
        }
        if (currentAmmo <= 0)
        {
            CrosshairImage.sprite = NoAmmoCrosshair;

        }
        else
        {
            CrosshairImage.sprite = Crosshair;
        }
    }
    public void Reload()
    {
        UIManager.StopBlink();
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

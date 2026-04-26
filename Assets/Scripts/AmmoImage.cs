using UnityEngine;
using UnityEngine.UI;

public class AmmoImage : MonoBehaviour
{
    WeaponAmmo weaponAmmo;
    Image image;
    void Start()
    {
        weaponAmmo = GetComponentInParent<ActionStateManager>().currentWeapon.GetComponent<WeaponAmmo>();
        image = GetComponent<Image>();
    }
    void Update()
    {
        image.fillAmount = Mathf.Lerp(image.fillAmount, (float)weaponAmmo.currentAmmo / weaponAmmo.clipSize, Time.deltaTime * 10);
    }
}

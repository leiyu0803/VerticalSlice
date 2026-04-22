using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text ammoText;
    public TMP_Text slash;
    public TMP_Text extraAmmoText;

    private Coroutine blinkRoutine;

    private void Update()
    {
        WeaponAmmo weaponAmmo = GetComponentInChildren<WeaponAmmo>();
        ammoText.text = weaponAmmo.currentAmmo.ToString();
        extraAmmoText.text = weaponAmmo.extraAmmo.ToString();
    }
    public IEnumerator PunchScale()
    {
        ammoText.transform.localScale = Vector3.one * 1.2f;
        yield return new WaitForSeconds(0.05f);
        ammoText.transform.localScale = Vector3.one;
    }

    public void StartBlink()
    {
        if (blinkRoutine != null)
            StopCoroutine(blinkRoutine);

        blinkRoutine = StartCoroutine(BlinkLoop());
    }
    public void StopBlink()
    {
        if (blinkRoutine != null)
            StopCoroutine(blinkRoutine);

        ammoText.enabled = true;
        slash.enabled = true;
        extraAmmoText.enabled = true;
    }


    IEnumerator BlinkLoop()
    {
        while (true)
        {
            ammoText.enabled = !ammoText.enabled;
            slash.enabled = !slash.enabled;
            extraAmmoText.enabled = !extraAmmoText.enabled;
            yield return new WaitForSeconds(0.1f);
        }
    }
}

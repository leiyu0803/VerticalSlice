using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text ammoText;
    public TMP_Text slash;
    public TMP_Text extraAmmoText;

    private Coroutine blinkRoutine;
    ItemManager itemManager;
    WeaponAmmo weaponAmmo;
    PlayerHealthManager playerHealthManager;

    public TMP_Text heal;
    public TMP_Text battery;
    public TMP_Text water;

    public Image health;
    public Image healthLerp;
    public Image sanity;
    public Image sanityLerp;

    public GameObject FullAuto;
    public GameObject SemiAuto;

    [HideInInspector] public float healthLastUpdateTime;
    [HideInInspector] public float sanityLastUpdateTime;
    private void Start()
    {
        itemManager = GetComponent<ItemManager>();
        weaponAmmo = GetComponentInChildren<WeaponAmmo>();
        playerHealthManager = GetComponent<PlayerHealthManager>();
    }
    private void Update()
    {
        healthLastUpdateTime += Time.deltaTime;
        sanityLastUpdateTime += Time.deltaTime;
        ammoText.text = weaponAmmo.currentAmmo.ToString();
        extraAmmoText.text = weaponAmmo.extraAmmo.ToString();
        heal.text = itemManager.heal.ToString();
        battery.text = itemManager.battery.ToString();
        water.text = itemManager.water.ToString();
        float healthPercent = playerHealthManager.GetHealth() / playerHealthManager.maxHealth;
        float sanityPercent = playerHealthManager.GetSanity() / playerHealthManager.maxSanity;
        health.fillAmount = Mathf.Lerp(health.fillAmount, healthPercent, Time.deltaTime * 20);
        if(healthLastUpdateTime >= 0.5f) healthLerp.fillAmount = Mathf.Lerp(healthLerp.fillAmount, healthPercent, Time.deltaTime * 5);
        sanity.fillAmount = Mathf.Lerp(sanity.fillAmount, sanityPercent, Time.deltaTime * 20);
        if(sanityLastUpdateTime >= 0.5f) sanityLerp.fillAmount = Mathf.Lerp(sanityLerp.fillAmount, sanityPercent, Time.deltaTime * 5);
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

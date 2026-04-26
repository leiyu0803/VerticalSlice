using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public float heal;
    public float battery;
    public float water;

    public float haelthHealAmount = 20;
    public float sanityHealAmount = 20;

    public float HealuseSpeed = 2;
    public float BatterySpeed = 2;
    public float WaterSpeed = 2;

    PlayerInput playerInput;
    PlayerHealthManager playerHealthManager;
    bool isUsingHeal = false;
    bool isUsingWater = false;
    float itemUseTime = 0;

    public Image healLerp;
    public Image batteryLerp;
    public Image waterLerp;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerHealthManager = GetComponent<PlayerHealthManager>();
    }
    void Update()
    {
        if (playerInput.actions["Heal"].WasPressedThisFrame())
        {
            if(!isUsingHeal && !isUsingWater && heal > 0 && playerHealthManager.GetHealth() < playerHealthManager.maxHealth)
            {
                isUsingHeal = true;
            }
        }
        if(playerInput.actions["Heal"].WasReleasedThisFrame())
        {
            isUsingHeal = false;
        }
        if (playerInput.actions["Eat"].WasPressedThisFrame())
        {
            if (!isUsingHeal && !isUsingWater && water > 0 && playerHealthManager.GetSanity() < playerHealthManager.maxSanity)
            {
                isUsingWater = true;
            }
        }
        if (playerInput.actions["Eat"].WasReleasedThisFrame())
        {
            isUsingWater = false;
        }
        if(isUsingHeal|| isUsingWater)
        {
            itemUseTime += Time.deltaTime;
            if (isUsingHeal)
            {
                healLerp.fillAmount = itemUseTime / HealuseSpeed;
                waterLerp.fillAmount = Mathf.Lerp(waterLerp.fillAmount, 0, Time.deltaTime * 10);
                if(itemUseTime >= HealuseSpeed)
                {
                    playerHealthManager.Heal(haelthHealAmount);
                    heal -= 1;
                    isUsingHeal = false;
                    itemUseTime = 0;
                }
            }
            if (isUsingWater)
            {
                waterLerp.fillAmount = itemUseTime / WaterSpeed;
                healLerp.fillAmount = Mathf.Lerp(healLerp.fillAmount, 0, Time.deltaTime * 10);
                if(itemUseTime >= WaterSpeed)
                {
                    playerHealthManager.SanityHeal(sanityHealAmount);
                    water -= 1;
                    isUsingWater = false;
                    itemUseTime = 0;
                }
            }
        }
        else
        {
            itemUseTime = 0;
            healLerp.fillAmount = Mathf.Lerp(healLerp.fillAmount, 0, Time.deltaTime * 10);
            waterLerp.fillAmount = Mathf.Lerp(waterLerp.fillAmount, 0, Time.deltaTime * 10);
        }
    }
}

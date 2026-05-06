using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerHealthManager : MonoBehaviour
{
    private float health;
    public float maxHealth = 100f;
    private float sanity;
    public float maxSanity = 100f;

    private UIManager uiManager;

    public Rig Rig;
    private List<GameObject> HealthDamageSourceList = new List<GameObject>();
    private List<float> HealthDamageValueList = new List<float>();
    private List<GameObject> SanityDamageSourceList = new List<GameObject>();
    private List<float> SanityDamageValueList = new List<float>();

    Animator animator;

    public GameObject HealthUI;
    public GameObject DeathUI;

    public GameObject DamageSourcePrefab;
    public GameObject HealthDamageList;
    public GameObject SanityDamageList;

    private void Start()
    {
        health = maxHealth;
        sanity = maxSanity;
        uiManager = GetComponent<UIManager>();
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage, GameObject source)
    {
        health -= damage;

        uiManager.healthLastUpdateTime = 0;
        if(HealthDamageSourceList.Contains(source))
        {
            int index = HealthDamageSourceList.IndexOf(source);
            HealthDamageValueList[index] += damage;
        }
        else
        {
            HealthDamageSourceList.Add(source);
            HealthDamageValueList.Add(damage);
        }
        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
    }

    public void SanityDamage(float damage, GameObject source)
    {
        sanity -= damage;

        uiManager.sanityLastUpdateTime = 0;
        if (SanityDamageSourceList.Contains(source))
        {
            int index = SanityDamageSourceList.IndexOf(source);
            SanityDamageValueList[index] += damage;
        }
        else
        {
            SanityDamageSourceList.Add(source);
            SanityDamageValueList.Add(damage);
        }
        if (sanity <= 0)
        {
            sanity = 0;
            Death();
        }
    }
    public void SanityHeal(float amount)
    {
        sanity += amount;
        if (sanity > maxSanity) sanity = maxSanity;
    }
    public float GetHealth()
    {
        return health;
    }
    public float GetSanity()
    {
        return sanity;
    }
    private void Death()
    {
        GetComponent<PlayerInput>().actions.Disable();
        AimStateManager aim = GetComponent<AimStateManager>();
        aim.SwitchState(aim.hip);
        ActionStateManager action = GetComponent<ActionStateManager>();
        GetComponent<InteractableItemManager>().interactables.Clear();
        action.currentWeapon.SetActive(false);
        Rig.weight = 0;
        animator.applyRootMotion = true;
        animator.SetLayerWeight(1, 0);
        animator.SetTrigger("Death");
        HealthUI.SetActive(false);
        DeathUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        for (int i = 0; i < HealthDamageSourceList.Count; i++)
        {
            GameObject damageSource = Instantiate(DamageSourcePrefab, HealthDamageList.transform);
            damageSource.GetComponent<DamageSourceUI>().SetDamageSource(HealthDamageSourceList[i].tag, HealthDamageValueList[i]);
            if (i == 4) { break; }
        }
        for (int i = 0; i < SanityDamageSourceList.Count; i++)
        {
            GameObject damageSource = Instantiate(DamageSourcePrefab, SanityDamageList.transform);
            damageSource.GetComponent<DamageSourceUI>().SetDamageSource(SanityDamageSourceList[i].tag, SanityDamageValueList[i]);
            if (i == 4) { break; }
        }
        enabled = false;
    }
}

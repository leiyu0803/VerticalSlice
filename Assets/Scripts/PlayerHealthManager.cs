using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private float health;
    public float maxHealth = 100f;
    private float sanity;
    public float maxSanity = 100f;

    private UIManager uiManager;

    private void Start()
    {
        health = maxHealth;
        sanity = maxSanity;
        uiManager = GetComponent<UIManager>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0) health = 0;
        uiManager.healthLastUpdateTime = 0;
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
    }

    public void SanityDamage(float damage)
    {
        sanity -= damage;
        if (sanity < 0) sanity = 0;
        uiManager.sanityLastUpdateTime = 0;
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
}

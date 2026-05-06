using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] AIManager aiManager;
    float LastDamageTime;

    private void Update()
    {
        LastDamageTime += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player")&&LastDamageTime>=1)
        {
            other.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(aiManager.HEALTH_DAMAGE, aiManager.gameObject);
            LastDamageTime = 0;
        }
    }
}

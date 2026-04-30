using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health;
    Animator animator;
    bool isDead = false;
    NavMeshAgent agent;
    AIManager aiManager;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        aiManager = GetComponent<AIManager>();
    }
    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            Debug.Log("Enemy health: " + health);
            if (health <= 0)
            {
                StartCoroutine(EnemyDeath());
            }
        }
    }
    void Update()
    {
        if (isDead)
        {
            transform.Translate(0, -1 * Time.deltaTime, 0);
        }
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator EnemyDeath()
    {
        animator.SetTrigger("Death");
        agent.enabled = false;
        aiManager.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(5);
        isDead = true;
    }
}

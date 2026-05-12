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
    public GameObject damage;
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
        if (isDead && transform.position.y > -3)
        {
            transform.Translate(0, -1 * Time.deltaTime, 0);
        }
    }
    IEnumerator EnemyDeath()
    {
        if(animator != null) animator.SetTrigger("Death");
        if(agent != null) agent.enabled = false;
        if(aiManager != null) aiManager.enabled = false;
        if(damage != null) damage.SetActive(false);
        GameObject a = GameObject.Find("TutorialProgress");
        if (a != null) 
        {
            if(a.GetComponent<TutorialProgress>().progress == 5 || a.GetComponent<TutorialProgress>().progress == 6 || a.GetComponent<TutorialProgress>().progress == 16)
            {
                a.GetComponent<TutorialProgress>().progress++;
            }
            if(gameObject.name == "Target")
            {
                Destroy(gameObject);
            }
        }
        if(GetComponent<CapsuleCollider>() != null) GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(5);
        isDead = true;
    }
}

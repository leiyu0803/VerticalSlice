using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health;
    Animator animator;
    bool isDead = false;
    void Start()
    {
        animator = GetComponent<Animator>();
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
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(5);
        isDead = true;
    }
}

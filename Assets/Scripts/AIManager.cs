using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent agent;
    Animator animator;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("Walk", true);
        agent.SetDestination(player.transform.position);
    }
}

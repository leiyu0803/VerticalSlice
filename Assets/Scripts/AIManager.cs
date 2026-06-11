using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    public float HEALTH_DAMAGE = 20, SANITY_DAMAGE = 20;

    [SerializeField] float lookDistance = 30, fov = 120, attackDistance = 5;
    [SerializeField] Transform EnemyEyes;
    Transform playerHead;

    bool WasFirstTimeSeeingPlayer = true;
    bool WasSeeingPlayer = false;
    bool WasRageAnimPlayed = false;
    bool WasAttacking = false;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        playerHead = GameObject.FindWithTag("PlayerHead").transform;
    }
    private void FixedUpdate()
    {
        if (PlayerSeen())
        {
            WasSeeingPlayer = true;
        }
        else
        {
            WasSeeingPlayer = false;
        }
    }

    private void Update()
    {
        if(WasSeeingPlayer)
        {
            OnPlayerSeen();
        }
        else
        {
            agent.SetDestination(transform.position);
            animator.SetBool("Walk", false);
        }
    }
    public void Rage()
    {
        player.GetComponent<PlayerHealthManager>().SanityDamage(SANITY_DAMAGE,this.gameObject);
    }

    public void OnRageAnimationPlayed()
    {
        WasRageAnimPlayed = true;
    }

    public bool PlayerSeen()
    {
        if(Vector3.Distance(EnemyEyes.position, playerHead.position) > lookDistance)return false;

        Vector3 dirToPlayer = (playerHead.position - EnemyEyes.position).normalized;
        float angleToPlayer = Vector3.Angle(EnemyEyes.parent.forward, dirToPlayer);
        if(angleToPlayer > fov / 2) return false;

        EnemyEyes.LookAt(playerHead.position);

        RaycastHit hit;
        if(Physics.Raycast(EnemyEyes.position, EnemyEyes.forward, out hit, lookDistance))
        {
            if (hit.transform == null) return false;
            if (hit.transform.name == playerHead.name) 
            {
                Debug.DrawLine(EnemyEyes.position, hit.point, Color.green);
                return true;
            }
        }
        return false;
    }
    private void Attack()
    {
        if(!WasAttacking)
        {
            WasAttacking = true;
            animator.SetTrigger("Attack");
        }
    }
    public void OnAttackAnimationPlayed()
    {
        WasAttacking = false;
    }
    private void OnPlayerSeen()
    {
        if (WasFirstTimeSeeingPlayer)
        {
            WasFirstTimeSeeingPlayer = false;
            animator.SetTrigger("Rage");
            audioSource.PlayOneShot(audioClip);
        }
        else if (WasAttacking)
        {
            agent.SetDestination(transform.position);
        }
        else if(Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {
            agent.SetDestination(transform.position);
            animator.SetBool("Walk", false);
            Attack();
        }
        else if (WasRageAnimPlayed)
        {
            agent.SetDestination(player.transform.position);
            animator.SetBool("Walk", true);
        }
    }
}

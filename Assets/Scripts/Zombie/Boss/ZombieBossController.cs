using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(ZombieBossAnimationController))]
public class ZombieBossController : MonoBehaviour, IDamageable
{
    [Header("Instances")]
    [SerializeField] private GameObject medKit;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private PlayerController player;
    private ZombieBossAnimationController animationController;
    private ZombieMoveAndRotate moveAndRotate;
    private Stats stats;

    [Header("Audio")]
    [SerializeField] private AudioClip dieSound;

    [Header("Attack")]
    private float attackRangeOffset = 0.5f;
    private float attackRange;
    [SerializeField][Min(0)] private int damage = 40;

    [Header("Despawn")]
    [SerializeField][Min(0)] private float timeToDespawnSec = 2;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<PlayerController>() as PlayerController;
        moveAndRotate = GetComponent<ZombieMoveAndRotate>();
        stats = GetComponent<Stats>();
        animationController = GetComponent<ZombieBossAnimationController>();

        navMeshAgent.speed = stats.Speed;
        attackRange = navMeshAgent.stoppingDistance;
    }

    private void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
        animationController.SetSpeed(navMeshAgent.velocity.magnitude);

        if (navMeshAgent.hasPath)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                animationController.SetAtackAnimation(true);
                moveAndRotate.Rotate(player.transform.position - transform.position);
            }
            else
                animationController.SetAtackAnimation(false);
        }
    }

    public void DoDamage()
    {
        if (navMeshAgent.remainingDistance <= attackRange + attackRangeOffset)
            player.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        stats.CurrentHealth -= damage;
        if (stats.CurrentHealth <= 0)
        {
            stats.CurrentHealth = 0;
            Die();
        }
    }

    public void Die()
    {
        this.enabled = false;
        Instantiate(medKit, transform.position, Quaternion.identity);
        AudioController.instance.PlayOneShot(dieSound);
        animationController.Die();
        StartCoroutine(moveAndRotate.ClipThougthTheGround(timeToDespawnSec));
        Destroy(gameObject, timeToDespawnSec+1);
        navMeshAgent.enabled = false;
        rb.isKinematic = false;
    }
}

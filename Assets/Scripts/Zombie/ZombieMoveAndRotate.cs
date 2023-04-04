using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMoveAndRotate : MoveAndRotate
{
    private PlayerController player;
    public Vector3 Dir {get; private set;}
    private Vector3 newDir;
    public float Dist { get; private set; }
    [SerializeField] private float chaseRange = 15;
    [SerializeField] public float AttackRange { get; private set; } = 2.5f;
    [SerializeField][Min(1)] private float randomPosRange = 10;
    [SerializeField][Min(1)] private float wanderTimer = 5;
    private float wanderCount;

    private void Start()
    {
        player = GameObject.FindWithTag(Strings.PlayerTag).GetComponent<PlayerController>();
        Dist = Vector3.Distance(player.transform.position, transform.position);
    }

    public void Move(float speed, out float attackRange)
    {
        try
        {
            Dist = Vector3.Distance(player.transform.position, transform.position);
        }
        catch (System.NullReferenceException)
        {
            Dist = Mathf.Infinity;
        }
            
        attackRange = this.AttackRange;
        if (Dist > chaseRange) Wander(speed);
        else if (Dist > attackRange) Chase(speed);
    }

    public void Rotate()
    {
        base.Rotate(Dir);
    }

    private void Wander(float speed)
    {
        wanderCount -= Time.deltaTime;

        if(wanderCount <= 0){
            newDir = GetRandomPos();
            wanderCount += wanderTimer + Random.Range(-1f, 1f);
        }

        bool closeEnough = Vector3.Distance(transform.position, newDir) <= 0.05;
        if(!closeEnough){
            Dir = (newDir - transform.position);
            Move(Dir.normalized, speed);
        }
    }


    private Vector3 GetRandomPos(){
        Vector3 randomPos = Random.insideUnitSphere * randomPosRange;
        randomPos += transform.position;
        randomPos.y = transform.position.y;
        return randomPos;
    }

    private void Chase(float speed)
    {
        Dir = (player.transform.position - transform.position).normalized;
        
        Move(Dir, speed);
    }

    public IEnumerator ClipThougthTheGround(float timeToDespawnSec){
        rb.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(timeToDespawnSec);
        rb.constraints = RigidbodyConstraints.None;
    }
}

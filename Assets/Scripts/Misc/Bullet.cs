using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField][Min(0)] private float speed = 30;
    [SerializeField][Min(0)] private int damage = 1;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other){
        Quaternion oppositeRotation = Quaternion.LookRotation(-transform.forward);

        if(other.tag == Strings.EnemyTag && other.GetComponent<ZombieController>() != null){
            ZombieController zombie = other.GetComponent<ZombieController>();
            zombie.TakeDamage(damage);
            zombie.InstantiateBloodParticle(transform.position, oppositeRotation);
        }
        
        if(other.tag == Strings.BossTag && other.GetComponent<ZombieBossController>() != null){
            ZombieBossController boss = other.GetComponent<ZombieBossController>();
            boss.TakeDamage(damage);
            boss.InstantiateBloodParticle(transform.position, oppositeRotation);
        }

        Destroy(gameObject);
    }
}

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
        if(other.tag == "enemy" && other.GetComponent<ZombieController>() != null){
            other.GetComponent<ZombieController>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}

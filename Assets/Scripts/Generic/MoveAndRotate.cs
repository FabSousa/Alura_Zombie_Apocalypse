using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class MoveAndRotate : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Move(Vector3 dir, float speed){
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }

    public virtual void Rotate(Vector3 target){
        Quaternion newRoration = Quaternion.LookRotation(target);
        rb.MoveRotation(newRoration);
    }
}

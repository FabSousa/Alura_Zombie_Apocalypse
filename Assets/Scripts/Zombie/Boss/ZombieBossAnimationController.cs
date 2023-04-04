using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ZombieBossController))]

public class ZombieBossAnimationController : MonoBehaviour
{
    private Animator animator;
    private ZombieBossController controller;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<ZombieBossController>();
    }

    void Update()
    {
        
    }

    public void SetSpeed(float speed){
        animator.SetFloat(Strings.ZombieMovAnimation, speed);
    }

    public void SetAtackAnimation(bool value){
        animator.SetBool(Strings.IsAtackingAnimation, value);
    }

    private void ZombieAttackEnd(){
        controller.DoDamage();
    }

    public void Die(){
        animator.SetTrigger(Strings.ZombieDieAnimation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZombieController))]
[RequireComponent(typeof(Animator))]
public class ZombieAnimationController : MonoBehaviour
{
    private GameMode gm;
    private ZombieController zc;
    private ZombieMoveAndRotate zmr;
    private Animator animator;

    private void Awake()
    {
        zc = GetComponent<ZombieController>();
        zmr = GetComponent<ZombieMoveAndRotate>();
        animator = GetComponent<Animator>();
        gm = GameObject.FindWithTag(Strings.CoreScriptsTag).GetComponent<GameMode>();
    }

    private void Update()
    {
        SetParameters();
        AtackAnimation();
    }

    private void SetParameters(){
        animator.SetFloat(Strings.ZombieMovAnimation, zmr.Dir.magnitude);
    }

    private void AtackAnimation(){
        if(zmr.Dist > zmr.AttackRange)  animator.SetBool(Strings.IsAtackingAnimation, false);
        else animator.SetBool(Strings.IsAtackingAnimation, true);
    }

    private void ZombieAttackEnd(){
        zc.DoDamage();
    }
    
    public void Die(){
        animator.SetTrigger(Strings.ZombieDieAnimation);
    }
}

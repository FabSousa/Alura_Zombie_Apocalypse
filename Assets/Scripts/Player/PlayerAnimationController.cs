using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerMoveAndRotate playerMoveAndRotate;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMoveAndRotate = GetComponent<PlayerMoveAndRotate>();
    }

    void Update()
    {
        animator.SetFloat(Strings.PlayerMovAnimation, playerMoveAndRotate.Dir.magnitude);
    }
}

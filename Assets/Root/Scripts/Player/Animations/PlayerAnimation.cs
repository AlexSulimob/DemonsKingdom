using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    Vector2 _direction;
    bool _isIdle;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var dir = Singleton<ControlInst>.Instance.Control.Player.Movement.ReadValue<Vector2>();
        if (dir != Vector2.zero)
        {
            if (Singleton<PlayerStates>.Instance.currentState != PlayerStates.pStates.Attacking)
            {
                _direction = dir;
            }
   
            _isIdle = false;
        } else { _isIdle = true; }

        animator.SetBool("isIdle",_isIdle);
        animator.SetFloat("x_Direction", _direction.x);
        animator.SetFloat("y_direction", _direction.y);
        if (Singleton<PlayerStates>.Instance.currentState == PlayerStates.pStates.Dead)
        {

            animator.SetBool("isAttacking", false);
            animator.SetBool("isDashing", false);
            animator.SetBool("isGettignHit", false);
            animator.SetBool("isWalking", false);

            animator.SetBool("isDead", true);
            return;
        }

        animator.SetBool("isInvicible", Singleton<PlayerStates>.Instance.isInvincible ? true:false);


        switch (Singleton<PlayerStates>.Instance.currentState)
        {
            case PlayerStates.pStates.Walking:
                animator.SetBool("isAttacking", false);
                animator.SetBool("isDashing", false);
                animator.SetBool("isGettignHit", false);
                animator.SetBool("isWalking", true);              
                break;
            case PlayerStates.pStates.Dashing:
                animator.SetBool("isAttacking", false);
                animator.SetBool("isDashing", true);
                animator.SetBool("isGettignHit", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", false);
                break;
            case PlayerStates.pStates.Attacking:
                animator.SetBool("isAttacking", true);
                animator.SetBool("isDashing", false);
                animator.SetBool("isGettignHit", false);
                animator.SetBool("isWalking", false);
                break;
            case PlayerStates.pStates.GettingDamage:
                animator.SetBool("isAttacking", false);
                animator.SetBool("isDashing", false);
                animator.SetBool("isGettignHit", true);
                animator.SetBool("isWalking", false);
                break;
            case PlayerStates.pStates.Dead:
                break;
            default:
                break;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Crow
{
    public class CrowAnimationController : MonoBehaviour
    {
        Rigidbody2D rb;
        Animator animator;
        Vector2 _direction;
        CrowState crowState;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            crowState = GetComponent<CrowState>();
        }

        void Update()
        {
            SetDrection();
            animator.SetFloat("x_direction", _direction.x);
            animator.SetFloat("y_direction", _direction.y);

            switch (crowState.CurrentState)
            {
                case CrowState.CrowStates.Idle:
                    animator.SetBool("isIdle", true);
                    animator.SetBool("isAttacking", false);
                    animator.SetBool("isGettingHit", false);
                    animator.SetBool("isDead", false);
                    break;
                case CrowState.CrowStates.Patrol:
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isAttacking", false);
                    animator.SetBool("isGettingHit", false);
                    animator.SetBool("isDead", false);
                    break;
                case CrowState.CrowStates.Attacking:
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isAttacking", true);
                    animator.SetBool("isGettingHit", false);
                    animator.SetBool("isDead", false);
                    break;
                case CrowState.CrowStates.GettingHit:
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isAttacking", false);
                    animator.SetBool("isGettingHit", true);
                    animator.SetBool("isDead", false);
                    break;
                case CrowState.CrowStates.Dead:
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isAttacking", false);
                    animator.SetBool("isGettingHit", false);
                    animator.SetBool("isDead", true);
                    break;
                
                default:Debug.Log("huita");
                    break;
            }
            

        }
        void SetDrection()
        {
            if (rb.velocity.normalized != Vector2.zero)
            {
                _direction = rb.velocity.normalized;
            }
        }
    }
}
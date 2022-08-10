using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMiliAnimation : MonoBehaviour
{
    EnemiMiliStates enemiMiliStates;
    public EnemyHealth enemyHealth;
    Animator animator;
    Rigidbody2D _rigidbody2D;
    Vector2 _direction;
    bool _isIdle;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        enemiMiliStates = GetComponent<EnemiMiliStates>();
        animator = GetComponent<Animator>();

        enemyHealth.dead += () => animator.SetBool("isDead", true);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(enemiMiliStates.CurrentState);
        var dir = _rigidbody2D.velocity;
        if (dir != Vector2.zero)
        {
            if (enemiMiliStates.CurrentState != EnemiMiliStates.States.Attack)
            {
                _direction = dir;
            }

            _isIdle = false;
        }
        else { _isIdle = true; }

        animator.SetBool("IsIdle", _isIdle);
        animator.SetFloat("x_direction", _direction.x);
        animator.SetFloat("y_direction", _direction.y);

        if (enemiMiliStates.CurrentState == EnemiMiliStates.States.Dead)
        {
            return;
        }
        if (enemiMiliStates.CurrentState == EnemiMiliStates.States.GettingHit)
        {
            animator.SetBool("isGettingHit", true);
        }else { animator.SetBool("isGettingHit", false); }

        if (enemiMiliStates.CurrentState == EnemiMiliStates.States.Attack)
        {
            animator.SetBool("Attack", true); 
        }else
        {
            animator.SetBool("Attack", false);
        }
    }
}

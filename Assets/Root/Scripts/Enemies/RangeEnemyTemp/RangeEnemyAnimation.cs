using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyAnimation : MonoBehaviour
{

    AnimationEvents animEvents;
    EnemiMiliStates enemiMiliStates;
    public EnemyHealth enemyHealth;
    Animator animator;
    Rigidbody2D _rigidbody2D;
    Vector2 _direction;
    bool _isIdle;
    Vector2 _tartgetPos;
    bool _isAttacking = false;
    void Start()
    {
        animEvents= GetComponent<AnimationEvents>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        enemiMiliStates = GetComponent<EnemiMiliStates>();
        animator = GetComponent<Animator>();

        animEvents.InitRangeAttack += enemyTarget =>  {_tartgetPos = enemyTarget; _isAttacking = true;};
        animEvents.EndAttack += () => _isAttacking = false;
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
                
            }
            _direction = dir;

            _isIdle = false;
        }
        else { _isIdle = true; }

        if(_isAttacking){
            //Debug.Log("first attack  : " + _tartgetPos);
            _direction = _tartgetPos - _rigidbody2D.position;
        } 
        _direction = _direction.normalized;
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

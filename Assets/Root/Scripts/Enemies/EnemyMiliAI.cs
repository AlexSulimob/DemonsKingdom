using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

using static EnemiMiliStates;
public class EnemyMiliAI : MonoBehaviour
{
    public float TickRate = 1f;
    Rigidbody2D rb;
    Seeker seeker;
    StateMachine stateMachine;
    public Transform target;
    float distanceToTarget;

    //follow
    public float maxFollowDistance;
    public float speedFollow;
    //patrol
    public float speedPatrol;
    public float patrolWaitTime;
    //getHit
    public float speedFlightHit;
    public float durationFlight;
    public float stunDurationAfterFlight;

    public float AttackImpulsTime = 0.2f;
    public float AttackImpulsSpeed = 5f;

    public PlayerInTriggerEvent agroEventObj;
    public PlayerInTriggerEvent AtkEventObj;
    public AnimationEvents animationEvents;
    public EnemyHealth enemyHealth;

    [HideInInspector]
    public Vector2 directionHit;

    [HideInInspector]
    public EnemiMiliStates enemyState;

    bool isDead = false;
    Vector2 _lastDirection;

    IState _doNothing;
    void Start()
    {
        stateMachine = new StateMachine();
        rb = GetComponent<Rigidbody2D>();
        enemyState = GetComponent<EnemiMiliStates>();
        seeker = GetComponent<Seeker>();

        //событие на нахождения в агро зоне
        Action OnAgroRange = () =>
            enemyState.CurrentState = enemyState.CurrentState == States.GettingHit ? States.GettingHit : States.Follow;

        agroEventObj.PlayerInTrigger += OnAgroRange;

        //когда цель в зоне действия атаки
        Action OnAttack = () =>
            enemyState.CurrentState = enemyState.CurrentState == States.GettingHit ? States.GettingHit : States.Attack;

        AtkEventObj.PlayerInTrigger += OnAttack;

        //атака закончилась
        animationEvents.EndAttack += () =>
            enemyState.CurrentState = States.Follow;

        //получил в ебало
        enemyHealth.isGetingHit += delegate (Vector2 directionHit)
        {
            this.directionHit = directionHit;
            if (enemyState.CurrentState != States.Dead)
            {
                enemyState.CurrentState = States.GettingHit;
            }

        };

        //событие смерти 
        enemyHealth.dead += delegate ()
        {
            if (enemyState.CurrentState != States.Dead)
            {
                StartCoroutine("DeadLastFlight");
            }
            isDead = true;
            enemyState.CurrentState = States.Dead;
            agroEventObj.PlayerInTrigger -= OnAgroRange;
            AtkEventObj.PlayerInTrigger -= OnAttack;

        };




        var MiliArgState = new EnemyMiliAgrState(rb, seeker, target);
        var randomMovementState = new RandomPatrolState(rb, seeker);
        var miliAttackState = new EmptyState(() => "attacking" );
        var getDamageState = new GetDamageState(this, rb);
        _doNothing = miliAttackState;

        randomMovementState.Speed = speedPatrol;
        randomMovementState.RefreshRate = patrolWaitTime;

        MiliArgState.Speed = speedFollow;

        getDamageState.Speed = speedFlightHit;
        getDamageState.HitDuration = durationFlight;
        getDamageState.StunDuration = stunDurationAfterFlight;

        stateMachine.AddTransition(randomMovementState, MiliArgState, () => enemyState.CurrentState == States.Follow);
        stateMachine.AddTransition(MiliArgState, randomMovementState, () => enemyState.CurrentState == States.RandomMovement);
        stateMachine.AddTransition(MiliArgState, miliAttackState, () => enemyState.CurrentState == States.Attack);
        stateMachine.AddTransition(miliAttackState, MiliArgState, () => enemyState.CurrentState == States.Follow);
        stateMachine.AddTransition(getDamageState, MiliArgState, () => enemyState.CurrentState == States.Follow);
        stateMachine.AddTransition(getDamageState, randomMovementState, () => enemyState.CurrentState == States.RandomMovement);

        stateMachine.AddAnyTransition(getDamageState, () => enemyState.CurrentState == States.GettingHit);
        stateMachine.AddAnyTransition(miliAttackState, () => enemyState.CurrentState == States.Dead);

        stateMachine.SetState(randomMovementState);
        //StartCoroutine("TickState");
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            enemyState.CurrentState = States.Dead;
            return;
        }
        
        distanceToTarget = Vector2.Distance(rb.position, target.position);
        if (distanceToTarget >= maxFollowDistance && enemyState.CurrentState == States.Follow)
        {
            enemyState.CurrentState = States.RandomMovement;
        }
        stateMachine.Tick();
        if (rb.velocity != Vector2.zero)
        {
            _lastDirection = rb.velocity;
        }
    }
    IEnumerator DeadLastFlight()
    {
        rb.velocity = directionHit * speedFlightHit;
        yield return new WaitForSeconds(durationFlight);
        rb.velocity = Vector2.zero;

    }
    public void AttackMoving()
    {
        StartCoroutine("AttackMovingC");
    }
    IEnumerator AttackMovingC()
    {
        float elapsedTime = 0;

        while (elapsedTime < AttackImpulsTime)
        {
            rb.velocity = _lastDirection.normalized * Mathf.Lerp(AttackImpulsSpeed, 0f, (elapsedTime / AttackImpulsTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        yield return null;
    }
    private void OnDestroy()
    {
        stateMachine.SetState(_doNothing);
    }
}

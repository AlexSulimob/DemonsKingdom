using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDamageState : IState
{
    IEnumerator coroutine;
    private readonly EnemyMiliAI enemyMiliAI;
    private Rigidbody2D rigidbody2D;
    Vector2 directionHit;

    private float speed = 25f;
    private float hitDuration = 0.1f;
    private float stunDuration = 5f;
    public float Speed { get => speed; set => speed = value; }
    public float HitDuration { get => hitDuration; set => hitDuration = value; }
    public float StunDuration { get => stunDuration; set => stunDuration = value; }

    public GetDamageState(EnemyMiliAI enemyMiliAI, Rigidbody2D rigidbody2D = null)
    {
        this.enemyMiliAI = enemyMiliAI;
        this.rigidbody2D = rigidbody2D;

    }

    public void OnEnter()
    {
        coroutine = GetDamage();
        directionHit = enemyMiliAI.directionHit;
        Singleton<GameManager>.Instance.StartCoroutine(coroutine);
    }

    public void OnExit()
    {
        Singleton<GameManager>.Instance.StopCoroutine(coroutine);
    }

    public void Tick()
    {

    }
    IEnumerator GetDamage()
    {
        rigidbody2D.velocity = directionHit * speed;
        yield return new WaitForSeconds(hitDuration);
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunDuration);
        enemyMiliAI.enemyState.CurrentState = EnemiMiliStates.States.Follow;
    }

}

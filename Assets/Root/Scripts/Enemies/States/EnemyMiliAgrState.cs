using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMiliAgrState : IState
{
    public float Speed { get; set; }
    public float RefreshRate { get => refreshRate; set => refreshRate = value; }

    IEnumerator coroutine; //переменна€ дл€ корутины, котора€ обновл€ет путь с задержкой. Ќе забудь еЄ выключить при выходе из стейта
    Path path;
    Seeker seeker;
    Rigidbody2D rigidbody2D;
    Transform target;

    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    float nextWaypointDistance = .1f;//расто€ние до следующеей точки в пути

    float refreshRate = 1f;
    public EnemyMiliAgrState(Rigidbody2D rigidbody2D = null, Seeker seeker = null, Transform target = null)
    {
        this.rigidbody2D = rigidbody2D;
        this.seeker = seeker;
        this.target = target;
    }

    public void OnEnter()
    {
        coroutine = UpdatePath();
        Singleton<GameManager>.Instance.StartCoroutine(coroutine);
    }
    IEnumerator UpdatePath()
    {
        for (; ; )
        {
            seeker.StartPath(rigidbody2D.position, target.position, OnPathComplete);
            yield return new WaitForSeconds(refreshRate);
        }

    }

    //завершение построени€ пути
    void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentWaypoint = 0;
        }
    }
    public void OnExit()
    {

        rigidbody2D.velocity = Vector2.zero; //останавливаем тело
        Singleton<GameManager>.Instance.StopCoroutine(coroutine); // выходим с корутины
    }

    public void Tick()
    {
        if (path == null)
        {
            return; //путь не нашЄлс€ и мы выходим
        }
        if(currentWaypoint >= path.vectorPath.Count) //дошли ли мы до конца пути. Ѕесполезна€ тут штука
        {
            reachedEndOfPath = true; //да, выходим
            return;
        }else
        {
            reachedEndOfPath = false;// не, дальше идЄм
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2D.position).normalized;
        float distance = Vector2.Distance(rigidbody2D.position, path.vectorPath[currentWaypoint]);
        if (direction == Vector2.zero) // иногда при обновление маршрута направление становитс€ равно нулю. 
        {
            direction = rigidbody2D.velocity.normalized; //Ётот костыль его убирает
        }
        rigidbody2D.velocity = direction * Speed;

        //priewDirection
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }


}

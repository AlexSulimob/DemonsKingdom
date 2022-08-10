using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class RandomPatrolState : IState
{
    public float Speed { get; set; }
    public float RefreshRate { get => refreshRate; set => refreshRate = value; }

    IEnumerator coroutine;
    Path path;
    Seeker seeker;
    Rigidbody2D rigidbody2D;
    Transform target;

    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    float nextWaypointDistance = .1f;

    float refreshRate = 10f;


    float Min = 1f;
    float Max = 2f;

    Vector2 getRandomPoint(Vector2 startPos)
    {
        float x_value;
        float y_value;
        var high_x = Random.Range(startPos.x + Min, startPos.x + Max);
        var low_x = Random.Range(startPos.x - Min, startPos.x - Max);

        var high_y = Random.Range(startPos.y + Min, startPos.y + Max);
        var low_y = Random.Range(startPos.y - Min, startPos.y - Max);

        if (Random.Range(0,2) == 1)
        {
            x_value = high_x;
        }else { x_value = low_x; }

        if (Random.Range(0, 2) == 1)
        {
            y_value = high_y;
        }
        else { y_value = low_y; }

        return new Vector2(x_value, y_value);
    }
    public RandomPatrolState(Rigidbody2D rigidbody2D = null, Seeker seeker = null)
    {
        this.rigidbody2D = rigidbody2D;
        this.seeker = seeker;
        Random.Range(1, 2);
    }

    public void OnEnter()
    {
        coroutine = UpdatePath();
        Singleton<GameManager>.Instance.StartCoroutine(coroutine);
    }
    IEnumerator UpdatePath()
    {
        for ( ; ; )
        {
            seeker.StartPath(rigidbody2D.position, getRandomPoint(rigidbody2D.position), OnPathComplete);
            yield return new WaitForSeconds(refreshRate);
        }

    }


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

        rigidbody2D.velocity = Vector2.zero; 
        Singleton<GameManager>.Instance.StopCoroutine(coroutine); 
    }

    public void Tick()
    {
        if (path == null)
        {
            return; 
        }
        if (currentWaypoint >= path.vectorPath.Count) 
        {
            rigidbody2D.velocity = Vector2.zero;
            reachedEndOfPath = true; 
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2D.position).normalized;
        float distance = Vector2.Distance(rigidbody2D.position, path.vectorPath[currentWaypoint]);

        rigidbody2D.velocity = direction * Speed;

        //priewDirection
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }
}

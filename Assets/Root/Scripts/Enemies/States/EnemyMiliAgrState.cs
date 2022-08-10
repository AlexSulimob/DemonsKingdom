using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMiliAgrState : IState
{
    public float Speed { get; set; }
    public float RefreshRate { get => refreshRate; set => refreshRate = value; }

    IEnumerator coroutine; //���������� ��� ��������, ������� ��������� ���� � ���������. �� ������ � ��������� ��� ������ �� ������
    Path path;
    Seeker seeker;
    Rigidbody2D rigidbody2D;
    Transform target;

    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    float nextWaypointDistance = .1f;//��������� �� ���������� ����� � ����

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

    //���������� ���������� ����
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

        rigidbody2D.velocity = Vector2.zero; //������������� ����
        Singleton<GameManager>.Instance.StopCoroutine(coroutine); // ������� � ��������
    }

    public void Tick()
    {
        if (path == null)
        {
            return; //���� �� ������� � �� �������
        }
        if(currentWaypoint >= path.vectorPath.Count) //����� �� �� �� ����� ����. ����������� ��� �����
        {
            reachedEndOfPath = true; //��, �������
            return;
        }else
        {
            reachedEndOfPath = false;// ��, ������ ���
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2D.position).normalized;
        float distance = Vector2.Distance(rigidbody2D.position, path.vectorPath[currentWaypoint]);
        if (direction == Vector2.zero) // ������ ��� ���������� �������� ����������� ���������� ����� ����. 
        {
            direction = rigidbody2D.velocity.normalized; //���� ������� ��� �������
        }
        rigidbody2D.velocity = direction * Speed;

        //priewDirection
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }


}

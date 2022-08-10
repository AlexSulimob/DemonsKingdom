using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    Vector2 _direction;
    public float speed = 10f;
    public Vector2 TargetPost { get; set;}
    public Vector2 Direction { get => _direction; set => _direction = value; }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Direction = ((Vector3)TargetPost - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = Direction * speed;
    }

}

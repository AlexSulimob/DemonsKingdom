using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAttack : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isPlayer = false;
    Vector2 direction;
    
    void Update()
    {
        
        direction = rb.velocity.normalized;
        if (isPlayer)
        {
            direction = Singleton<ControlInst>.Instance.Control.Player.Movement.ReadValue<Vector2>().normalized;
        }
        if (direction == Vector2.zero)
        {
            return;
        }
        float angle = Vector2.Angle(Vector2.right, direction);
        if (direction.y < 0.0f) { angle = 360.0f - angle; }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
